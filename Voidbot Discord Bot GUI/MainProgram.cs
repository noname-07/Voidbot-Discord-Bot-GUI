﻿using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Color = Discord.Color;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
namespace Voidbot_Discord_Bot_GUI
{
    public class MainProgram
    {
        private static Form1 _instance;

        public static Form1 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Form1();
                }
                return _instance;
            }
        }


        private DiscordSocketClient _client;
        // Expose _client through a property

        DiscordSocketClient client = new DiscordSocketClient();




        public DiscordSocketClient DiscordClient
        {
            get { return _client; }
            private set { _client = value; }
        }
        private string GptApiKey;
        private string DiscordBotToken;
        string startupPath = AppDomain.CurrentDomain.BaseDirectory;
        string userfile;
        string contentstr;
        string BotNickname;
        public bool isBotRunning = false;
        private bool shouldReconnect = true;
        // Define an event for log messages
        public event Action<string> LogReceived;
        public event Action<string> MessageReception;
        // Define the event
        public event Action<string> BotDisconnected;
        public event Func<Task> BotConnected;
        private CancellationTokenSource cancellationTokenSource;
        private List<LogMessage> logMessages = new List<LogMessage>();
        static void MainEntryPoint(string[] args)
           => new MainProgram().RunBotAsync().GetAwaiter().GetResult();
        public async Task LoadTasks()
        {
            // Load user settings from the INI file

            string userfile = @"\UserCFG.ini";
            GptApiKey = UserSettings(startupPath + userfile, "GptApiKey");
            DiscordBotToken = UserSettings(startupPath + userfile, "DiscordBotToken");
            contentstr = UserSettings(startupPath + userfile, "BotPersonality");
            BotNickname = UserSettings(startupPath + userfile, "BotNickname");
            Console.WriteLine(@"| API Keys Loaded. Opening connection to API Services | Status: Waiting For Connection...");
            // Check if the API keys are properly loaded
            if (string.IsNullOrEmpty(GptApiKey) || string.IsNullOrEmpty(DiscordBotToken))
            {
                Console.WriteLine("Error: API did not get SocketConnection. Are your API Keys correct? Exiting thread.");
                return;
            }


        }
        public string UserSettings(string File, string Identifier) // User Settings handler
        {
            using var S = new System.IO.StreamReader(File);
            string Result = "";
            while (S.Peek() != -1)
            {
                string Line = S.ReadLine();
                if (Line.ToLower().StartsWith(Identifier.ToLower() + "="))
                {
                    Result = Line.Substring(Identifier.Length + 1);
                }
            }
            return Result;

        }

        public void SetForm1Instance(Form1 form1Instance)
        {
            _instance = form1Instance;
        }
        public async Task StartBotAsync()
        {
            if (!isBotRunning)
            {
                isBotRunning = true;
                shouldReconnect = true; // Allow reconnection attempts
                                        // Start the bot
                await RunBotAsync();
            }
        }

        public async Task StopBot()
        {
            if (isBotRunning)
            {
                isBotRunning = false;
                shouldReconnect = false; // Prevent immediate reconnection
                                         // Stop the bot
                await DisconnectBot();
            }
        }


        public async Task DisconnectBot()
        {
            try
            {
                shouldReconnect = false; // Prevent immediate reconnection
                // Additional cleanup tasks can be added here
                if (_client != null)
                {

                    await _client.StopAsync();
                    await _client.LogoutAsync();

                    _client.Dispose();
                    _client = null;
                    DiscordClient = null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or display the error)
                Console.WriteLine($"Error during bot disconnection: {ex.Message}");
            }

            // Set _instance to null or perform any additional cleanup
            _instance = null;
        }


        public async Task RunBotAsync()
        {

            //TODO Implement proper usage of CancellationTokenSource to handle connections/disconnections gracefully.
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var socketConfig = new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences | GatewayIntents.MessageContent
                };

                await LoadTasks();

                _client = new DiscordSocketClient(socketConfig);
                _client.Log += Log;
                _client.MessageReceived += HandleMessageAsync;
                _client.UserJoined += UserJoined;
                _client.Ready += BotConnected;
                _client.Connected += () =>
                {
                    Console.WriteLine("Connected to Discord!");
                    return Task.CompletedTask;
                };

                _client.Disconnected += async (exception) =>
                {
                    Console.WriteLine($"Disconnected from Discord. Error: {exception?.Message}");

                    if (shouldReconnect)
                    {
                        await Task.Delay(new Random().Next(3, 5) * 1000);
                        await StartBotAsync();
                    }
                };

                // Start checking the connection state in a separate task
                Task.Run(async () =>
                {
                    while (true)
                    {
                        // Check if there are any logged error messages, if so, save to file for logging
                        bool hasErrors = logMessages.Any(logMessage => logMessage.Severity == LogSeverity.Error);
                        // If there are errors, trigger the event
                        if (hasErrors)
                        {
                            BotDisconnected?.Invoke("Error: Check Bot_logs.txt");
                        }
                        // Wait for a short interval before checking again
                        await Task.Delay(1000);
                    }
                });
                await _client.LoginAsync(TokenType.Bot, DiscordBotToken);
                await _client.StartAsync();

                // Use Task.Delay to keep the method alive until cancellation
                await Task.Delay(Timeout.Infinite, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Cancellation was requested, handle accordingly
                Console.WriteLine("Bot stopping due to cancellation request.");

                // Perform additional cleanup tasks if needed
                DisconnectBot().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"Error in RunBotAsync: {ex.Message}");
            }
        }



        private async Task Log(LogMessage arg)
        {
            // Append the log message to the file
            string logText = $"{DateTime.Now} [{arg.Severity}] {arg.Source}: {arg.Exception?.ToString() ?? arg.Message}";
            // Trigger the event with the log message
            LogReceived?.Invoke(logText);


            // Save to the file
            string filePath = Path.Combine(startupPath, "bot_logs.txt");

            try
            {
                // Check if the file exists, and create it if it doesn't
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        await sw.WriteLineAsync(logText);
                    }
                }
                else
                {
                    // Append the log text to the existing file
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        await sw.WriteLineAsync(logText);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public async Task<List<RestBan>> GetBanList(ulong guildId)
        {

            var guild = _client.GetGuild(guildId);

            if (guild != null)
            {
                // Retrieve the ban list
                var bansCollections = await guild.GetBansAsync().ToListAsync();

                // Flatten the nested collections into a single list
                var bans = bansCollections.SelectMany(collection => collection).ToList();

                return bans;
            }
            else
            {
                Console.WriteLine("Invalid ServerID provided.");
                return null;
            }
        }

        public async Task PopulateListViewWithConnectedUsersAsync()
        {
            try
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        // Check if the form is still accessible before updating the UI
                        if (_instance != null && !_instance.IsDisposed)
                        {
                            string userfile2 = @"\UserCFG.ini";
                            string GuildIDString = UserSettings(startupPath + userfile2, "ServerID"); // Your Server ID as string

                            // Convert string to ulong
                            if (ulong.TryParse(GuildIDString, out ulong GuildID))
                            {
                                // Fetch the list of connected users
                                var connectedUsers = (await GetConnectedUsersAsync(GuildID)).Cast<SocketUser>().ToList();

                                if (_instance.nsListView2?.IsHandleCreated == true)
                                {
                                    _instance.nsListView2.SuspendLayout();

                                    // Remove users no longer in the connectedUsers list
                                    foreach (var item in _instance.nsListView2._Items.ToList())
                                    {
                                        var userId = ulong.Parse(item.SubItems[1].Text);
                                        if (!connectedUsers.Any(user => user.Id == userId))
                                        {
                                            _instance.nsListView2.BeginInvoke(new Action(() =>
                                            {
                                                // Clear all items
                                                _instance.nsListView2.RemoveItemAt(0);

                                                // Re-add remaining items
                                                foreach (var user in connectedUsers)
                                                {
                                                    var nsListViewItem = _instance.nsListView2._Items.FirstOrDefault(item =>
                                                        item.Text == (user.GlobalName ?? user.Username));

                                                    if (nsListViewItem == null)
                                                    {
                                                        nsListViewItem = new NSListView.NSListViewItem();
                                                        nsListViewItem.Text = user.GlobalName ?? user.Username;
                                                        _instance.nsListView2.AddItem(nsListViewItem.Text, user.Username, user.Id.ToString());
                                                    }
                                                    else
                                                    {
                                                        nsListViewItem.SubItems[0].Text = user.Username;
                                                        nsListViewItem.SubItems[1].Text = user.Id.ToString();
                                                    }
                                                }

                                                // Update label on the UI thread
                                                _instance.nsLabel29.Value1 = _instance.nsListView2.Items.Length.ToString();
                                            }));
                                        }
                                    }

                                    foreach (var user in connectedUsers)
                                    {
                                        var nsListViewItem = _instance.nsListView2._Items.FirstOrDefault(item =>
                                        item.Text == (user.GlobalName ?? user.Username));

                                        if (nsListViewItem == null)
                                        {
                                            // Create a new item only if it doesn't exist
                                            nsListViewItem = new NSListView.NSListViewItem();
                                            nsListViewItem.Text = user.GlobalName ?? user.Username;
                                            _instance.nsListView2.AddItem(nsListViewItem.Text, user.Username, user.Id.ToString());
                                        }
                                        else
                                        {
                                            // Update the existing item with the new information
                                            nsListViewItem.SubItems[0].Text = user.Username;
                                            nsListViewItem.SubItems[1].Text = user.Id.ToString();
                                        }
                                    }

                                    _instance.nsListView2.InvalidateLayout();
                                    _instance.nsListView2.ResumeLayout();

                                    // Update label on the UI thread
                                    _instance.BeginInvoke(new Action(() =>
                                    {
                                        _instance.nsLabel29.Value1 = _instance.nsListView2.Items.Length.ToString();
                                    }));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ServerID provided in UserSettings.");
                            }
                        }

                        // Wait for a short interval before checking again
                        await Task.Delay(5000);
                    }
                });
            }
            catch
            {
                Console.WriteLine("Could not load Server Member list on server." + Environment.NewLine + "This could be a network error, or an issue with Discord Servers");


            }
        }

        public async Task PopulateListViewWithBannedUsers()
        {
            try
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        // Check if the form is still accessible before updating the UI
                        if (_instance != null && !_instance.IsDisposed)
                        {
                            string userfile2 = @"\UserCFG.ini";
                            string GuildIDString = UserSettings(startupPath + userfile2, "ServerID"); // Your Server ID as string

                            // Convert string to ulong
                            if (ulong.TryParse(GuildIDString, out ulong GuildID))
                            {
                                // Get the ban list for the guild
                                var bans = await GetBanList(GuildID);

                                if (bans != null && _instance.nsListView1?.IsHandleCreated == true)
                                {
                                    _instance.nsListView1.SuspendLayout();

                                    // Remove users no longer in the ban list
                                    foreach (var item in _instance.nsListView1._Items.ToList())
                                    {
                                        var userId = ulong.Parse(item.SubItems[1].Text);
                                        if (!bans.Any(ban => ban.User.Id == userId))
                                        {
                                            _instance.nsListView1.BeginInvoke(new Action(() =>
                                            {
                                                // Clear all items
                                                _instance.nsListView1.RemoveItemAt(0);

                                                // Re-add remaining items
                                                foreach (var ban in bans)
                                                {
                                                    var nsListViewItem = _instance.nsListView1._Items.FirstOrDefault(item =>
                                                        item.Text == (ban.User.GlobalName ?? ban.User.Username));

                                                    if (nsListViewItem == null)
                                                    {
                                                        nsListViewItem = new NSListView.NSListViewItem();
                                                        nsListViewItem.Text = ban.User.GlobalName ?? ban.User.Username;
                                                        _instance.nsListView1.AddItem(nsListViewItem.Text, ban.User.Username + " #" + ban.User.Discriminator, ban.User.Id.ToString(), ban.Reason);
                                                    }
                                                    else
                                                    {
                                                        nsListViewItem.SubItems[0].Text = ban.User.Username + " #" + ban.User.Discriminator;
                                                        nsListViewItem.SubItems[1].Text = ban.User.Id.ToString();
                                                        nsListViewItem.SubItems[2].Text = ban.Reason;
                                                    }

                                                }

                                                // Update label on the UI thread
                                                _instance.nsLabel30.Value1 = _instance.nsListView1.Items.Length.ToString();
                                            }));

                                        }
                                    }

                                    foreach (var ban in bans)
                                    {
                                        var nsListViewItem = _instance.nsListView1._Items.FirstOrDefault(item =>
                                            item.Text == (ban.User.GlobalName ?? ban.User.Username));

                                        if (nsListViewItem == null)
                                        {
                                            // Create a new item only if it doesn't exist
                                            nsListViewItem = new NSListView.NSListViewItem();
                                            nsListViewItem.Text = ban.User.GlobalName ?? ban.User.Username;

                                            // Use AddItem for _instance.nsListView1
                                            _instance.nsListView1.AddItem(nsListViewItem.Text, ban.User.Username + " #" + ban.User.Discriminator, ban.User.Id.ToString(), ban.Reason);
                                        }
                                        else
                                        {
                                            // Update existing item if needed
                                            nsListViewItem.SubItems[0].Text = ban.User.Username + " #" + ban.User.Discriminator;
                                            nsListViewItem.SubItems[1].Text = ban.User.Id.ToString();
                                            nsListViewItem.SubItems[2].Text = ban.Reason;
                                        }
                                    }

                                    _instance.nsListView1.InvalidateLayout();
                                    _instance.nsListView1.ResumeLayout();

                                    // Update label on the UI thread
                                    _instance.BeginInvoke(new Action(() =>
                                    {
                                        _instance.nsLabel30.Value1 = _instance.nsListView1.Items.Length.ToString();
                                    }));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ServerID provided in UserSettings.");
                            }
                        }

                        // Wait for a short interval before checking again
                        await Task.Delay(5000);
                    }
                });
            }
            catch
            {
                Console.WriteLine("Could not load Banned Users list on server." + Environment.NewLine + "This could be a network error, or an issue with Discord Servers");
            }

        }




        // Helper method to get connected users
        private async Task<List<SocketGuildUser>> GetConnectedUsersAsync(ulong guildId)
        {
            var guild = DiscordClient.GetGuild(guildId);

            if (guild != null)
            {
                // Retrieve the list of connected users
                var connectedUsers = guild.Users.ToList();

                return connectedUsers;
            }
            else
            {
                Console.WriteLine("Invalid guild ID provided.");
                return new List<SocketGuildUser>();
            }
        }

        public async Task PopulateComboBoxWithChannels()
        {
            try
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        // Check if the form is still accessible before updating the UI
                        if (_instance != null && !_instance.IsDisposed)
                        {
                            // Get all guilds (servers) the bot is connected to
                            var newChannels = new HashSet<string>();

                            foreach (var guild in _client?.Guilds ?? Enumerable.Empty<SocketGuild>())
                            {
                                // Check if the guild is null before accessing text channels
                                if (guild == null)
                                {
                                    continue;
                                }

                                // Get all text channels in the guild
                                var textChannels = guild.TextChannels;

                                // Add each text channel's name to the temporary HashSet
                                foreach (var channel in textChannels)
                                {
                                    // Check if the channel is null before adding
                                    if (channel != null)
                                    {
                                        newChannels.Add($"{channel.Name}");
                                    }
                                }
                            }

                            // Safely update the ComboBox items only if there are changes
                            _instance.nsComboBox1?.Invoke(new Action(() =>
                            {
                                // Check if the ComboBox is still accessible
                                if (_instance.nsComboBox1.IsHandleCreated)
                                {
                                    _instance.nsComboBox1.Sorted = false; // Disable sorting temporarily
                                    var currentItems = new HashSet<string>(_instance.nsComboBox1.Items.Cast<string>());
                                    if (!currentItems.SetEquals(newChannels))
                                    {
                                        _instance.nsComboBox1.Items.Clear();
                                        foreach (var channel in newChannels)
                                        {
                                            _instance.nsComboBox1.Items.Add(channel);
                                            _instance.nsComboBox1.Sorted = true; // Enable sorting
                                        }
                                    }
                                }
                            }));
                        }

                        // Wait for a short interval before checking again
                        await Task.Delay(3000);
                    }
                });
            }
            catch
            {
                Console.WriteLine("Could not populate channel list." + Environment.NewLine + "This could be a network error, or an issue with Discord Servers");
            }
        }

        public async Task SendMessageToDiscord(string message)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    string channelName = _instance.nsComboBox1.SelectedItem?.ToString();

                    // Call the main method with the obtained channelName
                    await SendMessageToDiscord(message, channelName);
                }
            });
        }

        public async Task SendMessageToDiscord(string message, string channelName)
        {
            await Task.Run(async () =>
            {
                if (_client != null && _client.ConnectionState == ConnectionState.Connected)
                {
                    // Find the guild (server) where the channel is located
                    var guild = _client.Guilds.FirstOrDefault(g => g.Channels.Any(c => c.Name == channelName));

                    if (guild != null)
                    {
                        // Get the text channel with the specified name
                        var textChannel = guild.TextChannels.FirstOrDefault(c => c.Name == channelName) as ISocketMessageChannel;

                        if (textChannel != null)
                        {
                            // Send the message
                            await textChannel.SendMessageAsync(message);
                        }
                        else
                        {
                            Console.WriteLine($"Unable to send message to channel name '{channelName}'. Text channel not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unable to send message to channel name '{channelName}'. Guild not found.");
                    }
                }
                else
                {
                    Console.WriteLine($"Unable to send message to channel name '{channelName}'. Bot is not connected.");
                }
            });
        }




        public async Task HandleMessageAsync(SocketMessage arg)
        {

            var message = arg as SocketUserMessage;
            if (message == null || message.Author == null || message.Author.IsBot)
            {
                // Either the message is null or the author is null or the author is a bot, so we don't process it
                return;
            }
            // Trigger the event with the log message

            string logMessage = $"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")}] {message.Author.Username}: {message.Content}";
            string userfile = @"\UserCFG.ini";
            string botNickname = UserSettings(startupPath + userfile, "BotNickname");

            if (message.MentionedUsers.Any(user => user.Id == _client.CurrentUser.Id) || message.Content.Contains(botNickname, StringComparison.OrdinalIgnoreCase) || message.Content.ToLower().StartsWith("/ask"))
            {
                string query = message.Content.Replace(botNickname, "", StringComparison.OrdinalIgnoreCase).Trim();
                string response = await GetOpenAiResponse(query);
                await message.Channel.SendMessageAsync(response);
                Console.WriteLine("Response sent");

            }
            if (message.Content.ToLower().StartsWith("/roll"))
            {
                // Generate a random number between 1 and 6
                var result = new Random().Next(1, 7);

                // Create an embed response with a custom color
                var embed = new EmbedBuilder
                {
                    Title = "🎲 You rolled",
                    Description = $"{result}",
                    Color = Color.DarkRed
                };

                // Send the embed response to the channel
                await message.Channel.SendMessageAsync(embed: embed.Build());

                // Log to console
                Console.WriteLine("Dice Roll Response sent");
            }
            // EightBall is an array of responses
            string[] EightBallResponses = { "Yes", "No", "Maybe", "Ask again later", "Tf do you think?", "Bitch, you might", "Possibly" };
            // Create an instance of the Random class
            Random rand = new Random();
            if (message.Content.ToLower().StartsWith("/8ball"))
            {
                // Check if the message contains a question
                string question = message.Content.Substring("/8ball".Length).Trim();

                if (!string.IsNullOrEmpty(question))
                {
                    // Get a random response from the EightBallResponses array
                    int randomEightBallMessage = rand.Next(EightBallResponses.Length);
                    string messageToPost = EightBallResponses[randomEightBallMessage];

                    // Respond with the 8-ball answer
                    await message.Channel.SendMessageAsync("```" + messageToPost + "```");
                    Console.WriteLine("8ball Response sent");
                }
                else
                {
                    // If no question is provided, prompt the user to ask a question
                    await message.Channel.SendMessageAsync("```" + "Please ask a question after `/8ball`." + "```");
                }
            }
            if (message.Content.ToLower().StartsWith("/duel"))
            {
                // Extract the mentioned user from the message
                var mentionedUsers = message.MentionedUsers;

                // Ensure there is at least one mentioned user
                if (mentionedUsers.Any())
                {
                    IUser challengedUser = mentionedUsers.First();

                    // Delete the original command message
                    await message.DeleteAsync();

                    // Get the user who invoked the command
                    var challenger = message.Author;

                    // Roll a 6-sided die for each participant
                    int challengerRoll = RollDice();
                    int challengedRoll = RollDice();

                    // Determine the winner based on the higher roll
                    IUser winner = (challengerRoll > challengedRoll) ? challenger : challengedUser;

                    // Create an embed response with the duel results
                    var embed = new EmbedBuilder
                    {
                        Title = "⚔️ Duel Results ⚔️",
                        Description = $"{challenger.Username} challenges {challengedUser.Username} to a duel!\n" +
                                      $"{challenger.Username} rolls a {challengerRoll}\n" +
                                      $"{challengedUser.Username} rolls a {challengedRoll}\n" +
                                      $"The winner is {winner.Mention}!",
                        Color = Color.DarkRed // Custom color (DarkRed)
                    };

                    // Send the embed response to the same channel
                    await message.Channel.SendMessageAsync(embed: embed.Build());

                    // Log to console
                    Console.WriteLine("Duel response sent");
                }
                else
                {
                    // If no mentioned user, inform the user to mention someone
                    await message.Channel.SendMessageAsync("Please mention a user to challenge to a duel.");
                }
            }



            if (message.Content.ToLower().StartsWith("/help"))
            {


                // Create an embed response with a custom color
                var embed = new EmbedBuilder
                {
                    Title = "🤖 List of available Bot Commands 🤖",
                    Description = "/ask: Ask the bot a question." + Environment.NewLine + "/8ball: Ask the magic 8 ball a question." + Environment.NewLine + "/roll: Roll the dice" + Environment.NewLine + "/duel: Mention a user to slappeth in thine face, and challenge to a duel!" + Environment.NewLine + "/coinflip: Flip a coin, Heads or tails." + Environment.NewLine + "/pokemon <Pokemon Name> Get a Pokemons stats " + Environment.NewLine + "/yt Search for a video or song on youtube, and automatically post the first result." + Environment.NewLine + "/lfg: (Game name) <Number of players needed>" + Environment.NewLine + "(This command only works for those in the Streamer Role)" + Environment.NewLine + "/live <YourTwitchUsername> ",
                    Color = Color.DarkRed // Custom color (DarkRed)
                };

                // Send the embed response to the channel
                await message.Channel.SendMessageAsync(embed: embed.Build());

                // Log to console
                Console.WriteLine("Help Response sent");
            }
            if (message.Content.ToLower().StartsWith("/say") && message.Author is SocketGuildUser auth)
            {
                // Check if the author has the "Administrator" permission
                if (auth.GuildPermissions.Administrator)
                {
                    // Extract the message content after "/say"
                    string messageContent = message.Content.Substring("/say".Length).Trim();

                    // Delete the original command message
                    await message.DeleteAsync();

                    // Send the echoed message to the channel
                    await message.Channel.SendMessageAsync(messageContent);

                    // Log to console
                    Console.WriteLine("Say Command Sent");
                }
                else
                {
                    // Inform the user that they don't have the necessary permission
                    await message.Channel.SendMessageAsync("You don't have permission to use this command.");
                }
            }
            if (message.Content.ToLower().StartsWith("/kick") && message.Author is SocketGuildUser author)
            {
                // Check if the author has the "Kick Members" permission
                if (author.GuildPermissions.KickMembers)
                {
                    // Extract the mention from the message
                    var mention = message.MentionedUsers.FirstOrDefault();

                    // Check if a user is mentioned
                    if (mention is SocketGuildUser userToKick)
                    {
                        string displayName = (message.Author as SocketGuildUser)?.DisplayName ?? message.Author.Username;
                        // Check if the mentioned user is a bot
                        await message.DeleteAsync();
                        // Kick the mentioned user
                        await userToKick.KickAsync();
                        // Log to console
                        await message.Channel.SendMessageAsync(displayName + $" Kicked 🦵{mention.Username}#{mention.Discriminator} from the server. D:");
                        Console.WriteLine($"Kicked {mention.Username}#{mention.Discriminator} from the server.");
                    }
                    else
                    {
                        // Inform the user that no user was mentioned
                        await message.Channel.SendMessageAsync("Please mention the user you want to kick.");
                    }
                }
                else
                {
                    // Inform the user that they don't have the necessary permission
                    await message.Channel.SendMessageAsync("You don't have permission to kick members.");
                }
            }
            if (message.Content.ToLower().StartsWith("/softban") && message.Author is SocketGuildUser authorSoftBan)
            {
                // Check if the author has the "Kick Members" or "Administrator" permission
                if (authorSoftBan.GuildPermissions.Administrator || authorSoftBan.GuildPermissions.KickMembers)
                {
                    // Extract the mention from the message
                    var mention = message.MentionedUsers.FirstOrDefault();

                    // Check if a user is mentioned
                    if (mention is SocketGuildUser userToSoftBan)
                    {
                        // Extract the reason from the command message (if provided)
                        string[] commandParts = message.Content.Split(' ');
                        string reason = (commandParts.Length > 2) ? string.Join(' ', commandParts.Skip(2)) : "No reason specified";
                        await message.DeleteAsync();
                        // Ban the mentioned user
                        await userToSoftBan.Guild.AddBanAsync(userToSoftBan, 1, reason);

                        // Unban the user immediately to perform a soft ban
                        await userToSoftBan.Guild.RemoveBanAsync(userToSoftBan);

                        // Log to console
                        await message.Channel.SendMessageAsync($"{authorSoftBan.Mention} Softbanned 🤏{userToSoftBan.Username}#{userToSoftBan.Discriminator} from the server. Reason: {reason}");
                        Console.WriteLine($"{authorSoftBan.Username} softbanned {userToSoftBan.Username}#{userToSoftBan.Discriminator} from the server. Reason: {reason}");
                    }
                    else
                    {
                        // Inform the user that no user was mentioned
                        await message.Channel.SendMessageAsync("Please mention the user you want to softban, optionally add a reason after the username.");
                    }
                }
                else
                {
                    // Inform the user that they don't have the necessary permission
                    await message.Channel.SendMessageAsync("You don't have permission to soft ban members.");
                }
            }

            if (message.Content.ToLower().StartsWith("/ban") && message.Author is SocketGuildUser authorban)
            {
                // Check if the author has the "Ban Members" or "Administrator" permission
                if (authorban.GuildPermissions.Administrator || authorban.GuildPermissions.BanMembers)
                {
                    // Extract the mention from the message
                    var mention = message.MentionedUsers.FirstOrDefault();

                    // Check if a user is mentioned
                    if (mention is SocketGuildUser userToBan)
                    {
                        // Extract the reason from the command message (if provided)
                        string[] commandParts = message.Content.Split(' ');
                        string reason = (commandParts.Length > 2) ? string.Join(' ', commandParts.Skip(2)) : "No reason specified";
                        await message.DeleteAsync();
                        // Ban the mentioned user with the provided reason
                        await userToBan.BanAsync(reason: reason);

                        // Log to console
                        await message.Channel.SendMessageAsync($"{authorban.Mention} banned {userToBan.Username}#{userToBan.Discriminator}🔨 from the server. Reason: {reason}");
                        Console.WriteLine($"{authorban.Username} banned {userToBan.Username}#{userToBan.Discriminator} from the server. Reason: {reason}");
                    }
                    else
                    {
                        // Inform the user that no user was mentioned
                        await message.Channel.SendMessageAsync("Please mention the user you want to ban, optionally add a reason after the username.");
                    }
                }
                else
                {
                    // Inform the user that they don't have the necessary permission
                    await message.Channel.SendMessageAsync("You don't have permission to ban members.");
                }
            }

            if (message.Content.ToLower().StartsWith("/coinflip"))
            {
                // Generate a random result (0 or 1) for heads or tails
                var result = new Random().Next(2);

                // Determine the outcome based on the random result
                string outcome = (result == 0) ? "Heads" : "Tails";

                // Create an embed response with a custom color
                var embed = new EmbedBuilder
                {
                    Title = "🪙 Coin Flip",
                    Description = outcome,
                    Color = Color.DarkRed // You can choose a different color if desired
                };

                // Send the embed response to the channel
                await message.Channel.SendMessageAsync(embed: embed.Build());

                // Log to console
                Console.WriteLine("Coin flip Response sent");
            }
            if (message.Content.ToLower().StartsWith("/lfg"))
            {
                // Use a regular expression to extract the game name inside parentheses
                var match = Regex.Match(message.Content, @"\(([^)]*)\) (\d+)");

                // Check if the regular expression matches
                if (match.Success)
                {
                    // Extract game name and players needed from the match
                    string userDefinedGameName = match.Groups[1].Value.Trim();
                    int userDefinedPlayersNeeded;

                    // Try to parse the number of players needed
                    if (int.TryParse(match.Groups[2].Value, out userDefinedPlayersNeeded))
                    {
                        // Create an embed response with a custom color
                        var embed = new EmbedBuilder
                        {
                            Title = "🎮 Looking For Group 🎮",
                            Description = $"Game: {userDefinedGameName}\nPlayers Needed: {userDefinedPlayersNeeded}\n{message.Author.Mention}" + " is looking for someone to party up!" + Environment.NewLine + "React to this message if you want to play!",
                            Color = Color.DarkRed // You can choose a different color if desired
                        };

                        // Delete the original command message
                        await message.DeleteAsync();

                        // Send the embed response to the channel
                        var lfgMessage = await message.Channel.SendMessageAsync(embed: embed.Build());

                        // Add a thumbs-up reaction to the LFG message by the person who ran the /lfg command
                        await lfgMessage.AddReactionAsync(new Emoji("👍"));

                        // Log to console
                        Console.WriteLine("Looking For Group Response sent");
                    }
                    else
                    {
                        // Handle the case where the provided players needed is not a valid number
                        await message.Channel.SendMessageAsync("Invalid number of players needed. Please use a valid number.");
                    }
                }
                else
                {
                    // Handle the case where the command format is incorrect
                    await message.Channel.SendMessageAsync("Invalid command format. Please use /lfg (game_name) <players_needed>");
                }
            }


            if (message.Content.ToLower().StartsWith("/pokemon"))
            {
                // Extract the Pokémon name from the message content
                string pokemonName = message.Content.Substring("/pokemon".Length).Trim();

                // Construct the URL for the PokemonDB page
                string pokemonDbUrl = $"https://pokemondb.net/pokedex/{pokemonName.ToLower()}";

                // Make a request to the PokemonDB website
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(pokemonDbUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the HTML content
                        string htmlContent = await response.Content.ReadAsStringAsync();

                        // Parse the HTML
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(htmlContent);

                        // Extract information using XPath or other methods
                        // Get Pokemon Types
                        var typesNodes = doc.DocumentNode.SelectNodes("//th[contains(text(), 'Type')]/following-sibling::td//a");
                        string pokemonTypes = string.Join(", ", typesNodes?.Select(node => node.InnerText));

                        // Get Pokemon Capture Rate
                        var captureRateNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Catch rate')]/following-sibling::td");
                        string captureRate = captureRateNode?.InnerText.Trim() ?? "N/A";

                        // Get Pokemon Base Stats
                        var baseStatsNodes = doc.DocumentNode.SelectNodes("//th[contains(text(), 'Base stats')]/following-sibling::td//td[@class='cell-num']");
                        string baseStats = baseStatsNodes != null ? string.Join(", ", baseStatsNodes.Select(node => node.InnerText)) : "N/A";
                        // Get Pokemon Species
                        var speciesNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Species')]/following-sibling::td");
                        string species = speciesNode?.InnerText.Trim() ?? "N/A";
                        // Get National Pokédex number TODO Fix this...
                        var nationalDexNode = doc.DocumentNode.SelectSingleNode("//th[text()='National №']/following-sibling::td");
                        string nationalDexNumber = nationalDexNode?.InnerText.Trim() ?? "N/A";

                        // Define an array of possible image classes in descending order of preference
                        string[] imageClasses = { "img-fixed img-sprite-v21", "img-fixed img-sprite-v8", "img-fixed img-sprite-v3" };

                        // Initialize imageUrl to null
                        string imageUrl = null;

                        // Loop through each image class and try to find the image URL
                        foreach (string imageClass in imageClasses)
                        {
                            var imageNode = doc.DocumentNode.SelectSingleNode($"//img[@class='{imageClass}']");
                            imageUrl = imageNode?.Attributes["src"]?.Value;

                            // If imageUrl is found, break out of the loop
                            if (!string.IsNullOrEmpty(imageUrl))
                            {
                                break;
                            }
                        }

                        // Extract height from the HTML and decode special characters
                        var heightNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Height')]/following-sibling::td");
                        string height = WebUtility.HtmlDecode(heightNode?.InnerText ?? string.Empty);

                        // Extract weight from the HTML and decode special characters
                        var weightNode = doc.DocumentNode.SelectSingleNode("//th[contains(text(), 'Weight')]/following-sibling::td");
                        string weight = WebUtility.HtmlDecode(weightNode?.InnerText ?? string.Empty);



                        // Create an embed response with the extracted information
                        var embed = new EmbedBuilder
                        {
                            Title = $"Pokemon: {pokemonName}",
                            Description = $"National Number: {nationalDexNumber}",
                            Url = pokemonDbUrl, // Set the URL for the title
                            Color = Color.DarkRed, // You can choose a different color if desired
                        };

                        // Add additional details to the embed
                        embed.AddField("Capture Rate", captureRate);
                        embed.AddField("Species", species);
                        embed.AddField("Types", pokemonTypes);
                        embed.AddField("Height", height);
                        embed.AddField("Weight", weight);



                        // Add image URL to the embed, if available
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            embed.ImageUrl = imageUrl;
                        }

                        // Send the embed response to the channel
                        await message.Channel.SendMessageAsync(embed: embed.Build());
                    }
                    else
                    {
                        // Handle the case where the request is not successful
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            // Send intro message to welcome channel, TODO: Make GUI with an Embed builder to customize on user needs
            if (message.Content.ToLower().StartsWith("/intro") && message.Author is SocketGuildUser authorlive)
            {
                string userfile2 = @"\UserCFG.ini";
                string youtubelink = UserSettings(startupPath + userfile2, "Youtube"); // Youtube link for intro message
                string twitch = UserSettings(startupPath + userfile2, "Twitch"); // Twitch link for intro message
                string steam = UserSettings(startupPath + userfile2, "Steam"); // Steam link for intro message
                string facebook = UserSettings(startupPath + userfile2, "Facebook"); // Facebook link for intro message
                string permaInviteUrl = UserSettings(startupPath + userfile2, "InviteLink"); // Invite link for intro message
                // Delete the original command message                                                                            
                await message.DeleteAsync();
                string roleString = UserSettings(startupPath + userfile2, "ModeratorRole");
                string role2String = UserSettings(startupPath + userfile2, "StreamerRole");

                if (ulong.TryParse(roleString, out ulong modrole))
                {
                    // Check if the author has the "Administrator" and your Defined Moderator permission (In CFG File)
                    if (authorlive.GuildPermissions.Administrator || (authorlive.Roles.Any(r => r.Id == modrole)))
                    {
                        string welcome = "https://cdn-longterm.mee6.xyz/plugins/welcome_message/banners/1161640178218049536/welcome.gif";
                        string rules = "https://cdn-longterm.mee6.xyz/plugins/welcome_message/banners/1161640178218049536/rules.png";
                        string links = "https://cdn-longterm.mee6.xyz/plugins/welcome_message/banners/1161640178218049536/links.png";
                        string mods = "https://cdn-longterm.mee6.xyz/plugins/welcome_message/banners/1161640178218049536/mods.png";
                        string invitelink = "https://cdn-longterm.mee6.xyz/plugins/welcome_message/banners/1161640178218049536/invite_link.png";
                        string servername = authorlive.Guild.Name;


                        var embedWelcome = new EmbedBuilder
                        {
                            Title = $"Welcome to {servername}",
                            Description = "Hello and a warm welcome to all of our new members! 👋 We're thrilled to have you join our community ❤️\r\n\r\n\r\n\U0001f91d Please, take some time to introduce yourself to the community – share a bit about yourself, your interests, and what brings you here.\r\n\r\n\r\n📜 Before you dive in, please take a moment to read through our server rules. We want everyone to have a great time here, and following the guidelines helps create a positive environment for everyone.\r\n\r\n\r\nWe're excited to have you here, and we can't wait to get to know you better. Enjoy your stay! 🎉\r\n",
                            Color = Color.DarkRed
                        };


                        var embedRules = new EmbedBuilder
                        {
                            Description = "1. Be Respectful\r\nWe do NOT tolerate negative behavior towards members. Respect one another as we expect you to. We STRONGLY discourage ANY sort of discrimination.\r\n\r\n\r\n2. No Spamming\r\nSpamming is annoying, and makes every sad. Just don't do it.\r\n\r\n\r\n3. Maintain Channel Topics\r\nPlease don't make clutter. Type in the appropriate channels. We want OPTIMAL server organization.\r\n\r\n\r\n4. Religion/Political Views\r\nNo pushing any kind of religious or political views on others in the Discord! Opinions are opinions. Let's leave it at that!\r\n\r\n\r\n5. Have Fun!!\r\nBeing a part of the server is a fun and welcoming experience. Stay true and supportive to one another.\r\n",
                            Color = Color.DarkRed
                        };

                        var embedLinks = new EmbedBuilder
                        {
                            Title = "Social Links",
                            Description = "Check out our social links below, Like, Follow, and Subscribe! Roles given out based on supporter status.",
                            Color = Color.DarkRed
                        };
                        embedLinks.AddField("\u200B", "\u200B", inline: true); // Zero-width space to create a column
                        embedLinks.AddField("YouTube", $"[YouTube Channel]({youtubelink})", inline: true);
                        embedLinks.AddField("Twitch", $"[Twitch Channel]({twitch})", inline: true);
                        embedLinks.AddField("\u200B", "\u200B", inline: true); // Zero-width space to create a column
                        embedLinks.AddField("Steam", $"[Steam Profile]({steam})", inline: true);
                        embedLinks.AddField("Facebook", $"[Facebook Page]({facebook})", inline: true);
                        embedLinks.AddField("\u200B", "\u200B", inline: true); // Zero-width space to create a column
                        var embedMods = new EmbedBuilder
                        {
                            Description = "Our moderation team is here to make sure this cozy little corner of Discord stays warm and safe for everyone. 🍵\r\n\r\n\r\nIf you ever come across any problems related to our server, don't hesitate to tag them in the channel. ❤️\r\n\r\n\r\nQuick heads-up: The moderation team won't handle issues through DMs between members.\r\nIf you run into any trouble there, it's best to report it directly to Discord's Trust & Safety and block the user.\r\n",
                            Color = Color.DarkRed
                        };

                        var embedInviteLink = new EmbedBuilder
                        {
                            Title = @"Spread the love by sharing our server invite link!",
                            Description = $@"```{permaInviteUrl}```",
                            Color = Color.DarkRed
                        };

                        // Combine all the messages and embeds into a single SendMessagesAsync call
                        await message.Channel.SendMessageAsync(welcome);
                        await message.Channel.SendMessageAsync(text: null, isTTS: false, embed: embedWelcome.Build());
                        await message.Channel.SendMessageAsync(rules);
                        await message.Channel.SendMessageAsync(text: null, isTTS: false, embed: embedRules.Build());
                        await message.Channel.SendMessageAsync(links);
                        await message.Channel.SendMessageAsync(text: null, isTTS: false, embed: embedLinks.Build());
                        await message.Channel.SendMessageAsync(mods);
                        await message.Channel.SendMessageAsync(text: null, isTTS: false, embed: embedMods.Build());
                        await message.Channel.SendMessageAsync(invitelink);
                        await message.Channel.SendMessageAsync(text: null, isTTS: false, embed: embedInviteLink.Build());

                        // Log to console
                        Console.WriteLine("Intro Message sent");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Could not convert role ID from string to Ulong!");
                }

            }

            if (message.Content.ToLower().StartsWith("/yt"))
            {
                string userfile2 = @"\UserCFG.ini";
                string youtubeAPIKey = UserSettings(startupPath + userfile2, "YoutubeAPIKey"); // Get Youtube API Key
                string youtubeappname = UserSettings(startupPath + userfile2, "YoutubeAppName"); // Get Youtube Application Name
                string query = message.Content.Substring("/yt".Length).Trim();

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = youtubeAPIKey,
                    ApplicationName = youtubeappname
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = query;
                searchListRequest.MaxResults = 1;

                var searchListResponse = await searchListRequest.ExecuteAsync();

                var searchResult = searchListResponse.Items.FirstOrDefault();

                if (searchResult != null)
                {
                    var videoId = searchResult.Id.VideoId;
                    var videoUrl = $"https://www.youtube.com/watch?v={videoId}";
                    // Delete the original command message
                    await message.DeleteAsync();
                    await message.Channel.SendMessageAsync(message.Author.Mention + " Posted a Youtube Request");
                    await message.Channel.SendMessageAsync(videoUrl);
                    Console.WriteLine("Youtube search Response sent");
                }
                else
                {
                    await message.Channel.SendMessageAsync("No search results found.");
                    Console.WriteLine("No search results found.");
                }
            }

            if (message.Content.ToLower().StartsWith("/pm"))
            {
                // Check if the user invoking the command has the "Administrator" permission
                var isAdmin = (message.Author as IGuildUser)?.GuildPermissions.Administrator ?? false;

                if (isAdmin)
                {
                    // Extract the mentioned user
                    var mentionedUser = message.MentionedUsers.FirstOrDefault();

                    // Check if a user is mentioned
                    if (mentionedUser != null)
                    {
                        // Extract the text after the command
                        string messageContent = message.Content.Substring("/pm".Length).Trim();

                        try
                        {
                            // Delete the original command message
                            await message.DeleteAsync();

                            // Send a private message to the mentioned user with the specified content
                            await mentionedUser.SendMessageAsync(messageContent);
                            Console.WriteLine("PM Command successfully sent");

                        }
                        catch (Exception ex)
                        {
                            // Exceptions, for example, when the mentioned user has direct messages disabled
                            // await message.Channel.SendMessageAsync($"Failed to send a private message to {mentionedUser.Mention}. Error: {ex.Message}");
                            Console.WriteLine("PM Command Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        // If no user is mentioned, inform the user to mention someone
                        await message.Channel.SendMessageAsync("Please mention a user to send a private message.");
                    }
                }
                else
                {
                    // If the user doesn't have the "Administrator" permission, inform them
                    await message.Channel.SendMessageAsync("You don't have permission to use this command.");
                }
            }
            if (message.Content.ToLower().StartsWith("/live") && message.Author is SocketGuildUser authlive)
            {
                // Check if the author has the "Administrator" permission, Mod permissions, or Streamer permissions

                string userfile2 = @"\UserCFG.ini";
                string roleString = UserSettings(startupPath + userfile2, "ModeratorRole");
                string role2String = UserSettings(startupPath + userfile2, "StreamerRole");

                if (ulong.TryParse(roleString, out ulong modrole) && ulong.TryParse(role2String, out ulong streamerrole))
                {
                    if (authlive.GuildPermissions.Administrator || (authlive.Roles.Any(r => r.Id == modrole) || (authlive.Roles.Any(r => r.Id == streamerrole))))
                    {
                        // Split the command to get the Twitch username and game as arguments
                        string[] commandParts = message.Content.Split(' ');

                        // Check if the command has at least two parts ("/live" and the Twitch username)
                        if (commandParts.Length >= 3)
                        {
                            // Get the Twitch username and game from the command
                            string twitchName = commandParts[1];
                            string gameName = string.Join(" ", commandParts.Skip(2));

                            // Get the current date and time
                            DateTime now = DateTime.Now;

                            // Get the display name of the user
                            string displayName = (message.Author as SocketGuildUser)?.DisplayName ?? message.Author.Username;

                            // Format the date and time as MonthName/day/year Time: hour:minute AM/PM
                            string formattedDateTime = now.ToString("MMMM dd,yyyy" + Environment.NewLine + "'Time:' h:mm tt");

                            // Delete the original command message
                            await message.DeleteAsync();

                            // Send the echoed message to the channel
                            await message.Channel.SendMessageAsync($"@everyone {displayName} is now LIVE on Twitch TV playing {gameName}!" + Environment.NewLine +
                                                                  $"Stream Started At: {formattedDateTime}" + Environment.NewLine +
                                                                  $"https://www.twitch.tv/{twitchName}");

                            // Log to console
                            Console.WriteLine("Twitch Update sent");
                        }
                        else
                        {
                            // Inform the user that they need to provide a Twitch username and game name
                            await message.Channel.SendMessageAsync("Please provide a Twitch username and game name. Usage: `/live twitchname gamename`");
                        }
                    }
                    else
                    {
                        // Inform the user that they don't have the necessary permission
                        await message.Channel.SendMessageAsync("You don't have permission to use this command.");
                    }

                }
                else
                {
                    Console.WriteLine("Error: Could not convert role ID from string to Ulong!");
                }
            }

            if (message.Content.ToLower().StartsWith("/purge") && message.Author is SocketGuildUser authPurge)
            {
                // Check if the author has the "Administrator" permission or moderator role
                string userfile2 = @"\UserCFG.ini";
                string roleString = UserSettings(startupPath + userfile2, "ModeratorRole");

                if (ulong.TryParse(roleString, out ulong xForceRole))
                {
                    var isAdmin = (message.Author as SocketGuildUser)?.GuildPermissions.Administrator ?? false;
                    var hasSpecificRole = (message.Author as SocketGuildUser)?.Roles.Any(r => r.Id == xForceRole) ?? false;
                    var isBot = (message.Author as SocketGuildUser)?.IsBot ?? false;
                    if (isAdmin || hasSpecificRole || isBot)
                    {
                        // Split the message content to get the number of messages to purge
                        string[] purgeArgs = message.Content.Split(' ');

                        if (purgeArgs.Length == 2 && int.TryParse(purgeArgs[1], out int messagesToPurge))
                        {
                            // Delete the original command message
                            await message.DeleteAsync();
                            // Get the channel
                            var channel = message.Channel as SocketTextChannel;

                            // Check if the channel is not null
                            if (channel != null)
                            {
                                // Fetch messages and filter out those older than two weeks
                                var messages = await channel.GetMessagesAsync(messagesToPurge).FlattenAsync();
                                var messagesToDelete = messages.Where(m => (DateTimeOffset.Now - m.CreatedAt).TotalDays < 14);
                                Random rnd = new Random();
                                int milliSecondDelay = rnd.Next(1000, 5001);
                                // Delete the filtered messages
                                await channel.DeleteMessagesAsync(messagesToDelete);
                                await Task.Delay(rnd.Next(1000, 5001));  // Add a delay of 1 second between deletions
                                // Inform about the purge
                                await channel.SendMessageAsync($"Purged {messagesToDelete.Count()} messages.");
                                Console.WriteLine($"Successfully Purged {messagesToDelete.Count()} messages.");
                            }
                        }
                        else
                        {
                            // Inform the user about the correct command usage
                            await message.Channel.SendMessageAsync("Please use the command as `/purge <number of messages>`.");
                        }
                    }
                    else
                    {
                        // Inform the user that they don't have the necessary permission
                        await message.Channel.SendMessageAsync("You don't have permission to use this command.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Could not get ModeratorRole ID (ulong) from config file!");
                }
            }



            if (message.Content.ToLower().StartsWith("/updatemsg"))
            {
                string userfile2 = @"\UserCFG.ini";
                string roleString = UserSettings(startupPath + userfile2, "ModeratorRole");
                // Check if the author is an administrator or has Moderator permissions/ID

                if (ulong.TryParse(roleString, out ulong xForceRole))
                {
                    var isAdmin = (message.Author as SocketGuildUser)?.GuildPermissions.Administrator ?? false;

                    var hasSpecificRole = (message.Author as SocketGuildUser)?.Roles.Any(r => r.Id == xForceRole) ?? false;
                    if (isAdmin || hasSpecificRole)
                    {
                        // Extract the text after the command
                        string messageContent = message.Content.Substring("/updatemsg".Length).Trim();
                        string formattedDateTime = DateTime.Now.ToString("MMMM/dd/yyyy" + Environment.NewLine + "'Time:' h:mm tt");
                        // Create an embed response with a custom color
                        var embed = new EmbedBuilder
                        {
                            Title = "📰 Server Update Information 📰" + Environment.NewLine + Environment.NewLine + "Date: " + formattedDateTime,
                            Description = messageContent,
                            Color = Color.DarkRed // You can choose a different color if desired
                        };
                        await message.DeleteAsync();
                        // Send the embed response to the channel
                        await message.Channel.SendMessageAsync(embed: embed.Build());

                        // Log to console
                        Console.WriteLine("Global Message sent");
                    }
                    else
                    {
                        // Inform the user that they don't have the necessary permission
                        await message.Channel.SendMessageAsync("You don't have permission to use this command.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Could not get ModeratorRole ID(ulong) from config file!");
                }


            }
            if (message.Content.ToLower().StartsWith("/poll"))
            {
                // Extract the poll question and options from the message content
                var match = Regex.Match(message.Content, @"/poll\s+(.*)");

                if (match.Success)
                {
                    string pollContent = match.Groups[1].Value.Trim();
                    await message.DeleteAsync();
                    // Create a poll message
                    var pollMessage = await message.Channel.SendMessageAsync($"@everyone **Poll:** {pollContent}\n\nReact with 👍 for Yes, and 👎 for No.");

                    // Add reactions for Yes and No
                    await pollMessage.AddReactionAsync(new Emoji("👍"));
                    await pollMessage.AddReactionAsync(new Emoji("👎"));
                }
                else
                {
                    // Handle the case where the command format is incorrect
                    await message.Channel.SendMessageAsync("Invalid command format. Please use /poll <question>");
                }
            }

            MessageReception?.Invoke($"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")}] {message.Author.Username}: {message.Content}");
            // Update chatLog
            _instance.Invoke(new Action(() =>
            {
                _instance.chatLog.Text += logMessage + Environment.NewLine;
            }));

        }

        private async Task RegisterSlashCommands()
        {
            // Register your slash commands (Not required, easier for users as typing a / will show them all available commands on the server instead of having to type /help)
            //TODO: Port over all existing /commands to register as slash commands
            var commands = new List<SlashCommandBuilder>
    {
        // Your existing commands here

        new SlashCommandBuilder()
            .WithName("ask")
            .WithDescription("Ask the bot a question.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("query")
                .WithDescription("Your question")
                .WithType(ApplicationCommandOptionType.String)),

        new SlashCommandBuilder()
            .WithName("roll")
            .WithDescription("Roll the dice"),

        new SlashCommandBuilder()
            .WithName("8ball")
            .WithDescription("Ask the magic 8 ball a question.")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("question")
                .WithDescription("The question you want to ask")
                .WithType(ApplicationCommandOptionType.String)),

               // Add more commands as needed
};
            string userfile2 = @"\UserCFG.ini";

            string GuildID = UserSettings(startupPath + userfile2, "ServerID"); //Your Server ID


            if (ulong.TryParse(GuildID, out ulong ServerId))
            {

                foreach (var command in commands)
                {
                    var builtCommand = command.Build();
                    await _client.Rest.CreateGuildCommand(builtCommand, ServerId); // Gets ServerID from UserCFG.ini
                }

                Console.WriteLine("Slash commands registered.");
            }
        }

        //Future use, this will allow creation of embedded required files
        static void ExtractResourceToFile(string resourceName, string filePath)
        {
            try
            {
                // Get the embedded resource stream
                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                    {
                        Console.WriteLine($"Error: Resource '{resourceName}' not found.");
                        return;
                    }

                    // Copy the content from the resource stream to the file
                    using (FileStream fileStream = File.Create(filePath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }

                    Console.WriteLine($"Config File Not found, creating file: '{resourceName}' extracted to '{filePath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting resource to file: {ex.Message}");
            }
        }
        //Fix for flickering UI, not sure why O_O
        private async Task UpdateUIAsync()
        {
            try
            {
                _instance.nsListView2?.Invoke(new Action(async () =>
                {
                    if (_instance.nsListView2.IsHandleCreated)
                    {
                        await PopulateListViewWithConnectedUsersAsync();

                    }
                }));

                _instance.nsListView1?.Invoke(new Action(async () =>
                {
                    if (_instance.nsListView1.IsHandleCreated)
                    {
                        await PopulateListViewWithBannedUsers();
                    }
                }));
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine($"Error updating UI: {ex.Message}");
            }
        }



        private async Task UserJoined(SocketGuildUser user)
        {
            // Get all channels in the server
            var channels = user.Guild.Channels;

            // Select the first text channel (you can modify this logic based on your requirements)
            var targetChannel = channels.FirstOrDefault(c => c is ITextChannel) as ITextChannel;
            var rules = channels.FirstOrDefault(c => c is ITextChannel && c.Name.Equals("welcome_channel", StringComparison.OrdinalIgnoreCase)) as ITextChannel;
            // Get the main channel by name (replace "main-channel" with your actual main channel name)
            var welcomeChannel = channels.FirstOrDefault(c => c is ITextChannel && c.Name.Equals("welcome_spam", StringComparison.OrdinalIgnoreCase)) as ITextChannel;
            string userfile2 = @"\UserCFG.ini";
            string roleString = UserSettings(startupPath + userfile2, "AutoRole");

            if (ulong.TryParse(roleString, out ulong xForceRole))
            {
                // Add the X-Force role to the user
                await user.AddRoleAsync(xForceRole);

                Console.WriteLine("AutoRole Successful, user given new role.");
            }
            else
            {
                // Handle the case where the conversion failed
                Console.WriteLine("Error: Could not convert AutoRole to ulong from string, AutoRole Could Not Be Assigned!");
            }




            await welcomeChannel.SendMessageAsync($"HEYO! Welcome to the server {user.Mention}! Be sure to read the Rules in the " + rules.Mention + " !");
            // Invoke the UI update asynchronously
            await UpdateUIAsync();

            Console.WriteLine("Welcome message sent");
        }

        // Check if the user's ID matches the one you want to kick, just for the lawls
        //ulong userIdToKick = USERID;

        //if (user.Id == userIdToKick)
        //{
        //    // Get all channels in the server
        //    var channels = user.Guild.Channels;

        //    // Select the first text channel (you can modify this logic based on your requirements)
        //    var targetChannel = channels.FirstOrDefault(c => c is ITextChannel) as ITextChannel;

        //    if (targetChannel != null)
        //    {
        //        // Kick the user
        //        await user.KickAsync("Auto-kicked based on user ID.");

        //        // Send a message to the selected channel
        //        await targetChannel.SendMessageAsync($"User {user.Username}#{user.Discriminator} ({user.Id}) was auto-kicked. BE GONE VILE MAN, BE GONE WITH YOU! :)");

        //        Console.WriteLine($"User {user.Username}#{user.Discriminator} ({user.Id}) auto-kicked based on user ID.");
        //    }
        //}
        //}


        private int RollDice()
        {
            // Generate a random number between 1 and 6 for a 6-sided die
            return new Random().Next(1, 7);
        }
        public async Task<string> GetOpenAiResponse(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GptApiKey}");

                string prompt = $"User: {query}\nChatGPT:";
                var messages = new List<object>
        {
            new { role = "system", content = $"\"{contentstr}\"" },
            new { role = "user", content = query }
        };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = messages,
                    max_tokens = 200
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(result);
                    var choices = jsonResponse["choices"];
                    var firstChoice = choices.FirstOrDefault();
                    var text = firstChoice?["message"]?["content"]?.ToString();
                    return text ?? "Unable to parse OpenAI response.";
                }
                else
                {
                    Console.WriteLine($"Error communicating with OpenAI API. Status code: {response.StatusCode}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error content: {errorContent}");
                    return "Error communicating with OpenAI API.";
                }
            }
        }
    }
}
