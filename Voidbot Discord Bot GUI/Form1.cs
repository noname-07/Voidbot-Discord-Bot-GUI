using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using System.Net;
using System.Text;
using Voidbot_Discord_Bot_GUI.Properties;

namespace Voidbot_Discord_Bot_GUI
{
    public partial class Form1 : Form
    {
        private static MainProgram _instance;

        public static MainProgram Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainProgram();
                }
                return _instance;
            }
        }
        MainProgram botInstance = new MainProgram();  // Create an instance of MainProgram
        private bool isFormVisible = true;
        DiscordSocketClient client;

        public Form1()
        {
            InitializeComponent();
            // Access _client through the property
            DiscordSocketClient client = botInstance.DiscordClient;
            // Subscribe to the SelectedIndexChanged event
            nsListView1.SelectedIndexChanged += nsListView1_SelectedIndexChanged;
            nsListView2.SelectedIndexChanged += nsListView2_SelectedIndexChanged;
            // Subscribe to the BotDisconnected event
            botInstance.BotDisconnected += OnBotDisconnected;
            botInstance.LogReceived += LogMessageReceived;
            botInstance.BotConnected += OnBotConnected;
            botInstance.MessageReception += SaveChatLogs;
        }

        public class TextBoxWriter : TextWriter
        {
            private System.Windows.Forms.TextBox textBox;

            public TextBoxWriter(System.Windows.Forms.TextBox textBox)
            {
                this.textBox = textBox;
            }

            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(char value)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.AppendText(value.ToString());
                    }));
                }
                else
                {
                    textBox.AppendText(value.ToString());
                }
            }

        }
        private async void SaveChatLogs(string content)
        {
            string filePath = Path.Combine(Application.StartupPath, "chatlogs.txt");

            Invoke(new Action(async () =>
            {
                try
                {
                    // Write or append the content to the file
                    await File.AppendAllTextAsync(filePath, content + Environment.NewLine);
                    // Console.WriteLine("Chat logs saved to chatlogs.txt.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving chat logs: {ex.Message}");
                }

            }));

        }
        // Method to handle received log messages
        private void LogMessageReceived(string logMessage)
        {
            // Update the TextBox with the log message
            Invoke(new Action(() =>
            {
                botConsoleView.Text += logMessage + Environment.NewLine;

            }));
        }

        private void OnBotDisconnected(string message)
        {


        }
        private async Task Outputcmd()
        {
            //Output Console Logs to OutputCMD Textbox, in seperate thread, avoids cross-thread issues
            Task.Run(async () =>
            {
                while (true)
                {
                    if (botInstance.DiscordClient != null)
                    {
                        if (botConsoleView.InvokeRequired)
                        {
                            botConsoleView.Invoke((MethodInvoker)delegate
                            {
                                TextBoxWriter writer = new TextBoxWriter(botConsoleView);
                                Console.SetOut(writer);
                            });
                        }
                        else
                        {
                            TextBoxWriter writer = new TextBoxWriter(botConsoleView);
                            Console.SetOut(writer);
                        }
                    }

                    // Check if the bot is stopped and exit the loop
                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
                    {
                        break;
                    }

                    // Wait for a short interval before checking again
                    await Task.Delay(1500);
                }
            });

        }
        private async Task Getbotdeets()
        {
            //Get bot connection deets, in seperate thread, avoids cross-thread issues
            Task.Run(async () =>
            {

                while (true)
                {
                    if (botInstance.DiscordClient != null)
                    {
                        ConnectionState currentState = botInstance.DiscordClient.ConnectionState;

                        if (label2.InvokeRequired)
                        {
                            label2.Invoke((MethodInvoker)delegate
                            {
                                UpdateLabel(currentState);
                            });
                        }
                        else
                        {
                            UpdateLabel(currentState);
                        }
                    }

                    // Check if the bot is stopped and exit the loop
                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
                    {
                        break;
                    }

                    // Wait for a short interval before checking again
                    await Task.Delay(3000);
                }
            });
        }
        private void UpdateLabel(ConnectionState currentState)
        {
            string statusMessage = GetConnectionStatusMessage(currentState);

            Invoke(new Action(() =>
            {
                if (currentState == ConnectionState.Connected)
                {
                    label2.Text = statusMessage;
                    label2.ForeColor = System.Drawing.Color.LimeGreen;
                }
                else
                {
                    label2.Text = statusMessage;
                    label2.ForeColor = System.Drawing.Color.Red;
                }
            }));
        }
        private string GetConnectionStatusMessage(ConnectionState connectionState)
        {
            switch (connectionState)
            {
                case ConnectionState.Connected:
                    return "Bot Connected...";
                case ConnectionState.Connecting:
                    return "Connecting...";
                case ConnectionState.Disconnected:
                    return "Not Connected...";
                case ConnectionState.Disconnecting:
                    return "Reconnecting...";
                default:
                    return "Unknown Connection State...";
            }
        }
        //Get bot name, in seperate thread, avoids cross-thread issues
        private async Task GetBotName()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (botInstance.DiscordClient != null)
                    {
                        if (nsLabel20.InvokeRequired)
                        {
                            nsLabel20.Invoke((MethodInvoker)delegate
                            {
                                nsLabel20.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Username;
                            });
                        }
                        else
                        {
                            nsLabel20.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Username;
                        }
                    }

                    // Check if the bot is stopped and exit the loop
                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
                    {
                        break;
                    }

                    // Wait for a short interval before checking again, them Discord Rate Limits
                    await Task.Delay(3000);
                }
            });
        }
        private async Task Activatesend()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (nsButton2.InvokeRequired)
                    {
                        nsButton2.Invoke((MethodInvoker)delegate
                        {
                            // Enable nsButton2 if the bot is disconnected, otherwise disable it
                            nsButton2.Enabled = botInstance.DiscordClient == null ||
                                                botInstance.DiscordClient.ConnectionState != ConnectionState.Connected;
                        });
                    }
                    else
                    {
                        // Enable nsButton2 if the bot is disconnected, otherwise disable it
                        nsButton2.Enabled = botInstance.DiscordClient == null ||
                                            botInstance.DiscordClient.ConnectionState != ConnectionState.Connected;
                    }

                    // Add other buttons to enable/disable on bot connect/disconnect
                    var buttonsToCheck = new[] { consolebtnSend, nsButton3, nsButton7, nsButton8, nsButton9, nsButton10, nsButton11, nsButton12 };

                    foreach (var button in buttonsToCheck)
                    {
                        if (button.InvokeRequired)
                        {
                            button.Invoke((MethodInvoker)delegate
                            {
                                // Check if the bot is connected before enabling/disabling the button
                                button.Enabled = botInstance.DiscordClient != null &&
                                                  botInstance.DiscordClient.ConnectionState == ConnectionState.Connected;
                            });
                        }
                        else
                        {
                            // Check if the bot is connected before enabling/disabling the button
                            button.Enabled = botInstance.DiscordClient != null &&
                                              botInstance.DiscordClient.ConnectionState == ConnectionState.Connected;
                        }
                    }

                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState != ConnectionState.Connected)
                    {
                        break;
                    }

                    // Wait for a short interval before checking again, Discord Rate Limits and all
                    await Task.Delay(3000);
                }
            });
        }



        private async Task GetBotStatus()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    // Check if the bot is stopped and exit the loop
                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
                    {
                        break;
                    }

                    if (nsLabel22.InvokeRequired)
                    {
                        nsLabel22.Invoke((MethodInvoker)delegate
                        {
                            // Check again if DiscordClient is not null before accessing CurrentUser
                            if (botInstance.DiscordClient != null)
                            {
                                nsLabel22.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Status;
                            }
                        });
                    }
                    else
                    {
                        // Check again if DiscordClient is not null before accessing CurrentUser
                        if (botInstance.DiscordClient != null)
                        {
                            nsLabel22.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Status;
                        }
                    }

                    // Wait for a short interval before checking again, else the Discord Rate Limit gets mad
                    await Task.Delay(2000);
                }
            });
        }

        private async Task OnBotConnected()
        {
            botInstance.SetForm1Instance(this); // Pass the instance of Form1 to MainProgram, run initial methods and logic
            await Task.WhenAll(
    botInstance.PopulateListViewWithBannedUsers(),
    botInstance.PopulateListViewWithConnectedUsersAsync(),
    botInstance.PopulateComboBoxWithChannels()
// Other tasks...
);


            await LoadAvatarIntoPictureBox(botInstance.DiscordClient.CurrentUser.GetAvatarUrl(ImageFormat.Auto, 256));
            await Getbotdeets();
            await GetBotName();
            await GetBotStatus();
            await Outputcmd();
            await Activatesend();
        }
        private async Task LoadAvatarIntoPictureBox(string avatarUrl)
        {
            // Load the avatar into PictureBox1
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                // Download the image
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(avatarUrl);

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        System.Drawing.Image avatarImage = System.Drawing.Image.FromStream(ms);
                        pictureBox1.Image = avatarImage;
                    }
                }
            }
        }
        private void nsButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to save your settings?", "VoidBot Discord Bot [GUI]", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var INI2 = new Voidbot_Discord_Bot_GUI.inisettings(); // access to inisettings.cs functions
                INI2.Path = Application.StartupPath + @"\UserCFG.ini"; // defines path of .ini
                INI2.WriteValue("Settings", "GptApiKey", GptApiKey.Text, INI2.GetPath()); // Save the GptApiKey
                INI2.WriteValue("Settings", "YoutubeAPIKey", YoutubeAPIKey.Text, INI2.GetPath()); // Save the YoutubeAPIKey
                INI2.WriteValue("Settings", "YoutubeAppName", YoutubeAppName.Text, INI2.GetPath()); // Save the YoutubeAppName
                INI2.WriteValue("Settings", "DiscordBotToken", DiscordBotToken.Text, INI2.GetPath()); // Save the DiscordBotToken
                INI2.WriteValue("Settings", "Youtube", Youtube.Text, INI2.GetPath()); // Save the Youtube link
                INI2.WriteValue("Settings", "Twitch", Twitch.Text, INI2.GetPath()); // Save the Twitch link
                INI2.WriteValue("Settings", "Steam", Steam.Text, INI2.GetPath()); // Save the Steam link
                INI2.WriteValue("Settings", "Facebook", Facebook.Text, INI2.GetPath()); // Save the Facebook link
                INI2.WriteValue("Settings", "InviteLink", InviteLink.Text, INI2.GetPath()); // Save the InviteLink
                INI2.WriteValue("Settings", "ServerID", ServerID.Text, INI2.GetPath()); // Save the ServerID
                INI2.WriteValue("Settings", "AutoRole", AutoRole.Text, INI2.GetPath()); // Save the AutoRole ID
                INI2.WriteValue("Settings", "ModeratorRole", ModeratorRole.Text, INI2.GetPath()); // Save the ModeratorRole ID
                INI2.WriteValue("Settings", "StreamerRole", StreamerRole.Text, INI2.GetPath()); // Save the StreamerRole ID
                INI2.WriteValue("Settings", "BotNickname", BotNickname.Text, INI2.GetPath()); // Save the BotNickname
                INI2.WriteValue("Settings", "BotPersonality", BotPersonality.Text, INI2.GetPath()); // Save the BotPersonality

            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }
        // start bot method
        private async void nsButton2_Click(object sender, EventArgs e)
        {
            if (!botInstance.isBotRunning)
            {

                Invoke(new Action(() =>
                {
                    label2.Text = " Connecting...";
                    label2.ForeColor = System.Drawing.Color.LimeGreen;
                    nsButton2.Enabled = false;
                    nsButton1.Enabled = false;
                    nsButton2.Enabled = false;

                    GptApiKey.Enabled = false;
                    YoutubeAPIKey.Enabled = false;
                    YoutubeAppName.Enabled = false;
                    DiscordBotToken.Enabled = false;
                    Youtube.Enabled = false;
                    Twitch.Enabled = false;
                    Steam.Enabled = false;
                    Facebook.Enabled = false;
                    InviteLink.Enabled = false;
                    ServerID.Enabled = false;
                    AutoRole.Enabled = false;
                    ModeratorRole.Enabled = false;
                    StreamerRole.Enabled = false;
                    BotNickname.Enabled = false;
                    BotPersonality.Enabled = false;
                }));



                // Attempt to connect the bot
                await botInstance.StartBotAsync();

            }


        }
        // stop bot method
        private async void nsButton3_Click(object sender, EventArgs e)
        {

            if (botInstance.isBotRunning)
            {


                Invoke(new Action(() =>
                {
                    label2.Text = " Disconnected...";
                    label2.ForeColor = System.Drawing.Color.Red;

                    nsButton1.Enabled = true;
                    nsButton3.Enabled = false;
                    pictureBox1.Image = Resources._2451296;
                    nsLabel22.Value2 = " Offline";
                    nsLabel20.Value2 = " ";
                    GptApiKey.Enabled = true;
                    YoutubeAPIKey.Enabled = true;
                    YoutubeAppName.Enabled = true;
                    DiscordBotToken.Enabled = true;
                    Youtube.Enabled = true;
                    Twitch.Enabled = true;
                    Steam.Enabled = true;
                    Facebook.Enabled = true;
                    InviteLink.Enabled = true;
                    ServerID.Enabled = true;
                    AutoRole.Enabled = true;
                    ModeratorRole.Enabled = true;
                    StreamerRole.Enabled = true;
                    BotNickname.Enabled = true;
                    BotPersonality.Enabled = true;

                    // You can stop, or recreate the botInstance


                }));
                // Perform cleanup and stop the bot (add your bot stopping logic here)


                // Clear the NSListView controls on the UI thread
                Invoke(new Action(() =>
                {
                    // Manually remove items from nsListView2
                    if (nsListView2?.IsHandleCreated == true)
                    {
                        nsListView2.BeginInvoke(new Action(() =>
                        {
                            while (nsListView2.Items.Length > 0)
                            {
                                nsListView2.RemoveItemAt(0);
                            }
                            nsLabel29.Value1 = "0"; // Update label to show 0 items
                        }));
                    }

                    // Use RemoveItemAt(0) for nsListView1
                    if (nsListView1?.IsHandleCreated == true)
                    {
                        nsListView1.BeginInvoke(new Action(() =>
                        {
                            while (nsListView1.Items.Length > 0)
                            {
                                nsListView1.RemoveItemAt(0);
                            }
                            nsLabel30.Value1 = "0"; // Update label to show 0 items
                        }));
                    }
                }));

                await botInstance.StopBot();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string userfile;
            userfile = @"\UserCFG.ini";
            string userconfigs;
            userconfigs = Application.StartupPath + @"\UserCFG.ini";
            if (!System.IO.File.Exists(userconfigs))
            {

                MessageBox.Show("UserCFG.ini not found in Application Directory, Creating file...");

                Module1.SaveToDisk("UserCFG.ini", Application.StartupPath + @"\UserCFG.ini");

            }
            var INI2 = new Voidbot_Discord_Bot_GUI.inisettings(); // access to inisettings.cs functions
            INI2.Path = Application.StartupPath + @"\UserCFG.ini"; // defines path of .ini
            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "GptApiKey")))
            {
                GptApiKey.Text = "Input API Key...";
            }
            else
            {
                GptApiKey.Text = UserSettings(Application.StartupPath + userfile, "GptApiKey");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "YoutubeAPIKey")))
            {
                YoutubeAPIKey.Text = "Input API Key...";
            }
            else
            {
                YoutubeAPIKey.Text = UserSettings(Application.StartupPath + userfile, "YoutubeAPIKey");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "YoutubeAppName")))
            {
                YoutubeAppName.Text = "Input Youtube API Keys App Name...";
            }
            else
            {
                YoutubeAppName.Text = UserSettings(Application.StartupPath + userfile, "YoutubeAppName");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "DiscordBotToken")))
            {
                DiscordBotToken.Text = "Input API Key...";
            }
            else
            {

                DiscordBotToken.Text = UserSettings(Application.StartupPath + userfile, "DiscordBotToken");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "Youtube")))
            {
                Youtube.Text = "Input Youtube link...";
            }
            else
            {

                Youtube.Text = UserSettings(Application.StartupPath + userfile, "Youtube");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "Twitch")))
            {
                Twitch.Text = "Input Twitch link...";
            }
            else
            {

                Twitch.Text = UserSettings(Application.StartupPath + userfile, "Twitch");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "Steam")))
            {
                Steam.Text = "Input Steam link...";
            }
            else
            {

                Steam.Text = UserSettings(Application.StartupPath + userfile, "Steam");
            }
            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "Facebook")))
            {
                Facebook.Text = "Input Facebook link...";
            }
            else
            {
                Facebook.Text = UserSettings(Application.StartupPath + userfile, "Facebook");
            }
            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "ServerID")))
            {
                ServerID.Text = "Input Your Server ID";
            }
            else
            {
                ServerID.Text = UserSettings(Application.StartupPath + userfile, "ServerID");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "InviteLink")))
            {
                InviteLink.Text = "Permanent Discord Invite link...";
            }
            else
            {
                InviteLink.Text = UserSettings(Application.StartupPath + userfile, "InviteLink");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "AutoRole")))
            {
                AutoRole.Text = "Input Role ID to give to new members on join...";
            }
            else
            {
                AutoRole.Text = UserSettings(Application.StartupPath + userfile, "AutoRole");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "ModeratorRole")))
            {
                ModeratorRole.Text = "Input Role ID of Moderator Role (For mod commands)...";
            }
            else
            {

                ModeratorRole.Text = UserSettings(Application.StartupPath + userfile, "ModeratorRole");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "StreamerRole")))
            {
                StreamerRole.Text = "Input Role ID of Streamer Role (For /live commands)...";
            }
            else
            {

                StreamerRole.Text = UserSettings(Application.StartupPath + userfile, "StreamerRole");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "BotNickname")))
            {
                BotNickname.Text = "VoidBot";
            }
            else
            {

                BotNickname.Text = UserSettings(Application.StartupPath + userfile, "BotNickname");
            }

            if (string.IsNullOrEmpty(UserSettings(Application.StartupPath + userfile, "BotPersonality")))
            {
                BotPersonality.Text = "You are a helpful assistant, responding with snark and sarcasm.";
            }
            else
            {

                BotPersonality.Text = UserSettings(Application.StartupPath + userfile, "BotPersonality");
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;


                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
                this.Hide();
            }
        }
        public string UserSettings(string File, string Identifier)
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

        private void nsCheckBox1_CheckedChanged(object sender)
        {
            // Toggle password characters based on the checkbox state
            bool showPassword = nsCheckBox1.Checked;

            // Set the UseSystemPasswordChar property for GptApiKey TextBox
            GptApiKey.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for YoutubeAPIKey TextBox
            YoutubeAPIKey.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for YoutubeAppName TextBox
            YoutubeAppName.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for DiscordBotToken TextBox
            DiscordBotToken.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for InviteLink TextBox
            InviteLink.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for AutoRole TextBox
            AutoRole.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for ModeratorRole TextBox
            ModeratorRole.UseSystemPasswordChar = !showPassword;

            // Set the UseSystemPasswordChar property for StreamerRole TextBox
            StreamerRole.UseSystemPasswordChar = !showPassword;
            // Set the UseSystemPasswordChar property for ServerID TextBox
            ServerID.UseSystemPasswordChar = !showPassword;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/V0idpool");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.buymeacoffee.com/voidpool");
        }

        private void nsButton4_Click(object sender, EventArgs e)
        {
            botConsoleView.Text = null;
        }
        private void nsButton5_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
            notifyIcon1.Visible = true;
            this.Hide();
        }



        private void notifyIcon1_MouseEnter(object sender, EventArgs e)
        {

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                this.Show();
                this.WindowState = FormWindowState.Normal;


                notifyIcon1.Visible = false;
            }
        }

        private void openBotPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Show();
            this.WindowState = FormWindowState.Normal;


            notifyIcon1.Visible = false;
        }

        private void closeBotToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void SetNotifyIconTooltip()
        {
            string userfile = @"\UserCFG.ini";
            string botNickname = "VoidBot Discord Bot Running in Tray: " + UserSettings(Application.StartupPath + userfile, "BotNickname");

            string title = string.IsNullOrEmpty(botNickname) ? "VoidBot Discord Bot Running in Tray: Waiting..." : botNickname;

            if (botInstance.DiscordClient != null)
            {
                notifyIcon1.Text = title;
                notifyIcon1.ShowBalloonTip(1000, title, title, ToolTipIcon.Info);
            }
            else
            {
                //do nothing
            }

            // Check if the bot is stopped
            if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
            {
                notifyIcon1.Text = "VoidBot Discord Bot Running in Tray: Waiting...";
                notifyIcon1.ShowBalloonTip(1000, "VoidBot Discord Bot Running in Tray: Waiting...", "VoidBot Discord Bot Running in Tray: Waiting...", ToolTipIcon.Info);

            }


        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {

            SetNotifyIconTooltip();

        }

        private async void consolebtnSend_Click(object sender, EventArgs e)
        {
            // Get the message from the TextBox
            string messageToSend = commandInputConsoleview.Text;
            string channelName = nsComboBox1.Text;

            // send message to selected channel
            await botInstance.SendMessageToDiscord(messageToSend, channelName);
            commandInputConsoleview.Text = null;
        }

        private void nsGroupBox10_Click(object sender, EventArgs e)
        {

        }



        private void nsListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If not on the UI thread, invoke this method on the UI thread
                Invoke(new Action(() => nsListView1_SelectedIndexChanged(sender, e)));
                return;
            }


            if (nsListView1.SelectedItems != null && nsListView1._SelectedItems.Count > 0)
            {
                var selectedItem = nsListView1.SelectedItems[0];

                // Debugging: Output the subitem count to understand the awful structure of NSListView LOL
                // Console.WriteLine($"Subitem count: {selectedItem.SubItems.Count}");

                if (selectedItem.SubItems.Count >= 3)
                {
                    label6.Text = selectedItem.SubItems[0].Text; // username is in the first subitem
                    label7.Text = selectedItem.SubItems[1].Text; // ID is in the second subitem
                    nsTextBox1.Text = selectedItem.SubItems[2].Text; // Reason is in the third subitem
                }
                else
                {
                    // Handle the case where there are not enough subitems
                    label6.Text = "No username available";
                    label7.Text = "N/A";
                    nsTextBox1.Text = "No ban reason found.";
                }
            }
            else
            {
                // Handle the case where no item is selected
                label6.Text = "No item selected";
                label7.Text = "N/A";
                nsTextBox1.Text = "";
            }
        }

        private void nsListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If not on the UI thread, invoke this method on the UI thread
                Invoke(new Action(() => nsListView2_SelectedIndexChanged(sender, e)));
                return;
            }

            //Console.WriteLine($"Subitem Count: {nsListView2._Items.Count}");
            if (nsListView2.SelectedItems != null && nsListView2._SelectedItems.Count > 0)
            {
                var selectedItem = nsListView2.SelectedItems[0];

                // Debugging: Output the subitem count to understand the awful structure of NSListView LOL
                //Console.WriteLine($"Subitem 2 count: {selectedItem.SubItems.Count}");

                if (selectedItem.SubItems.Count >= 2)
                {
                    label12.Text = selectedItem.SubItems[0].Text; // username is in the first subitem
                    label11.Text = selectedItem.SubItems[1].Text; // userID is in the second subitem
                }
                else
                {
                    // Handle the case where there are not enough subitems
                    label12.Text = "N/A";
                    label11.Text = "N/A";

                }
            }
            else
            {
                // Handle the case where no item is selected
                label12.Text = "N/A";
                label11.Text = "N/A";

            }
        }

        private void nsButton6_Click(object sender, EventArgs e)
        {
            string folderPath = Path.GetDirectoryName(Path.Combine(Application.StartupPath, "chatlogs.txt"));

            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show(folderPath + " Not found, chatlogs.txt should be created on run. Please report bug (chatlogs creation error)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void nsButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (nsListView1.SelectedItems != null && nsListView1._SelectedItems.Count > 0)
                {
                    var selectedItem = nsListView1.SelectedItems[0];

                    if (ulong.TryParse(label7.Text, out ulong userId))
                    {
                        string userfile2 = @"\UserCFG.ini";
                        string GuildIDString = UserSettings(Application.StartupPath + userfile2, "ServerID");

                        if (ulong.TryParse(GuildIDString, out ulong guildId))
                        {
                            var guild = botInstance.DiscordClient.GetGuild(guildId);

                            if (guild != null)
                            {
                                var ban = await guild.GetBanAsync(userId);

                                if (ban != null)
                                {
                                    Console.WriteLine($"Removing ban for user with ID: {userId}");
                                    chatLog.AppendText($"Removing ban for user with ID: {userId}");
                                    await guild.RemoveBanAsync(userId);
                                    RemoveFromGlobalBans(label6.Text, label7.Text);

                                    // Invoke UI updates for nsListView1
                                    nsListView1.BeginInvoke(new Action(() =>
                                    {
                                        nsListView1._Items.Remove(selectedItem);
                                        nsListView1.InvalidateLayout();
                                    }));

                                    // Wait for to finish updating before updating other UI elements
                                    await Task.Delay(500);

                                    // Perform UI updates for other UI elements (if needed)
                                    Invoke(new Action(() =>
                                    {
                                        // Update other UI elements here, e.g., labels
                                        label6.Text = "";
                                        label7.Text = "";
                                    }));

                                    //// Perform other tasks on the UI thread
                                    //await Task.WhenAll(
                                    //    botInstance.PopulateComboBoxWithChannels()
                                    //// Other UI-related tasks...
                                    //);


                                    //// Wait for nsListView2 to finish updating before updating other UI elements
                                    //await Task.Delay(500);

                                    //// Invoke UI updates for other UI elements (if needed)
                                    //Invoke(new Action(() =>
                                    //{
                                    //    // Update other UI elements here, if necessary
                                    //}));

                                    //// Perform other tasks on the UI thread (if needed)
                                    //await Task.WhenAll(
                                    //// Other UI-related tasks...
                                    //);
                                }
                                else
                                {
                                    Console.WriteLine($"User with ID {userId} is not banned in the guild.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ServerID provided.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ServerID format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No User selected");
                    }
                }
                else
                {
                    MessageBox.Show("No user selected. Please select a user to unban.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Ban CMD: {ex.Message}");
            }
        }



        private void RemoveFromGlobalBans(string username, string userId)
        {
            try
            {
                // Specify the path to bans_global.txt
                string filePath = "bans_global.txt";

                // Read all lines from the file
                var lines = File.ReadAllLines(filePath).ToList();

                // Find and remove the entry with the matching username and userID
                string entryToRemove = $"Username: {username} | UserID: {userId}";
                lines.Remove(entryToRemove);

                // Write the modified content back to the file
                File.WriteAllLines(filePath, lines);

                // Optionally, provide feedback or perform additional actions
                Console.WriteLine($"Entry removed from {filePath}");
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                Console.WriteLine($"Error removing entry from bans_global.txt: {ex.Message}");
            }
        }
        private void SaveGlobalBanList(string content)
        {
            try
            {
                // Specify the path to bans_global.txt
                string filePath = "bans_global.txt";

                // Check if the content already exists in the file
                if (!File.Exists(filePath) || !File.ReadLines(filePath).Contains(content))
                {
                    // Append the content to the file
                    System.IO.File.AppendAllText(filePath, content + Environment.NewLine);

                    // provide feedback or perform additional actions
                    Console.WriteLine($"Content saved to {filePath}");
                }
                else
                {
                    // provide feedback that the content already exists
                    Console.WriteLine($"Content already exists in {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                Console.WriteLine($"Error saving to text file: {ex.Message}");
            }
        }
        private void nsButton8_Click(object sender, EventArgs e)
        {
            if (nsListView1.SelectedItems != null && nsListView1._SelectedItems.Count > 0)
            {
                // Your code here
                SaveGlobalBanList($"Username: {label6.Text} | UserID: {label7.Text}");

                // Check if chatLog already has text and add a newline accordingly
                if (!string.IsNullOrEmpty(chatLog.Text))
                {
                    chatLog.AppendText(Environment.NewLine);
                }

                chatLog.AppendText($"Saved Ban Entry [Username: {label6.Text} | UserID: {label7.Text}]");
            }
            else
            {
                // Check if chatLog already has text and add a newline accordingly
                if (!string.IsNullOrEmpty(chatLog.Text))
                {
                    chatLog.AppendText(Environment.NewLine);
                }
                chatLog.AppendText("[SYSTEM MESSAGE] No user selected, Please select a user to add to the Global Bans list.");
            }
        }


        private async void nsButton10_Click(object sender, EventArgs e)
        {
            // Check if a user is selected
            if (nsListView2.SelectedItems != null && nsListView2._SelectedItems.Count > 0)
            {
                var selectedItem = nsListView2.SelectedItems[0];

                // Get the user ID from label11.Text
                if (ulong.TryParse(label11.Text, out ulong userId))
                {
                    // Convert string to ulong for guild ID
                    string userfile2 = @"\UserCFG.ini";
                    string GuildIDString = UserSettings(Application.StartupPath + userfile2, "ServerID");

                    if (ulong.TryParse(GuildIDString, out ulong guildId))
                    {
                        var guild = botInstance.DiscordClient.GetGuild(guildId);

                        if (guild != null)
                        {
                            // Get banned users list
                            var ban = await guild.GetBanAsync(userId);

                            if (ban == null)
                            {
                                // Ask for confirmation before kicking
                                DialogResult result = MessageBox.Show($"Are you sure you want to kick user {userId}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    // User confirmed, proceed with kicking
                                    Console.WriteLine($"Bot Kicked 🦵: {userId}" + " from the server. D:");
                                    chatLog.AppendText($"Bot Kicked 🦵: {userId}" + " from the server. D:");
                                    await guild.AddBanAsync(userId, 0, label11.Text);
                                    await guild.RemoveBanAsync(userId);

                                    // Invoke UI updates for other UI elements
                                    Invoke(new Action(() =>
                                    {
                                        // Update other UI elements here, e.g., labels
                                        label6.Text = "";
                                        label7.Text = "";
                                    }));

                                    nsListView2.BeginInvoke(new Action(() =>
                                    {
                                        nsListView2._Items.Remove(selectedItem);
                                        nsListView2.InvalidateLayout();
                                    }));
                                }
                                // If 'No' is pressed, do nothing
                            }
                            else
                            {
                                // User is already banned, do nothing
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid guild ID provided.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ServerID format.");
                    }
                }
                else
                {
                    Console.WriteLine("No user selected. Please select a user to kick");
                }
            }
            else
            {
                // Handle the case where no user is selected
                MessageBox.Show("No user selected. Please select a user to kick.");
            }
        }



        private async Task<ulong> GetChannelIdByName(string channelName)
        {

            var guild = (botInstance.DiscordClient as DiscordSocketClient)?.Guilds.FirstOrDefault();

            if (guild != null)
            {
                // Fetch all channels in the guild
                var channels = guild.Channels;

                // Find the channel by name
                var channel = channels.FirstOrDefault(c => c.Name == channelName);

                // Return the channel ID if found
                return channel?.Id ?? 0;
            }

            return 0;
        }
        private void nsButton11_Click(object sender, EventArgs e)
        {
            // Get the selected channel name
            string selectedChannelName = nsComboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedChannelName))
            {
                // Get the selected number of messages to purge from the dropdown box
                if (int.TryParse(nsComboBox2.Text?.ToString(), out int messagesToPurge))
                {
                    // Call the method to handle the purge for the selected channel with the specified message count
                    HandlePurgeForChannel(selectedChannelName, messagesToPurge);
                }
                else
                {
                    // Inform the user that the selected message count is not valid
                    MessageBox.Show("Please select a valid number of messages to purge.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Inform the user that no channel is selected
                MessageBox.Show("Please select a channel from the dropdown box.", "Channel Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task HandlePurgeForChannel(string channelName, int messagesToPurge)
        {
            // Get the channel ID by name
            ulong channelId = await GetChannelIdByName(channelName);

            if (channelId != 0)
            {
                // Call the method to handle the purge for the selected channel ID with the specified message count
                await HandlePurgeForChannel(channelId, messagesToPurge);
            }
            else
            {
                // Inform the user that the channel name is not found
                MessageBox.Show($"Channel ID not found for the selected channel name: {channelName}", "Channel ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task HandlePurgeForChannel(ulong channelId, int messagesToPurge)
        {
            // Get the channel
            var channel = botInstance.DiscordClient.GetChannel(channelId) as ISocketMessageChannel;

            // Check if the channel is not null
            if (channel != null)
            {
                // Fetch messages and filter out those older than two months
                var messages = await channel.GetMessagesAsync(messagesToPurge).FlattenAsync();
                var messagesToDelete = messages.Where(m => (DateTimeOffset.Now - m.CreatedAt).TotalDays < 60);

                // Delete the filtered messages
                await (channel as ITextChannel)?.DeleteMessagesAsync(messagesToDelete);

                // Inform about the purge
                await channel.SendMessageAsync($"Purged {messagesToDelete.Count()} messages.");
                Console.WriteLine($"Successfully purged {messagesToDelete.Count()} messages.");
            }
        }

        private async void nsButton9_Click(object sender, EventArgs e)
        {
            // Check if any item is selected in the nsListView1

            if (nsListView2.SelectedItems != null && nsListView2._SelectedItems.Count > 0)
            {
                var selectedItem = nsListView2.SelectedItems[0];

                // Get the user ID from label11.Text
                if (ulong.TryParse(label11.Text, out ulong userId))
                {
                    // Convert string to ulong for guild ID
                    string userfile2 = @"\UserCFG.ini";
                    string GuildIDString = UserSettings(Application.StartupPath + userfile2, "ServerID");

                    if (ulong.TryParse(GuildIDString, out ulong guildId))
                    {
                        var guild = botInstance.DiscordClient.GetGuild(guildId);

                        if (guild != null)
                        {
                            // Get information about the ban
                            var ban = await guild.GetBanAsync(userId);

                            if (ban == null)
                            {
                                // User is not banned, proceed with banning
                                Console.WriteLine($"Bot banned: " + label12.Text + "🔨 from the server. Reason: " + nsTextBox2.Text);
                                chatLog.AppendText($"Bot banned: " + label12.Text + "🔨 from the server. Reason: " + nsTextBox2.Text);
                                await guild.AddBanAsync(userId, 7, nsTextBox2.Text);
                                // Add entry to bans_global.txt
                                SaveGlobalBanList($"Username: {label12.Text} | UserID: {label11.Text}");


                                nsListView2.BeginInvoke(new Action(() =>
                                {
                                    nsListView2._Items.Remove(selectedItem);


                                    nsListView2.InvalidateLayout();

                                }));
                            }
                            else
                            {
                                // User is already banned
                                Console.WriteLine($"User with ID {userId} is already banned in the guild.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ServerID provided.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ServerID format.");
                    }
                }
                else
                {
                    Console.WriteLine("No User selected, please select a user.");
                }
            }
            else
            {
                // Handle the case where no item is selected
                MessageBox.Show("No user selected. Please select a user to ban.");
            }


        }

        private async void nsButton12_Click(object sender, EventArgs e)
        {


            if (nsListView2.SelectedItems != null && nsListView2._SelectedItems.Count > 0)
            {
                var selectedItem = nsListView2.SelectedItems[0];

                // Get the user ID from label11.Text
                if (ulong.TryParse(label11.Text, out ulong userId))
                {
                    // Convert string to ulong for guild ID
                    string userfile2 = @"\UserCFG.ini";
                    string GuildIDString = UserSettings(Application.StartupPath + userfile2, "ServerID");

                    if (ulong.TryParse(GuildIDString, out ulong guildId))
                    {
                        var guild = botInstance.DiscordClient.GetGuild(guildId);

                        if (guild != null)
                        {
                            // Get banned users list
                            var ban = await guild.GetBanAsync(userId);

                            if (ban == null)
                            {
                                // User is not banned, proceed with kicking
                                Console.WriteLine($"Bot Softbanned and Pruned 🦵: {userId}" + " from the server. D:");
                                chatLog.AppendText($"Bot Softbanned and Pruned 🦵: {userId}" + " from the server. D:");
                                // Optionally send the message to the server
                                //await botInstance.SendMessageToDiscord($"Bot Softbanned and Pruned 🦵: {userId}" + " from the server. D:");
                                await guild.AddBanAsync(userId, 0, label11.Text);

                                await guild.RemoveBanAsync(userId);
                                // Invoke UI updates for other UI elements
                                Invoke(new Action(() =>
                                {
                                    // Update other UI elements here, e.g., labels
                                    label6.Text = "";
                                    label7.Text = "";
                                }));


                                nsListView2.BeginInvoke(new Action(() =>
                                {
                                    nsListView2._Items.Remove(selectedItem);


                                    nsListView2.InvalidateLayout();


                                }));
                            }
                            else
                            {
                                //do nothing, they're just being kicked
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid guild ID provided.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ServerID format.");
                    }
                }
                else
                {
                    Console.WriteLine("No user selected, Please select a user to kick");
                }
            }
            else
            {
                // Handle the case where no user is selected
                MessageBox.Show("No user selected. Please select a user to kick.");
            }

        }

    }
}


