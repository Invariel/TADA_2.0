namespace TADA_Advanced_Desktop_Alerts
{
    partial class MainTADAForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainTADAForm));
            this.numud_bgB = new System.Windows.Forms.NumericUpDown();
            this.numud_bgG = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numud_bgR = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ShowHideAnimationWindow = new System.Windows.Forms.Button();
            this.txt_BackgroundARGB = new System.Windows.Forms.TextBox();
            this.numud_AnimationWindowSizeY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numud_AnimationWindowSizeX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SaveManifest = new System.Windows.Forms.Button();
            this.btn_LoadManifest = new System.Windows.Forms.Button();
            this.btn_MergeManifest = new System.Windows.Forms.Button();
            this.dg_Alerts = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_LoadChannelDetails = new System.Windows.Forms.Button();
            this.btn_SaveChannelDetails = new System.Windows.Forms.Button();
            this.btn_RevokeAccessToken = new System.Windows.Forms.Button();
            this.txt_AccessToken = new System.Windows.Forms.TextBox();
            this.btn_GetAccessToken = new System.Windows.Forms.Button();
            this.txt_ChannelName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tab_Main = new System.Windows.Forms.TabControl();
            this.tab_Manifest = new System.Windows.Forms.TabPage();
            this.tab_Banned = new System.Windows.Forms.TabPage();
            this.lbl_Banned = new System.Windows.Forms.Label();
            this.txt_BlockedWords = new System.Windows.Forms.TextBox();
            this.tab_AlertSettings = new System.Windows.Forms.TabPage();
            this.mtxt_TimeBetweenTTS = new System.Windows.Forms.MaskedTextBox();
            this.lbl_MuteTTS2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chk_DoReadMyEmotes = new System.Windows.Forms.CheckBox();
            this.chk_DoReadMyCheermote = new System.Windows.Forms.CheckBox();
            this.chk_DontReadGlobalCheermotes = new System.Windows.Forms.CheckBox();
            this.chk_DontReadGlobalEmotes = new System.Windows.Forms.CheckBox();
            this.mtxt_FollowCalloutLurkers = new System.Windows.Forms.MaskedTextBox();
            this.chk_FollowCalloutLurkers = new System.Windows.Forms.CheckBox();
            this.chk_TimeBetweenTTS = new System.Windows.Forms.CheckBox();
            this.lbl_MuteTTS1 = new System.Windows.Forms.Label();
            this.tab_Configuration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ColourPicker = new System.Windows.Forms.Button();
            this.rad_30FPS = new System.Windows.Forms.RadioButton();
            this.rad_60FPS = new System.Windows.Forms.RadioButton();
            this.btn_Update = new System.Windows.Forms.Button();
            this.alertNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tab_Main.SuspendLayout();
            this.tab_Manifest.SuspendLayout();
            this.tab_Banned.SuspendLayout();
            this.tab_AlertSettings.SuspendLayout();
            this.tab_Configuration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // numud_bgB
            // 
            this.numud_bgB.Location = new System.Drawing.Point(164, 50);
            this.numud_bgB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_bgB.Name = "numud_bgB";
            this.numud_bgB.Size = new System.Drawing.Size(42, 20);
            this.numud_bgB.TabIndex = 79;
            this.numud_bgB.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // numud_bgG
            // 
            this.numud_bgG.Location = new System.Drawing.Point(96, 50);
            this.numud_bgG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_bgG.Name = "numud_bgG";
            this.numud_bgG.Size = new System.Drawing.Size(42, 20);
            this.numud_bgG.TabIndex = 75;
            this.numud_bgG.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 73;
            this.label5.Text = "G";
            // 
            // numud_bgR
            // 
            this.numud_bgR.Location = new System.Drawing.Point(27, 50);
            this.numud_bgR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_bgR.Name = "numud_bgR";
            this.numud_bgR.Size = new System.Drawing.Size(42, 20);
            this.numud_bgR.TabIndex = 71;
            this.numud_bgR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "R";
            // 
            // btn_ShowHideAnimationWindow
            // 
            this.btn_ShowHideAnimationWindow.Location = new System.Drawing.Point(326, 53);
            this.btn_ShowHideAnimationWindow.Name = "btn_ShowHideAnimationWindow";
            this.btn_ShowHideAnimationWindow.Size = new System.Drawing.Size(156, 23);
            this.btn_ShowHideAnimationWindow.TabIndex = 89;
            this.btn_ShowHideAnimationWindow.Text = "Hide &Animation Window";
            this.btn_ShowHideAnimationWindow.UseVisualStyleBackColor = true;
            this.btn_ShowHideAnimationWindow.Click += new System.EventHandler(this.HideAnimationWindow);
            // 
            // txt_BackgroundARGB
            // 
            this.txt_BackgroundARGB.Location = new System.Drawing.Point(110, 23);
            this.txt_BackgroundARGB.Name = "txt_BackgroundARGB";
            this.txt_BackgroundARGB.Size = new System.Drawing.Size(67, 20);
            this.txt_BackgroundARGB.TabIndex = 67;
            this.txt_BackgroundARGB.Text = "#FFFFFF";
            // 
            // numud_AnimationWindowSizeY
            // 
            this.numud_AnimationWindowSizeY.Location = new System.Drawing.Point(410, 23);
            this.numud_AnimationWindowSizeY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numud_AnimationWindowSizeY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numud_AnimationWindowSizeY.Name = "numud_AnimationWindowSizeY";
            this.numud_AnimationWindowSizeY.Size = new System.Drawing.Size(72, 20);
            this.numud_AnimationWindowSizeY.TabIndex = 87;
            this.numud_AnimationWindowSizeY.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "&Window Dimensions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "&Background Colour";
            // 
            // numud_AnimationWindowSizeX
            // 
            this.numud_AnimationWindowSizeX.Location = new System.Drawing.Point(326, 23);
            this.numud_AnimationWindowSizeX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numud_AnimationWindowSizeX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numud_AnimationWindowSizeX.Name = "numud_AnimationWindowSizeX";
            this.numud_AnimationWindowSizeX.Size = new System.Drawing.Size(72, 20);
            this.numud_AnimationWindowSizeX.TabIndex = 83;
            this.numud_AnimationWindowSizeX.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(398, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "x";
            // 
            // btn_SaveManifest
            // 
            this.btn_SaveManifest.Location = new System.Drawing.Point(8, 196);
            this.btn_SaveManifest.Name = "btn_SaveManifest";
            this.btn_SaveManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_SaveManifest.TabIndex = 5;
            this.btn_SaveManifest.Text = "Save Alerts";
            this.btn_SaveManifest.UseVisualStyleBackColor = true;
            // 
            // btn_LoadManifest
            // 
            this.btn_LoadManifest.Location = new System.Drawing.Point(133, 196);
            this.btn_LoadManifest.Name = "btn_LoadManifest";
            this.btn_LoadManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_LoadManifest.TabIndex = 7;
            this.btn_LoadManifest.Text = "Load Alerts";
            this.btn_LoadManifest.UseVisualStyleBackColor = true;
            // 
            // btn_MergeManifest
            // 
            this.btn_MergeManifest.Location = new System.Drawing.Point(258, 196);
            this.btn_MergeManifest.Name = "btn_MergeManifest";
            this.btn_MergeManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_MergeManifest.TabIndex = 9;
            this.btn_MergeManifest.Text = "Merge Alerts";
            this.btn_MergeManifest.UseVisualStyleBackColor = true;
            // 
            // dg_Alerts
            // 
            this.dg_Alerts.AutoGenerateColumns = false;
            this.dg_Alerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Alerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alertNameDataGridViewTextBoxColumn,
            this.alertTypeDataGridViewTextBoxColumn,
            this.Edit});
            this.dg_Alerts.DataSource = this.alertBindingSource;
            this.dg_Alerts.Location = new System.Drawing.Point(8, 6);
            this.dg_Alerts.Name = "dg_Alerts";
            this.dg_Alerts.Size = new System.Drawing.Size(494, 184);
            this.dg_Alerts.TabIndex = 3;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "New / Edit";
            this.Edit.MinimumWidth = 75;
            this.Edit.Name = "Edit";
            this.Edit.Text = "Edit";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_LoadChannelDetails);
            this.groupBox2.Controls.Add(this.btn_SaveChannelDetails);
            this.groupBox2.Controls.Add(this.btn_RevokeAccessToken);
            this.groupBox2.Controls.Add(this.txt_AccessToken);
            this.groupBox2.Controls.Add(this.btn_GetAccessToken);
            this.groupBox2.Controls.Add(this.txt_ChannelName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(8, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 127);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Twitch Settings";
            // 
            // btn_LoadChannelDetails
            // 
            this.btn_LoadChannelDetails.Location = new System.Drawing.Point(326, 73);
            this.btn_LoadChannelDetails.Name = "btn_LoadChannelDetails";
            this.btn_LoadChannelDetails.Size = new System.Drawing.Size(156, 23);
            this.btn_LoadChannelDetails.TabIndex = 105;
            this.btn_LoadChannelDetails.Text = "Load Channel Details";
            this.btn_LoadChannelDetails.UseVisualStyleBackColor = true;
            // 
            // btn_SaveChannelDetails
            // 
            this.btn_SaveChannelDetails.Location = new System.Drawing.Point(10, 73);
            this.btn_SaveChannelDetails.Name = "btn_SaveChannelDetails";
            this.btn_SaveChannelDetails.Size = new System.Drawing.Size(156, 23);
            this.btn_SaveChannelDetails.TabIndex = 103;
            this.btn_SaveChannelDetails.Text = "Save Channel Details";
            this.btn_SaveChannelDetails.UseVisualStyleBackColor = true;
            // 
            // btn_RevokeAccessToken
            // 
            this.btn_RevokeAccessToken.Location = new System.Drawing.Point(372, 43);
            this.btn_RevokeAccessToken.Name = "btn_RevokeAccessToken";
            this.btn_RevokeAccessToken.Size = new System.Drawing.Size(110, 23);
            this.btn_RevokeAccessToken.TabIndex = 101;
            this.btn_RevokeAccessToken.Text = "Revoke Token";
            this.btn_RevokeAccessToken.UseVisualStyleBackColor = true;
            // 
            // txt_AccessToken
            // 
            this.txt_AccessToken.Location = new System.Drawing.Point(127, 44);
            this.txt_AccessToken.Name = "txt_AccessToken";
            this.txt_AccessToken.Size = new System.Drawing.Size(239, 20);
            this.txt_AccessToken.TabIndex = 99;
            this.txt_AccessToken.Text = "Paste Access Token Here";
            // 
            // btn_GetAccessToken
            // 
            this.btn_GetAccessToken.Location = new System.Drawing.Point(10, 43);
            this.btn_GetAccessToken.Name = "btn_GetAccessToken";
            this.btn_GetAccessToken.Size = new System.Drawing.Size(110, 23);
            this.btn_GetAccessToken.TabIndex = 97;
            this.btn_GetAccessToken.Text = "Get Access Token";
            this.btn_GetAccessToken.UseVisualStyleBackColor = true;
            // 
            // txt_ChannelName
            // 
            this.txt_ChannelName.Location = new System.Drawing.Point(127, 17);
            this.txt_ChannelName.Name = "txt_ChannelName";
            this.txt_ChannelName.Size = new System.Drawing.Size(355, 20);
            this.txt_ChannelName.TabIndex = 95;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Channel Name";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(383, 196);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(120, 23);
            this.btn_Connect.TabIndex = 11;
            this.btn_Connect.Text = "Connect!";
            this.btn_Connect.UseVisualStyleBackColor = true;
            // 
            // tab_Main
            // 
            this.tab_Main.Controls.Add(this.tab_Manifest);
            this.tab_Main.Controls.Add(this.tab_Banned);
            this.tab_Main.Controls.Add(this.tab_AlertSettings);
            this.tab_Main.Controls.Add(this.tab_Configuration);
            this.tab_Main.Location = new System.Drawing.Point(13, 13);
            this.tab_Main.Name = "tab_Main";
            this.tab_Main.SelectedIndex = 0;
            this.tab_Main.Size = new System.Drawing.Size(516, 254);
            this.tab_Main.TabIndex = 1;
            // 
            // tab_Manifest
            // 
            this.tab_Manifest.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Manifest.Controls.Add(this.dg_Alerts);
            this.tab_Manifest.Controls.Add(this.btn_SaveManifest);
            this.tab_Manifest.Controls.Add(this.btn_Connect);
            this.tab_Manifest.Controls.Add(this.btn_LoadManifest);
            this.tab_Manifest.Controls.Add(this.btn_MergeManifest);
            this.tab_Manifest.Location = new System.Drawing.Point(4, 22);
            this.tab_Manifest.Name = "tab_Manifest";
            this.tab_Manifest.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Manifest.Size = new System.Drawing.Size(508, 228);
            this.tab_Manifest.TabIndex = 0;
            this.tab_Manifest.Text = "Alert Manifest";
            // 
            // tab_Banned
            // 
            this.tab_Banned.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Banned.Controls.Add(this.lbl_Banned);
            this.tab_Banned.Controls.Add(this.txt_BlockedWords);
            this.tab_Banned.Location = new System.Drawing.Point(4, 22);
            this.tab_Banned.Name = "tab_Banned";
            this.tab_Banned.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Banned.Size = new System.Drawing.Size(508, 228);
            this.tab_Banned.TabIndex = 2;
            this.tab_Banned.Text = "Banned Words";
            // 
            // lbl_Banned
            // 
            this.lbl_Banned.AutoSize = true;
            this.lbl_Banned.Location = new System.Drawing.Point(6, 3);
            this.lbl_Banned.Name = "lbl_Banned";
            this.lbl_Banned.Size = new System.Drawing.Size(422, 13);
            this.lbl_Banned.TabIndex = 21;
            this.lbl_Banned.Text = "A space-separated list of words you don\'t want to have appear in viewer alert mes" +
    "sages.";
            // 
            // txt_BlockedWords
            // 
            this.txt_BlockedWords.Location = new System.Drawing.Point(8, 19);
            this.txt_BlockedWords.Multiline = true;
            this.txt_BlockedWords.Name = "txt_BlockedWords";
            this.txt_BlockedWords.Size = new System.Drawing.Size(494, 203);
            this.txt_BlockedWords.TabIndex = 23;
            // 
            // tab_AlertSettings
            // 
            this.tab_AlertSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tab_AlertSettings.Controls.Add(this.mtxt_TimeBetweenTTS);
            this.tab_AlertSettings.Controls.Add(this.lbl_MuteTTS2);
            this.tab_AlertSettings.Controls.Add(this.label8);
            this.tab_AlertSettings.Controls.Add(this.chk_DoReadMyEmotes);
            this.tab_AlertSettings.Controls.Add(this.chk_DoReadMyCheermote);
            this.tab_AlertSettings.Controls.Add(this.chk_DontReadGlobalCheermotes);
            this.tab_AlertSettings.Controls.Add(this.chk_DontReadGlobalEmotes);
            this.tab_AlertSettings.Controls.Add(this.mtxt_FollowCalloutLurkers);
            this.tab_AlertSettings.Controls.Add(this.chk_FollowCalloutLurkers);
            this.tab_AlertSettings.Controls.Add(this.chk_TimeBetweenTTS);
            this.tab_AlertSettings.Controls.Add(this.lbl_MuteTTS1);
            this.tab_AlertSettings.Location = new System.Drawing.Point(4, 22);
            this.tab_AlertSettings.Name = "tab_AlertSettings";
            this.tab_AlertSettings.Size = new System.Drawing.Size(508, 228);
            this.tab_AlertSettings.TabIndex = 3;
            this.tab_AlertSettings.Text = "Alert Settings";
            // 
            // mtxt_TimeBetweenTTS
            // 
            this.mtxt_TimeBetweenTTS.Location = new System.Drawing.Point(100, 126);
            this.mtxt_TimeBetweenTTS.Mask = "###:##.##";
            this.mtxt_TimeBetweenTTS.Name = "mtxt_TimeBetweenTTS";
            this.mtxt_TimeBetweenTTS.PromptChar = '0';
            this.mtxt_TimeBetweenTTS.Size = new System.Drawing.Size(60, 20);
            this.mtxt_TimeBetweenTTS.TabIndex = 45;
            this.mtxt_TimeBetweenTTS.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // lbl_MuteTTS2
            // 
            this.lbl_MuteTTS2.AutoSize = true;
            this.lbl_MuteTTS2.Location = new System.Drawing.Point(28, 152);
            this.lbl_MuteTTS2.Name = "lbl_MuteTTS2";
            this.lbl_MuteTTS2.Size = new System.Drawing.Size(365, 13);
            this.lbl_MuteTTS2.TabIndex = 49;
            this.lbl_MuteTTS2.Text = "Messages received before this time will be displayed on screen but not read.";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(202, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(300, 40);
            this.label8.TabIndex = 51;
            this.label8.Text = "While these settings will prevent Twitch emotes from being read by the text-to-sp" +
    "eech voice, it will not catch third party emotes. (Better Twitch TV, FrankerFace" +
    "Z, etc.)";
            // 
            // chk_DoReadMyEmotes
            // 
            this.chk_DoReadMyEmotes.AutoSize = true;
            this.chk_DoReadMyEmotes.Enabled = false;
            this.chk_DoReadMyEmotes.Location = new System.Drawing.Point(28, 36);
            this.chk_DoReadMyEmotes.Name = "chk_DoReadMyEmotes";
            this.chk_DoReadMyEmotes.Size = new System.Drawing.Size(163, 17);
            this.chk_DoReadMyEmotes.TabIndex = 33;
            this.chk_DoReadMyEmotes.Text = "Except my channel\'s emotes.";
            this.chk_DoReadMyEmotes.UseVisualStyleBackColor = true;
            // 
            // chk_DoReadMyCheermote
            // 
            this.chk_DoReadMyCheermote.AutoSize = true;
            this.chk_DoReadMyCheermote.Enabled = false;
            this.chk_DoReadMyCheermote.Location = new System.Drawing.Point(28, 82);
            this.chk_DoReadMyCheermote.Name = "chk_DoReadMyCheermote";
            this.chk_DoReadMyCheermote.Size = new System.Drawing.Size(240, 17);
            this.chk_DoReadMyCheermote.TabIndex = 37;
            this.chk_DoReadMyCheermote.Text = "Except my Cheermote. (Twitch Partners only.)";
            this.chk_DoReadMyCheermote.UseVisualStyleBackColor = true;
            // 
            // chk_DontReadGlobalCheermotes
            // 
            this.chk_DontReadGlobalCheermotes.AutoSize = true;
            this.chk_DontReadGlobalCheermotes.Enabled = false;
            this.chk_DontReadGlobalCheermotes.Location = new System.Drawing.Point(8, 59);
            this.chk_DontReadGlobalCheermotes.Name = "chk_DontReadGlobalCheermotes";
            this.chk_DontReadGlobalCheermotes.Size = new System.Drawing.Size(113, 17);
            this.chk_DontReadGlobalCheermotes.TabIndex = 35;
            this.chk_DontReadGlobalCheermotes.Text = "Don\'t read cheers.";
            this.chk_DontReadGlobalCheermotes.UseVisualStyleBackColor = true;
            // 
            // chk_DontReadGlobalEmotes
            // 
            this.chk_DontReadGlobalEmotes.AutoSize = true;
            this.chk_DontReadGlobalEmotes.Enabled = false;
            this.chk_DontReadGlobalEmotes.Location = new System.Drawing.Point(8, 13);
            this.chk_DontReadGlobalEmotes.Name = "chk_DontReadGlobalEmotes";
            this.chk_DontReadGlobalEmotes.Size = new System.Drawing.Size(115, 17);
            this.chk_DontReadGlobalEmotes.TabIndex = 31;
            this.chk_DontReadGlobalEmotes.Text = "Don\'t read emotes.";
            this.chk_DontReadGlobalEmotes.UseVisualStyleBackColor = true;
            // 
            // mtxt_FollowCalloutLurkers
            // 
            this.mtxt_FollowCalloutLurkers.Location = new System.Drawing.Point(348, 103);
            this.mtxt_FollowCalloutLurkers.Mask = "###:##.##";
            this.mtxt_FollowCalloutLurkers.Name = "mtxt_FollowCalloutLurkers";
            this.mtxt_FollowCalloutLurkers.PromptChar = '0';
            this.mtxt_FollowCalloutLurkers.Size = new System.Drawing.Size(60, 20);
            this.mtxt_FollowCalloutLurkers.TabIndex = 41;
            this.mtxt_FollowCalloutLurkers.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // chk_FollowCalloutLurkers
            // 
            this.chk_FollowCalloutLurkers.AutoSize = true;
            this.chk_FollowCalloutLurkers.Location = new System.Drawing.Point(8, 105);
            this.chk_FollowCalloutLurkers.Name = "chk_FollowCalloutLurkers";
            this.chk_FollowCalloutLurkers.Size = new System.Drawing.Size(344, 17);
            this.chk_FollowCalloutLurkers.TabIndex = 39;
            this.chk_FollowCalloutLurkers.Text = "Don\'t play Follow alerts for users that have been silent for more than";
            this.chk_FollowCalloutLurkers.UseVisualStyleBackColor = true;
            // 
            // chk_TimeBetweenTTS
            // 
            this.chk_TimeBetweenTTS.AutoSize = true;
            this.chk_TimeBetweenTTS.Location = new System.Drawing.Point(8, 128);
            this.chk_TimeBetweenTTS.Name = "chk_TimeBetweenTTS";
            this.chk_TimeBetweenTTS.Size = new System.Drawing.Size(96, 17);
            this.chk_TimeBetweenTTS.TabIndex = 43;
            this.chk_TimeBetweenTTS.Text = "Ensure at least";
            this.chk_TimeBetweenTTS.UseVisualStyleBackColor = true;
            // 
            // lbl_MuteTTS1
            // 
            this.lbl_MuteTTS1.AutoSize = true;
            this.lbl_MuteTTS1.Location = new System.Drawing.Point(161, 130);
            this.lbl_MuteTTS1.Name = "lbl_MuteTTS1";
            this.lbl_MuteTTS1.Size = new System.Drawing.Size(217, 13);
            this.lbl_MuteTTS1.TabIndex = 47;
            this.lbl_MuteTTS1.Text = "between TTS messages from the same user.";
            // 
            // tab_Configuration
            // 
            this.tab_Configuration.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Configuration.Controls.Add(this.groupBox1);
            this.tab_Configuration.Controls.Add(this.groupBox2);
            this.tab_Configuration.Location = new System.Drawing.Point(4, 22);
            this.tab_Configuration.Name = "tab_Configuration";
            this.tab_Configuration.Size = new System.Drawing.Size(508, 228);
            this.tab_Configuration.TabIndex = 4;
            this.tab_Configuration.Text = "TADA Configuration";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ColourPicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_ShowHideAnimationWindow);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeX);
            this.groupBox1.Controls.Add(this.numud_bgB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numud_bgR);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_BackgroundARGB);
            this.groupBox1.Controls.Add(this.numud_bgG);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 82);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Window Settings";
            // 
            // btn_ColourPicker
            // 
            this.btn_ColourPicker.Image = ((System.Drawing.Image)(resources.GetObject("btn_ColourPicker.Image")));
            this.btn_ColourPicker.Location = new System.Drawing.Point(184, 23);
            this.btn_ColourPicker.Name = "btn_ColourPicker";
            this.btn_ColourPicker.Size = new System.Drawing.Size(22, 23);
            this.btn_ColourPicker.TabIndex = 65;
            this.btn_ColourPicker.UseVisualStyleBackColor = true;
            // 
            // rad_30FPS
            // 
            this.rad_30FPS.AutoSize = true;
            this.rad_30FPS.Checked = true;
            this.rad_30FPS.Location = new System.Drawing.Point(392, 269);
            this.rad_30FPS.Name = "rad_30FPS";
            this.rad_30FPS.Size = new System.Drawing.Size(60, 17);
            this.rad_30FPS.TabIndex = 15;
            this.rad_30FPS.TabStop = true;
            this.rad_30FPS.Text = "30 FPS";
            this.rad_30FPS.UseVisualStyleBackColor = true;
            // 
            // rad_60FPS
            // 
            this.rad_60FPS.AutoSize = true;
            this.rad_60FPS.Location = new System.Drawing.Point(459, 269);
            this.rad_60FPS.Name = "rad_60FPS";
            this.rad_60FPS.Size = new System.Drawing.Size(60, 17);
            this.rad_60FPS.TabIndex = 17;
            this.rad_60FPS.Text = "60 FPS";
            this.rad_60FPS.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(25, 270);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(120, 23);
            this.btn_Update.TabIndex = 13;
            this.btn_Update.Text = "Newest Version";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Visible = false;
            // 
            // alertNameDataGridViewTextBoxColumn
            // 
            this.alertNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.alertNameDataGridViewTextBoxColumn.DataPropertyName = "AlertName";
            this.alertNameDataGridViewTextBoxColumn.HeaderText = "Alert Nickname";
            this.alertNameDataGridViewTextBoxColumn.Name = "alertNameDataGridViewTextBoxColumn";
            // 
            // alertTypeDataGridViewTextBoxColumn
            // 
            this.alertTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.alertTypeDataGridViewTextBoxColumn.DataPropertyName = "AlertType";
            this.alertTypeDataGridViewTextBoxColumn.HeaderText = "Alert Type";
            this.alertTypeDataGridViewTextBoxColumn.MinimumWidth = 80;
            this.alertTypeDataGridViewTextBoxColumn.Name = "alertTypeDataGridViewTextBoxColumn";
            this.alertTypeDataGridViewTextBoxColumn.Width = 80;
            // 
            // alertBindingSource
            // 
            this.alertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlert);
            // 
            // MainTADAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 298);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.rad_60FPS);
            this.Controls.Add(this.rad_30FPS);
            this.Controls.Add(this.tab_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainTADAForm";
            this.Text = "TADA Advanced Desktop Alerts";
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tab_Main.ResumeLayout(false);
            this.tab_Manifest.ResumeLayout(false);
            this.tab_Banned.ResumeLayout(false);
            this.tab_Banned.PerformLayout();
            this.tab_AlertSettings.ResumeLayout(false);
            this.tab_AlertSettings.PerformLayout();
            this.tab_Configuration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeY;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeX;
        private System.Windows.Forms.Button btn_ShowHideAnimationWindow;
        private System.Windows.Forms.BindingSource alertBindingSource;
        private System.Windows.Forms.Button btn_SaveManifest;
        private System.Windows.Forms.Button btn_LoadManifest;
        private System.Windows.Forms.Button btn_MergeManifest;
        private System.Windows.Forms.DataGridView dg_Alerts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numud_bgG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numud_bgR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_BackgroundARGB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numud_bgB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_RevokeAccessToken;
        private System.Windows.Forms.TextBox txt_AccessToken;
        private System.Windows.Forms.Button btn_GetAccessToken;
        private System.Windows.Forms.TextBox txt_ChannelName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_LoadChannelDetails;
        private System.Windows.Forms.Button btn_SaveChannelDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.TabControl tab_Main;
        private System.Windows.Forms.TabPage tab_Manifest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ColourPicker;
        private System.Windows.Forms.TabPage tab_Banned;
        internal System.Windows.Forms.RadioButton rad_30FPS;
        internal System.Windows.Forms.RadioButton rad_60FPS;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TabPage tab_AlertSettings;
        private System.Windows.Forms.TabPage tab_Configuration;
        private System.Windows.Forms.MaskedTextBox mtxt_TimeBetweenTTS;
        private System.Windows.Forms.Label lbl_MuteTTS2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chk_DoReadMyEmotes;
        private System.Windows.Forms.CheckBox chk_DoReadMyCheermote;
        private System.Windows.Forms.CheckBox chk_DontReadGlobalCheermotes;
        private System.Windows.Forms.CheckBox chk_DontReadGlobalEmotes;
        private System.Windows.Forms.MaskedTextBox mtxt_FollowCalloutLurkers;
        private System.Windows.Forms.CheckBox chk_FollowCalloutLurkers;
        private System.Windows.Forms.CheckBox chk_TimeBetweenTTS;
        private System.Windows.Forms.Label lbl_MuteTTS1;
        private System.Windows.Forms.Label lbl_Banned;
        private System.Windows.Forms.TextBox txt_BlockedWords;
    }
}

