using System.Diagnostics;

namespace Voidbot_Discord_Bot_GUI
{
    public partial class Form1 : Form
    {
        MainProgram botInstance = new MainProgram();  // Create an instance of MainProgram

        public Form1()
        {
            InitializeComponent();


            // Subscribe to the BotDisconnected event
            botInstance.BotDisconnected += OnBotDisconnected;
            botInstance.LogReceived += LogMessageReceived;
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
                label2.ForeColor = Color.Red;
            }));

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
                nsLabel19.Value2 = UserSettings(Application.StartupPath + userfile, "BotNickname");
                personalityInfo.Text = UserSettings(Application.StartupPath + userfile, "BotPersonality");
                GptApiKey.Enabled = false;
                YoutubeAPIKey.Enabled = false;
                YoutubeAppName.Enabled = false;
                DiscordBotToken.Enabled = false;
                Youtube.Enabled = false;
                Twitch.Enabled = false;
                Steam.Enabled = false;
                Facebook.Enabled = false;
                InviteLink.Enabled = false;
                AutoRole.Enabled = false;
                ModeratorRole.Enabled = false;
                StreamerRole.Enabled = false;
                BotNickname.Enabled = false;
                BotPersonality.Enabled = false;
                Invoke(new Action(() =>
                {
                    label2.Text = " Bot Running!";
                    label2.ForeColor = Color.LimeGreen;
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
                nsLabel19.Value2 = " BOT NOT STARTED";
                personalityInfo.Text = "";
                GptApiKey.Enabled = true;
                YoutubeAPIKey.Enabled = true;
                YoutubeAppName.Enabled = true;
                DiscordBotToken.Enabled = true;
                Youtube.Enabled = true;
                Twitch.Enabled = true;
                Steam.Enabled = true;
                Facebook.Enabled = true;
                InviteLink.Enabled = true;
                AutoRole.Enabled = true;
                ModeratorRole.Enabled = true;
                StreamerRole.Enabled = true;
                BotNickname.Enabled = true;
                BotPersonality.Enabled = true;
                Invoke(new Action(() =>
                {
                    label2.Text = " Not Connected...";
                    label2.ForeColor = Color.Red;
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
            if (!File.Exists(userconfigs))
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

        private void nsCheckBox1_CheckedChanged_1(object sender)
        {

        }
    }

}
