namespace Voidbot_Discord_Bot_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            nsTabControl1 = new NSTabControl();
            tabPage1 = new TabPage();
            nsGroupBox3 = new NSGroupBox();
            ServerID = new NSTextBox();
            nsLabel23 = new NSLabel();
            nsLabel17 = new NSLabel();
            nsLabel16 = new NSLabel();
            nsLabel13 = new NSLabel();
            BotPersonality = new NSTextBox();
            nsLabel12 = new NSLabel();
            BotNickname = new NSTextBox();
            nsLabel11 = new NSLabel();
            nsLabel14 = new NSLabel();
            StreamerRole = new NSTextBox();
            nsLabel15 = new NSLabel();
            InviteLink = new NSTextBox();
            ModeratorRole = new NSTextBox();
            AutoRole = new NSTextBox();
            nsGroupBox2 = new NSGroupBox();
            nsLabel10 = new NSLabel();
            nsLabel9 = new NSLabel();
            nsLabel8 = new NSLabel();
            nsLabel7 = new NSLabel();
            nsLabel5 = new NSLabel();
            Youtube = new NSTextBox();
            Twitch = new NSTextBox();
            Facebook = new NSTextBox();
            Steam = new NSTextBox();
            nsGroupBox1 = new NSGroupBox();
            nsCheckBox1 = new NSCheckBox();
            nsLabel6 = new NSLabel();
            GptApiKey = new NSTextBox();
            YoutubeAPIKey = new NSTextBox();
            nsLabel4 = new NSLabel();
            YoutubeAppName = new NSTextBox();
            nsLabel3 = new NSLabel();
            DiscordBotToken = new NSTextBox();
            nsLabel2 = new NSLabel();
            nsLabel1 = new NSLabel();
            nsGroupBox4 = new NSGroupBox();
            pictureBox1 = new PictureBox();
            nsLabel19 = new NSLabel();
            nsLabel20 = new NSLabel();
            nsLabel22 = new NSLabel();
            nsGroupBox5 = new NSGroupBox();
            nsButton5 = new NSButton();
            label2 = new Label();
            label1 = new Label();
            nsGroupBox6 = new NSGroupBox();
            nsButton3 = new NSButton();
            nsButton1 = new NSButton();
            nsButton2 = new NSButton();
            nsGroupBox8 = new NSGroupBox();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            tabPage2 = new TabPage();
            botConsoleView = new TextBox();
            nsGroupBox7 = new NSGroupBox();
            nsButton4 = new NSButton();
            nsLabel18 = new NSLabel();
            nsGroupBox9 = new NSGroupBox();
            nsComboBox1 = new NSComboBox();
            consolebtnSend = new NSButton();
            commandInputConsoleview = new NSTextBox();
            nsLabel21 = new NSLabel();
            notifyIcon1 = new NotifyIcon(components);
            nsContextMenu1 = new NSContextMenu();
            openBotPanelToolStripMenuItem = new ToolStripMenuItem();
            closeBotToolStripMenuItem = new ToolStripMenuItem();
            nsTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            nsGroupBox3.SuspendLayout();
            nsGroupBox2.SuspendLayout();
            nsGroupBox1.SuspendLayout();
            nsGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            nsGroupBox5.SuspendLayout();
            nsGroupBox6.SuspendLayout();
            nsGroupBox8.SuspendLayout();
            tabPage2.SuspendLayout();
            nsGroupBox7.SuspendLayout();
            nsGroupBox9.SuspendLayout();
            nsContextMenu1.SuspendLayout();
            SuspendLayout();
            // 
            // nsTabControl1
            // 
            nsTabControl1.Alignment = TabAlignment.Left;
            nsTabControl1.Controls.Add(tabPage1);
            nsTabControl1.Controls.Add(tabPage2);
            nsTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            nsTabControl1.ItemSize = new Size(28, 115);
            nsTabControl1.Location = new Point(1, -3);
            nsTabControl1.Multiline = true;
            nsTabControl1.Name = "nsTabControl1";
            nsTabControl1.SelectedIndex = 0;
            nsTabControl1.Size = new Size(1156, 491);
            nsTabControl1.SizeMode = TabSizeMode.Fixed;
            nsTabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(50, 50, 50);
            tabPage1.Controls.Add(nsGroupBox3);
            tabPage1.Controls.Add(nsGroupBox2);
            tabPage1.Controls.Add(nsGroupBox1);
            tabPage1.Controls.Add(nsGroupBox4);
            tabPage1.Controls.Add(nsGroupBox5);
            tabPage1.Controls.Add(nsGroupBox6);
            tabPage1.Controls.Add(nsGroupBox8);
            tabPage1.Location = new Point(119, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1033, 483);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Bot Main Page";
            // 
            // nsGroupBox3
            // 
            nsGroupBox3.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox3.Controls.Add(ServerID);
            nsGroupBox3.Controls.Add(nsLabel23);
            nsGroupBox3.Controls.Add(nsLabel17);
            nsGroupBox3.Controls.Add(nsLabel16);
            nsGroupBox3.Controls.Add(nsLabel13);
            nsGroupBox3.Controls.Add(BotPersonality);
            nsGroupBox3.Controls.Add(nsLabel12);
            nsGroupBox3.Controls.Add(BotNickname);
            nsGroupBox3.Controls.Add(nsLabel11);
            nsGroupBox3.Controls.Add(nsLabel14);
            nsGroupBox3.Controls.Add(StreamerRole);
            nsGroupBox3.Controls.Add(nsLabel15);
            nsGroupBox3.Controls.Add(InviteLink);
            nsGroupBox3.Controls.Add(ModeratorRole);
            nsGroupBox3.Controls.Add(AutoRole);
            nsGroupBox3.DrawSeperator = false;
            nsGroupBox3.Location = new Point(545, 99);
            nsGroupBox3.Name = "nsGroupBox3";
            nsGroupBox3.Size = new Size(486, 270);
            nsGroupBox3.SubTitle = "";
            nsGroupBox3.TabIndex = 35;
            nsGroupBox3.Text = "nsGroupBox3";
            nsGroupBox3.Title = "";
            // 
            // ServerID
            // 
            ServerID.Location = new Point(157, 34);
            ServerID.MaxLength = 32767;
            ServerID.Multiline = false;
            ServerID.Name = "ServerID";
            ServerID.ReadOnly = false;
            ServerID.Size = new Size(313, 23);
            ServerID.TabIndex = 27;
            ServerID.TextAlign = HorizontalAlignment.Left;
            ServerID.UseSystemPasswordChar = true;
            // 
            // nsLabel23
            // 
            nsLabel23.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel23.Location = new Point(6, 34);
            nsLabel23.Name = "nsLabel23";
            nsLabel23.Size = new Size(123, 23);
            nsLabel23.TabIndex = 28;
            nsLabel23.Text = "nsLabel23";
            nsLabel23.Value1 = "Your Server ID: ";
            nsLabel23.Value2 = " ";
            // 
            // nsLabel17
            // 
            nsLabel17.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel17.Location = new Point(4, 208);
            nsLabel17.Name = "nsLabel17";
            nsLabel17.Size = new Size(123, 23);
            nsLabel17.TabIndex = 26;
            nsLabel17.Text = "nsLabel17";
            nsLabel17.Value1 = "Bot Personality: ";
            nsLabel17.Value2 = " ";
            // 
            // nsLabel16
            // 
            nsLabel16.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel16.Location = new Point(4, 179);
            nsLabel16.Name = "nsLabel16";
            nsLabel16.Size = new Size(87, 23);
            nsLabel16.TabIndex = 25;
            nsLabel16.Text = "nsLabel16";
            nsLabel16.Value1 = "Bot Name: ";
            nsLabel16.Value2 = " ";
            // 
            // nsLabel13
            // 
            nsLabel13.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel13.Location = new Point(4, 150);
            nsLabel13.Name = "nsLabel13";
            nsLabel13.Size = new Size(140, 23);
            nsLabel13.TabIndex = 24;
            nsLabel13.Text = "nsLabel13";
            nsLabel13.Value1 = "Streamer Role ID:";
            nsLabel13.Value2 = " ";
            // 
            // BotPersonality
            // 
            BotPersonality.Location = new Point(157, 208);
            BotPersonality.MaxLength = 32767;
            BotPersonality.Multiline = true;
            BotPersonality.Name = "BotPersonality";
            BotPersonality.ReadOnly = false;
            BotPersonality.Size = new Size(313, 47);
            BotPersonality.TabIndex = 15;
            BotPersonality.TextAlign = HorizontalAlignment.Left;
            BotPersonality.UseSystemPasswordChar = false;
            // 
            // nsLabel12
            // 
            nsLabel12.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel12.Location = new Point(4, 121);
            nsLabel12.Name = "nsLabel12";
            nsLabel12.Size = new Size(140, 23);
            nsLabel12.TabIndex = 23;
            nsLabel12.Text = "nsLabel12";
            nsLabel12.Value1 = "Moderator Role ID:";
            nsLabel12.Value2 = " ";
            // 
            // BotNickname
            // 
            BotNickname.Location = new Point(157, 179);
            BotNickname.MaxLength = 32767;
            BotNickname.Multiline = false;
            BotNickname.Name = "BotNickname";
            BotNickname.ReadOnly = false;
            BotNickname.Size = new Size(313, 23);
            BotNickname.TabIndex = 14;
            BotNickname.TextAlign = HorizontalAlignment.Left;
            BotNickname.UseSystemPasswordChar = false;
            // 
            // nsLabel11
            // 
            nsLabel11.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel11.Location = new Point(4, 92);
            nsLabel11.Name = "nsLabel11";
            nsLabel11.Size = new Size(103, 23);
            nsLabel11.TabIndex = 22;
            nsLabel11.Text = "nsLabel11";
            nsLabel11.Value1 = "AutoRole ID:";
            nsLabel11.Value2 = " ";
            // 
            // nsLabel14
            // 
            nsLabel14.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel14.Location = new Point(4, 3);
            nsLabel14.Name = "nsLabel14";
            nsLabel14.Size = new Size(161, 23);
            nsLabel14.TabIndex = 21;
            nsLabel14.Text = "nsLabel14";
            nsLabel14.Value1 = "Bot";
            nsLabel14.Value2 = " Discord Settings";
            // 
            // StreamerRole
            // 
            StreamerRole.Location = new Point(157, 150);
            StreamerRole.MaxLength = 32767;
            StreamerRole.Multiline = false;
            StreamerRole.Name = "StreamerRole";
            StreamerRole.ReadOnly = false;
            StreamerRole.Size = new Size(313, 23);
            StreamerRole.TabIndex = 13;
            StreamerRole.TextAlign = HorizontalAlignment.Left;
            StreamerRole.UseSystemPasswordChar = true;
            // 
            // nsLabel15
            // 
            nsLabel15.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel15.Location = new Point(4, 63);
            nsLabel15.Name = "nsLabel15";
            nsLabel15.Size = new Size(123, 23);
            nsLabel15.TabIndex = 20;
            nsLabel15.Text = "nsLabel15";
            nsLabel15.Value1 = "Permanent Link:";
            nsLabel15.Value2 = " ";
            // 
            // InviteLink
            // 
            InviteLink.Location = new Point(157, 63);
            InviteLink.MaxLength = 32767;
            InviteLink.Multiline = false;
            InviteLink.Name = "InviteLink";
            InviteLink.ReadOnly = false;
            InviteLink.Size = new Size(313, 23);
            InviteLink.TabIndex = 10;
            InviteLink.TextAlign = HorizontalAlignment.Left;
            InviteLink.UseSystemPasswordChar = true;
            // 
            // ModeratorRole
            // 
            ModeratorRole.Location = new Point(157, 121);
            ModeratorRole.MaxLength = 32767;
            ModeratorRole.Multiline = false;
            ModeratorRole.Name = "ModeratorRole";
            ModeratorRole.ReadOnly = false;
            ModeratorRole.Size = new Size(313, 23);
            ModeratorRole.TabIndex = 12;
            ModeratorRole.TextAlign = HorizontalAlignment.Left;
            ModeratorRole.UseSystemPasswordChar = true;
            // 
            // AutoRole
            // 
            AutoRole.Location = new Point(157, 92);
            AutoRole.MaxLength = 32767;
            AutoRole.Multiline = false;
            AutoRole.Name = "AutoRole";
            AutoRole.ReadOnly = false;
            AutoRole.Size = new Size(313, 23);
            AutoRole.TabIndex = 11;
            AutoRole.TextAlign = HorizontalAlignment.Left;
            AutoRole.UseSystemPasswordChar = true;
            // 
            // nsGroupBox2
            // 
            nsGroupBox2.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox2.Controls.Add(nsLabel10);
            nsGroupBox2.Controls.Add(nsLabel9);
            nsGroupBox2.Controls.Add(nsLabel8);
            nsGroupBox2.Controls.Add(nsLabel7);
            nsGroupBox2.Controls.Add(nsLabel5);
            nsGroupBox2.Controls.Add(Youtube);
            nsGroupBox2.Controls.Add(Twitch);
            nsGroupBox2.Controls.Add(Facebook);
            nsGroupBox2.Controls.Add(Steam);
            nsGroupBox2.DrawSeperator = false;
            nsGroupBox2.Location = new Point(3, 287);
            nsGroupBox2.Name = "nsGroupBox2";
            nsGroupBox2.Size = new Size(539, 173);
            nsGroupBox2.SubTitle = "";
            nsGroupBox2.TabIndex = 34;
            nsGroupBox2.Text = "nsGroupBox2";
            nsGroupBox2.Title = "";
            // 
            // nsLabel10
            // 
            nsLabel10.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel10.Location = new Point(4, 122);
            nsLabel10.Name = "nsLabel10";
            nsLabel10.Size = new Size(109, 23);
            nsLabel10.TabIndex = 24;
            nsLabel10.Text = "nsLabel10";
            nsLabel10.Value1 = "Facebook Link:";
            nsLabel10.Value2 = " ";
            // 
            // nsLabel9
            // 
            nsLabel9.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel9.Location = new Point(4, 93);
            nsLabel9.Name = "nsLabel9";
            nsLabel9.Size = new Size(109, 23);
            nsLabel9.TabIndex = 23;
            nsLabel9.Text = "nsLabel9";
            nsLabel9.Value1 = "Steam Link:";
            nsLabel9.Value2 = " ";
            // 
            // nsLabel8
            // 
            nsLabel8.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel8.Location = new Point(4, 64);
            nsLabel8.Name = "nsLabel8";
            nsLabel8.Size = new Size(109, 23);
            nsLabel8.TabIndex = 22;
            nsLabel8.Text = "nsLabel8";
            nsLabel8.Value1 = "Twitch Link:";
            nsLabel8.Value2 = " ";
            // 
            // nsLabel7
            // 
            nsLabel7.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel7.Location = new Point(4, 3);
            nsLabel7.Name = "nsLabel7";
            nsLabel7.Size = new Size(129, 23);
            nsLabel7.TabIndex = 21;
            nsLabel7.Text = "nsLabel7";
            nsLabel7.Value1 = "Bot";
            nsLabel7.Value2 = " Social Links";
            // 
            // nsLabel5
            // 
            nsLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel5.Location = new Point(4, 35);
            nsLabel5.Name = "nsLabel5";
            nsLabel5.Size = new Size(109, 23);
            nsLabel5.TabIndex = 20;
            nsLabel5.Text = "nsLabel5";
            nsLabel5.Value1 = "Youtube Link:";
            nsLabel5.Value2 = " ";
            // 
            // Youtube
            // 
            Youtube.Location = new Point(156, 35);
            Youtube.MaxLength = 32767;
            Youtube.Multiline = false;
            Youtube.Name = "Youtube";
            Youtube.ReadOnly = false;
            Youtube.Size = new Size(368, 23);
            Youtube.TabIndex = 6;
            Youtube.TextAlign = HorizontalAlignment.Left;
            Youtube.UseSystemPasswordChar = false;
            // 
            // Twitch
            // 
            Twitch.Location = new Point(156, 64);
            Twitch.MaxLength = 32767;
            Twitch.Multiline = false;
            Twitch.Name = "Twitch";
            Twitch.ReadOnly = false;
            Twitch.Size = new Size(368, 23);
            Twitch.TabIndex = 7;
            Twitch.TextAlign = HorizontalAlignment.Left;
            Twitch.UseSystemPasswordChar = false;
            // 
            // Facebook
            // 
            Facebook.Location = new Point(156, 122);
            Facebook.MaxLength = 32767;
            Facebook.Multiline = false;
            Facebook.Name = "Facebook";
            Facebook.ReadOnly = false;
            Facebook.Size = new Size(368, 23);
            Facebook.TabIndex = 9;
            Facebook.TextAlign = HorizontalAlignment.Left;
            Facebook.UseSystemPasswordChar = false;
            // 
            // Steam
            // 
            Steam.Location = new Point(156, 93);
            Steam.MaxLength = 32767;
            Steam.Multiline = false;
            Steam.Name = "Steam";
            Steam.ReadOnly = false;
            Steam.Size = new Size(368, 23);
            Steam.TabIndex = 8;
            Steam.TextAlign = HorizontalAlignment.Left;
            Steam.UseSystemPasswordChar = false;
            // 
            // nsGroupBox1
            // 
            nsGroupBox1.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox1.Controls.Add(nsCheckBox1);
            nsGroupBox1.Controls.Add(nsLabel6);
            nsGroupBox1.Controls.Add(GptApiKey);
            nsGroupBox1.Controls.Add(YoutubeAPIKey);
            nsGroupBox1.Controls.Add(nsLabel4);
            nsGroupBox1.Controls.Add(YoutubeAppName);
            nsGroupBox1.Controls.Add(nsLabel3);
            nsGroupBox1.Controls.Add(DiscordBotToken);
            nsGroupBox1.Controls.Add(nsLabel2);
            nsGroupBox1.Controls.Add(nsLabel1);
            nsGroupBox1.DrawSeperator = false;
            nsGroupBox1.Location = new Point(0, 99);
            nsGroupBox1.Name = "nsGroupBox1";
            nsGroupBox1.Size = new Size(539, 182);
            nsGroupBox1.SubTitle = "";
            nsGroupBox1.TabIndex = 33;
            nsGroupBox1.Text = "nsGroupBox1";
            nsGroupBox1.Title = "";
            // 
            // nsCheckBox1
            // 
            nsCheckBox1.Checked = false;
            nsCheckBox1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nsCheckBox1.Location = new Point(342, 6);
            nsCheckBox1.Name = "nsCheckBox1";
            nsCheckBox1.Size = new Size(182, 23);
            nsCheckBox1.TabIndex = 22;
            nsCheckBox1.Text = "Show Keys/ Sensitive Info";
            nsCheckBox1.CheckedChanged += nsCheckBox1_CheckedChanged;
            // 
            // nsLabel6
            // 
            nsLabel6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel6.Location = new Point(3, 3);
            nsLabel6.Name = "nsLabel6";
            nsLabel6.Size = new Size(129, 23);
            nsLabel6.TabIndex = 21;
            nsLabel6.Text = "nsLabel6";
            nsLabel6.Value1 = "Bot API";
            nsLabel6.Value2 = " Settings";
            // 
            // GptApiKey
            // 
            GptApiKey.Location = new Point(156, 34);
            GptApiKey.MaxLength = 32767;
            GptApiKey.Multiline = false;
            GptApiKey.Name = "GptApiKey";
            GptApiKey.ReadOnly = false;
            GptApiKey.Size = new Size(368, 23);
            GptApiKey.TabIndex = 2;
            GptApiKey.TextAlign = HorizontalAlignment.Left;
            GptApiKey.UseSystemPasswordChar = true;
            // 
            // YoutubeAPIKey
            // 
            YoutubeAPIKey.Location = new Point(156, 63);
            YoutubeAPIKey.MaxLength = 32767;
            YoutubeAPIKey.Multiline = false;
            YoutubeAPIKey.Name = "YoutubeAPIKey";
            YoutubeAPIKey.ReadOnly = false;
            YoutubeAPIKey.Size = new Size(368, 23);
            YoutubeAPIKey.TabIndex = 3;
            YoutubeAPIKey.TextAlign = HorizontalAlignment.Left;
            YoutubeAPIKey.UseSystemPasswordChar = true;
            // 
            // nsLabel4
            // 
            nsLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel4.Location = new Point(7, 121);
            nsLabel4.Name = "nsLabel4";
            nsLabel4.Size = new Size(143, 23);
            nsLabel4.TabIndex = 19;
            nsLabel4.Text = "nsLabel4";
            nsLabel4.Value1 = "Discord Bot Token:";
            nsLabel4.Value2 = " ";
            // 
            // YoutubeAppName
            // 
            YoutubeAppName.Location = new Point(156, 92);
            YoutubeAppName.MaxLength = 32767;
            YoutubeAppName.Multiline = false;
            YoutubeAppName.Name = "YoutubeAppName";
            YoutubeAppName.ReadOnly = false;
            YoutubeAppName.Size = new Size(368, 23);
            YoutubeAppName.TabIndex = 4;
            YoutubeAppName.TextAlign = HorizontalAlignment.Left;
            YoutubeAppName.UseSystemPasswordChar = true;
            // 
            // nsLabel3
            // 
            nsLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel3.Location = new Point(7, 92);
            nsLabel3.Name = "nsLabel3";
            nsLabel3.Size = new Size(143, 23);
            nsLabel3.TabIndex = 18;
            nsLabel3.Text = "nsLabel3";
            nsLabel3.Value1 = "Youtube API Name:";
            nsLabel3.Value2 = " ";
            // 
            // DiscordBotToken
            // 
            DiscordBotToken.Location = new Point(156, 121);
            DiscordBotToken.MaxLength = 32767;
            DiscordBotToken.Multiline = false;
            DiscordBotToken.Name = "DiscordBotToken";
            DiscordBotToken.ReadOnly = false;
            DiscordBotToken.Size = new Size(368, 23);
            DiscordBotToken.TabIndex = 5;
            DiscordBotToken.TextAlign = HorizontalAlignment.Left;
            DiscordBotToken.UseSystemPasswordChar = true;
            // 
            // nsLabel2
            // 
            nsLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel2.Location = new Point(7, 63);
            nsLabel2.Name = "nsLabel2";
            nsLabel2.Size = new Size(134, 23);
            nsLabel2.TabIndex = 17;
            nsLabel2.Text = "nsLabel2";
            nsLabel2.Value1 = "Youtube API Key:";
            nsLabel2.Value2 = " ";
            // 
            // nsLabel1
            // 
            nsLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            nsLabel1.Location = new Point(7, 34);
            nsLabel1.Name = "nsLabel1";
            nsLabel1.Size = new Size(134, 23);
            nsLabel1.TabIndex = 16;
            nsLabel1.Text = "nsLabel1";
            nsLabel1.Value1 = "ChatGPT API Key:";
            nsLabel1.Value2 = " ";
            // 
            // nsGroupBox4
            // 
            nsGroupBox4.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox4.Controls.Add(pictureBox1);
            nsGroupBox4.Controls.Add(nsLabel19);
            nsGroupBox4.Controls.Add(nsLabel20);
            nsGroupBox4.Controls.Add(nsLabel22);
            nsGroupBox4.DrawSeperator = false;
            nsGroupBox4.Location = new Point(0, 1);
            nsGroupBox4.Name = "nsGroupBox4";
            nsGroupBox4.Size = new Size(539, 95);
            nsGroupBox4.SubTitle = "";
            nsGroupBox4.TabIndex = 36;
            nsGroupBox4.Text = "nsGroupBox4";
            nsGroupBox4.Title = "";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources._2451296;
            pictureBox1.Location = new Point(6, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(93, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 33;
            pictureBox1.TabStop = false;
            // 
            // nsLabel19
            // 
            nsLabel19.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nsLabel19.Location = new Point(203, 5);
            nsLabel19.Name = "nsLabel19";
            nsLabel19.Size = new Size(198, 23);
            nsLabel19.TabIndex = 30;
            nsLabel19.Text = "nsLabel19";
            nsLabel19.Value1 = "Discord Bot";
            nsLabel19.Value2 = " Information";
            // 
            // nsLabel20
            // 
            nsLabel20.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            nsLabel20.Location = new Point(103, 26);
            nsLabel20.Name = "nsLabel20";
            nsLabel20.Size = new Size(421, 19);
            nsLabel20.TabIndex = 31;
            nsLabel20.Text = "nsLabel20";
            nsLabel20.Value1 = "Bot Name: ";
            nsLabel20.Value2 = " ";
            // 
            // nsLabel22
            // 
            nsLabel22.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            nsLabel22.Location = new Point(103, 47);
            nsLabel22.Name = "nsLabel22";
            nsLabel22.Size = new Size(207, 21);
            nsLabel22.TabIndex = 34;
            nsLabel22.Text = "nsLabel22";
            nsLabel22.Value1 = "Online Status: ";
            nsLabel22.Value2 = " ";
            // 
            // nsGroupBox5
            // 
            nsGroupBox5.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox5.Controls.Add(nsButton5);
            nsGroupBox5.Controls.Add(label2);
            nsGroupBox5.Controls.Add(label1);
            nsGroupBox5.DrawSeperator = false;
            nsGroupBox5.Location = new Point(545, 1);
            nsGroupBox5.Name = "nsGroupBox5";
            nsGroupBox5.Size = new Size(486, 95);
            nsGroupBox5.SubTitle = "";
            nsGroupBox5.TabIndex = 37;
            nsGroupBox5.Text = "nsGroupBox5";
            nsGroupBox5.Title = "";
            // 
            // nsButton5
            // 
            nsButton5.Location = new Point(369, 3);
            nsButton5.Name = "nsButton5";
            nsButton5.Size = new Size(113, 23);
            nsButton5.TabIndex = 29;
            nsButton5.Text = "Minimize To Tray";
            nsButton5.Click += nsButton5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(192, 0, 0);
            label2.Location = new Point(265, 36);
            label2.Name = "label2";
            label2.Size = new Size(134, 21);
            label2.TabIndex = 28;
            label2.Text = "Not Connected...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(88, 36);
            label1.Name = "label1";
            label1.Size = new Size(181, 21);
            label1.TabIndex = 27;
            label1.Text = "Bot Connection Status: ";
            // 
            // nsGroupBox6
            // 
            nsGroupBox6.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox6.Controls.Add(nsButton3);
            nsGroupBox6.Controls.Add(nsButton1);
            nsGroupBox6.Controls.Add(nsButton2);
            nsGroupBox6.DrawSeperator = false;
            nsGroupBox6.Location = new Point(545, 375);
            nsGroupBox6.Name = "nsGroupBox6";
            nsGroupBox6.Size = new Size(486, 85);
            nsGroupBox6.SubTitle = "";
            nsGroupBox6.TabIndex = 38;
            nsGroupBox6.Text = "nsGroupBox6";
            nsGroupBox6.Title = "";
            // 
            // nsButton3
            // 
            nsButton3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nsButton3.Location = new Point(244, 45);
            nsButton3.Name = "nsButton3";
            nsButton3.Size = new Size(239, 33);
            nsButton3.TabIndex = 26;
            nsButton3.Text = "Stop Bot";
            nsButton3.Click += nsButton3_Click;
            // 
            // nsButton1
            // 
            nsButton1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nsButton1.Location = new Point(2, 6);
            nsButton1.Name = "nsButton1";
            nsButton1.Size = new Size(479, 33);
            nsButton1.TabIndex = 0;
            nsButton1.Text = "Save Settings...";
            nsButton1.Click += nsButton1_Click;
            // 
            // nsButton2
            // 
            nsButton2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nsButton2.Location = new Point(3, 45);
            nsButton2.Name = "nsButton2";
            nsButton2.Size = new Size(239, 33);
            nsButton2.TabIndex = 1;
            nsButton2.Text = "Run Bot";
            nsButton2.Click += nsButton2_Click;
            // 
            // nsGroupBox8
            // 
            nsGroupBox8.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox8.Controls.Add(linkLabel1);
            nsGroupBox8.Controls.Add(linkLabel2);
            nsGroupBox8.DrawSeperator = false;
            nsGroupBox8.Location = new Point(2, 460);
            nsGroupBox8.Name = "nsGroupBox8";
            nsGroupBox8.Size = new Size(1028, 23);
            nsGroupBox8.SubTitle = "";
            nsGroupBox8.TabIndex = 39;
            nsGroupBox8.Text = "nsGroupBox8";
            nsGroupBox8.Title = "";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.FromArgb(30, 30, 30);
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(7, 3);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(217, 15);
            linkLabel1.TabIndex = 33;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Donate or Subscribe via BuyMeACoffee!";
            linkLabel1.VisitedLinkColor = Color.Silver;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.FromArgb(30, 30, 30);
            linkLabel2.LinkColor = Color.White;
            linkLabel2.Location = new Point(914, 3);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(110, 15);
            linkLabel2.TabIndex = 30;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "My GitHub Projects";
            linkLabel2.VisitedLinkColor = Color.Silver;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(50, 50, 50);
            tabPage2.Controls.Add(botConsoleView);
            tabPage2.Controls.Add(nsGroupBox7);
            tabPage2.Controls.Add(nsGroupBox9);
            tabPage2.Location = new Point(119, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1033, 483);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Console View";
            // 
            // botConsoleView
            // 
            botConsoleView.BackColor = Color.Black;
            botConsoleView.ForeColor = Color.White;
            botConsoleView.Location = new Point(3, 44);
            botConsoleView.Multiline = true;
            botConsoleView.Name = "botConsoleView";
            botConsoleView.ReadOnly = true;
            botConsoleView.ScrollBars = ScrollBars.Vertical;
            botConsoleView.Size = new Size(1030, 332);
            botConsoleView.TabIndex = 32;
            // 
            // nsGroupBox7
            // 
            nsGroupBox7.Controls.Add(nsButton4);
            nsGroupBox7.Controls.Add(nsLabel18);
            nsGroupBox7.DrawSeperator = false;
            nsGroupBox7.Location = new Point(5, 3);
            nsGroupBox7.Name = "nsGroupBox7";
            nsGroupBox7.Size = new Size(1024, 35);
            nsGroupBox7.SubTitle = "";
            nsGroupBox7.TabIndex = 34;
            nsGroupBox7.Text = "nsGroupBox7";
            nsGroupBox7.Title = "";
            // 
            // nsButton4
            // 
            nsButton4.Location = new Point(880, 6);
            nsButton4.Name = "nsButton4";
            nsButton4.Size = new Size(138, 23);
            nsButton4.TabIndex = 33;
            nsButton4.Text = "Clear Output Window";
            nsButton4.Click += nsButton4_Click;
            // 
            // nsLabel18
            // 
            nsLabel18.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nsLabel18.Location = new Point(3, 6);
            nsLabel18.Name = "nsLabel18";
            nsLabel18.Size = new Size(441, 23);
            nsLabel18.TabIndex = 31;
            nsLabel18.Text = "nsLabel18";
            nsLabel18.Value1 = "Bot Console View (Shows Logs, Errors, Disconnects, Etc.)";
            nsLabel18.Value2 = " ";
            // 
            // nsGroupBox9
            // 
            nsGroupBox9.BackColor = Color.FromArgb(30, 30, 30);
            nsGroupBox9.Controls.Add(nsComboBox1);
            nsGroupBox9.Controls.Add(consolebtnSend);
            nsGroupBox9.Controls.Add(commandInputConsoleview);
            nsGroupBox9.Controls.Add(nsLabel21);
            nsGroupBox9.DrawSeperator = false;
            nsGroupBox9.Location = new Point(3, 381);
            nsGroupBox9.Name = "nsGroupBox9";
            nsGroupBox9.Size = new Size(1030, 101);
            nsGroupBox9.SubTitle = "";
            nsGroupBox9.TabIndex = 38;
            nsGroupBox9.Text = "nsGroupBox9";
            nsGroupBox9.Title = "";
            // 
            // nsComboBox1
            // 
            nsComboBox1.BackColor = Color.FromArgb(50, 50, 50);
            nsComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            nsComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            nsComboBox1.ForeColor = Color.White;
            nsComboBox1.FormattingEnabled = true;
            nsComboBox1.Items.AddRange(new object[] { "main-channel" });
            nsComboBox1.Location = new Point(5, 35);
            nsComboBox1.Name = "nsComboBox1";
            nsComboBox1.Size = new Size(232, 24);
            nsComboBox1.TabIndex = 38;
            // 
            // consolebtnSend
            // 
            consolebtnSend.Enabled = false;
            consolebtnSend.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            consolebtnSend.Location = new Point(947, 18);
            consolebtnSend.Name = "consolebtnSend";
            consolebtnSend.Size = new Size(79, 59);
            consolebtnSend.TabIndex = 37;
            consolebtnSend.Text = "Send";
            consolebtnSend.Click += consolebtnSend_Click;
            // 
            // commandInputConsoleview
            // 
            commandInputConsoleview.Location = new Point(243, 5);
            commandInputConsoleview.MaxLength = 32767;
            commandInputConsoleview.Multiline = true;
            commandInputConsoleview.Name = "commandInputConsoleview";
            commandInputConsoleview.ReadOnly = false;
            commandInputConsoleview.Size = new Size(698, 87);
            commandInputConsoleview.TabIndex = 35;
            commandInputConsoleview.TextAlign = HorizontalAlignment.Left;
            commandInputConsoleview.UseSystemPasswordChar = false;
            // 
            // nsLabel21
            // 
            nsLabel21.Font = new Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nsLabel21.Location = new Point(29, 5);
            nsLabel21.Name = "nsLabel21";
            nsLabel21.Size = new Size(190, 24);
            nsLabel21.TabIndex = 36;
            nsLabel21.Text = "nsLabel21";
            nsLabel21.Value1 = "Send Message";
            nsLabel21.Value2 = "to Channel";
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Bot is running in the background...";
            notifyIcon1.BalloonTipTitle = "VoidBot Discord Bot";
            notifyIcon1.ContextMenuStrip = nsContextMenu1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            notifyIcon1.MouseMove += notifyIcon1_MouseMove;
            // 
            // nsContextMenu1
            // 
            nsContextMenu1.ForeColor = Color.White;
            nsContextMenu1.Items.AddRange(new ToolStripItem[] { openBotPanelToolStripMenuItem, closeBotToolStripMenuItem });
            nsContextMenu1.Name = "nsContextMenu1";
            nsContextMenu1.Size = new Size(157, 48);
            nsContextMenu1.Text = "File...";
            // 
            // openBotPanelToolStripMenuItem
            // 
            openBotPanelToolStripMenuItem.Name = "openBotPanelToolStripMenuItem";
            openBotPanelToolStripMenuItem.Size = new Size(156, 22);
            openBotPanelToolStripMenuItem.Text = "Open Bot Panel";
            openBotPanelToolStripMenuItem.Click += openBotPanelToolStripMenuItem_Click;
            // 
            // closeBotToolStripMenuItem
            // 
            closeBotToolStripMenuItem.Name = "closeBotToolStripMenuItem";
            closeBotToolStripMenuItem.Size = new Size(156, 22);
            closeBotToolStripMenuItem.Text = "Close Bot...";
            closeBotToolStripMenuItem.Click += closeBotToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1155, 486);
            Controls.Add(nsTabControl1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "VoidBot Discord Bot [GUI]";
            Load += Form1_Load;
            nsTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            nsGroupBox3.ResumeLayout(false);
            nsGroupBox2.ResumeLayout(false);
            nsGroupBox1.ResumeLayout(false);
            nsGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            nsGroupBox5.ResumeLayout(false);
            nsGroupBox5.PerformLayout();
            nsGroupBox6.ResumeLayout(false);
            nsGroupBox8.ResumeLayout(false);
            nsGroupBox8.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            nsGroupBox7.ResumeLayout(false);
            nsGroupBox9.ResumeLayout(false);
            nsContextMenu1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NSTabControl nsTabControl1;
        private TabPage tabPage1;
        private NSGroupBox nsGroupBox3;
        private NSLabel nsLabel17;
        private NSLabel nsLabel16;
        private NSLabel nsLabel13;
        private NSTextBox BotPersonality;
        private NSLabel nsLabel12;
        private NSTextBox BotNickname;
        private NSLabel nsLabel11;
        private NSLabel nsLabel14;
        private NSTextBox StreamerRole;
        private NSLabel nsLabel15;
        private NSTextBox InviteLink;
        private NSTextBox ModeratorRole;
        private NSTextBox AutoRole;
        private NSGroupBox nsGroupBox2;
        private NSLabel nsLabel10;
        private NSLabel nsLabel9;
        private NSLabel nsLabel8;
        private NSLabel nsLabel7;
        private NSLabel nsLabel5;
        private NSTextBox Youtube;
        private NSTextBox Twitch;
        private NSTextBox Facebook;
        private NSTextBox Steam;
        private NSGroupBox nsGroupBox1;
        private NSCheckBox nsCheckBox1;
        private NSLabel nsLabel6;
        private NSTextBox GptApiKey;
        private NSTextBox YoutubeAPIKey;
        private NSLabel nsLabel4;
        private NSTextBox YoutubeAppName;
        private NSLabel nsLabel3;
        private NSTextBox DiscordBotToken;
        private NSLabel nsLabel2;
        private NSLabel nsLabel1;
        private NSGroupBox nsGroupBox4;
        private PictureBox pictureBox1;
        private NSLabel nsLabel19;
        private NSLabel nsLabel20;
        private NSGroupBox nsGroupBox5;
        public Label label2;
        private Label label1;
        private NSGroupBox nsGroupBox6;
        private NSButton nsButton3;
        private NSButton nsButton1;
        private NSButton nsButton2;
        private TabPage tabPage2;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private NSLabel nsLabel18;
        private NSButton nsButton4;
        private NSGroupBox nsGroupBox7;
        private NotifyIcon notifyIcon1;
        private NSButton nsButton5;
        private NSContextMenu nsContextMenu1;
        private ToolStripMenuItem openBotPanelToolStripMenuItem;
        private ToolStripMenuItem closeBotToolStripMenuItem;
        private NSGroupBox nsGroupBox8;
        private NSButton consolebtnSend;
        private NSLabel nsLabel21;
        private NSTextBox commandInputConsoleview;
        private NSGroupBox nsGroupBox9;
        public NSComboBox nsComboBox1;
        private NSLabel nsLabel22;
        public TextBox botConsoleView;
        private NSTextBox ServerID;
        private NSLabel nsLabel23;
    }
}
