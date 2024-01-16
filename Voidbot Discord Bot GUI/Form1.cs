using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using TwitchLib.Communication.Interfaces;
using Voidbot_Discord_Bot_GUI.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            // Subscribe to the BotDisconnected event
            botInstance.BotDisconnected += OnBotDisconnected;
            botInstance.LogReceived += LogMessageReceived;
            botInstance.BotConnected += OnBotConnected;
            
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
            Invoke(new Action(() =>
            {
                label2.Text = message;
                label2.ForeColor = System.Drawing.Color.Red;
            }));

        }
        private async Task Outputcmd()
        {
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
            
            Task.Run(async () =>
            {
                while (true)
                {
                    if (botInstance.DiscordClient != null)
                    {
                        if (botInstance.DiscordClient.ConnectionState == ConnectionState.Connected)
                        {
                            if (label2.InvokeRequired)
                            {
                                label2.Invoke((MethodInvoker)delegate
                                {
                                    label2.Text = " Bot Connected...";
                                });
                            }
                            else
                            {
                                label2.Text = " Bot Connected...";
                            }
                        }
                        else
                        {
                            if (label2.InvokeRequired)
                            {
                                label2.Invoke((MethodInvoker)delegate
                                {
                                    label2.Text = " Not Connected...";
                                });
                            }
                            else
                            {
                                label2.Text = " Not Connected...";
                            }
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

                    // Wait for a short interval before checking again
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
            if (consolebtnSend.InvokeRequired)
            {
                consolebtnSend.Invoke((MethodInvoker)delegate
                {
                    // Check if the bot is connected before enabling the button
                    consolebtnSend.Enabled = botInstance.DiscordClient != null &&
                                              botInstance.DiscordClient.ConnectionState == ConnectionState.Connected;
                });
            }
            else
            {
                // Check if the bot is connected before enabling the button
                consolebtnSend.Enabled = botInstance.DiscordClient != null &&
                                          botInstance.DiscordClient.ConnectionState == ConnectionState.Connected;
            }

                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState != ConnectionState.Connected)
                    {
                        break;
                    }


                    // Wait for a short interval before checking again
                    await Task.Delay(1000);
                }
            });
        }

        private async Task GetBotStatus()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (botInstance.DiscordClient != null)
                    {
                        if (nsLabel22.InvokeRequired)
                        {
                            nsLabel22.Invoke((MethodInvoker)delegate
                            {
                                nsLabel22.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Status;
                            });
                        }
                        else
                        {
                            nsLabel22.Value2 = " " + botInstance.DiscordClient.CurrentUser?.Status;
                        }
                    }

                    // Check if the bot is stopped and exit the loop
                    if (botInstance.DiscordClient == null || botInstance.DiscordClient.ConnectionState == ConnectionState.Disconnected)
                    {
                        break;
                    }

                    // Wait for a short interval before checking again
                    await Task.Delay(2000);
                }
            });
        }
        private async Task OnBotConnected()
        {
       
                botInstance.SetForm1Instance(this); // Pass the instance of Form1 to MainProgram
              await botInstance.PopulateComboBoxWithChannels();
            // Get the bot's avatar URL
            string avatarUrl = botInstance.DiscordClient.CurrentUser.GetAvatarUrl(ImageFormat.Auto, 256);
           
            // Load the avatar into PictureBox1
            await LoadAvatarIntoPictureBox(avatarUrl);
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
                // You can use a library like WebClient or HttpClient to download the image
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

        private async void nsButton2_Click(object sender, EventArgs e)
        {
            string userfile = @"\UserCFG.ini";

            if (!botInstance.isBotRunning)
            {
                nsButton2.Enabled = false;
                nsButton1.Enabled = false;
               
           
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
                Invoke(new Action(() =>
                {
                    label2.Text = " Connecting...";
                    label2.ForeColor = System.Drawing.Color.LimeGreen;
                }));



                // Attempt to connect the bot
                await botInstance.StartBotAsync();

            }


        }

        private async void nsButton3_Click(object sender, EventArgs e)
        {

            if (botInstance.isBotRunning)
            {
                nsButton2.Enabled = true;
                nsButton1.Enabled = true;
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
                // Restart or recreate the botInstance
               

                Invoke(new Action(() =>
                {
                    label2.Text = " Not Connected...";
                    label2.ForeColor = System.Drawing.Color.Red;
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

            // send message to "main-channel" (Change this to the channel you'd like)
            await botInstance.SendMessageToDiscord(messageToSend, channelName);
            commandInputConsoleview.Text = null;
        }

        //private async void nsButton6_Click(object sender, EventArgs e)
        //{
         
        //    botInstance.SetForm1Instance(this); // Pass the instance of Form1 to MainProgram
        //    await botInstance.PopulateComboBoxWithChannels();
        //}
    }

}
//dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --output "C:\Users\Ken\Desktop\TotK Tools Mod Manager" <======================== Publish single file options powershell commands