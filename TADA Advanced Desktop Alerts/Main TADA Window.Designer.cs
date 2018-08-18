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
            this.alertNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_LoadChannelDetails = new System.Windows.Forms.Button();
            this.btn_SaveChannelDetails = new System.Windows.Forms.Button();
            this.btn_RevokeAccessToken = new System.Windows.Forms.Button();
            this.txt_AccessToken = new System.Windows.Forms.TextBox();
            this.btn_GetAccessToken = new System.Windows.Forms.Button();
            this.txt_ChannelName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Manifest = new System.Windows.Forms.TabPage();
            this.tab_Other = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ColourPicker = new System.Windows.Forms.Button();
            this.tab_Banned = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rad_30FPS = new System.Windows.Forms.RadioButton();
            this.rad_60FPS = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_Manifest.SuspendLayout();
            this.tab_Other.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tab_Banned.SuspendLayout();
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
            this.numud_bgB.TabIndex = 14;
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
            this.numud_bgG.TabIndex = 13;
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
            this.label6.TabIndex = 5;
            this.label6.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 5;
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
            this.numud_bgR.TabIndex = 12;
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
            this.label4.TabIndex = 5;
            this.label4.Text = "R";
            // 
            // btn_ShowHideAnimationWindow
            // 
            this.btn_ShowHideAnimationWindow.Location = new System.Drawing.Point(326, 53);
            this.btn_ShowHideAnimationWindow.Name = "btn_ShowHideAnimationWindow";
            this.btn_ShowHideAnimationWindow.Size = new System.Drawing.Size(156, 23);
            this.btn_ShowHideAnimationWindow.TabIndex = 15;
            this.btn_ShowHideAnimationWindow.Text = "Hide &Animation Window";
            this.btn_ShowHideAnimationWindow.UseVisualStyleBackColor = true;
            this.btn_ShowHideAnimationWindow.Click += new System.EventHandler(this.HideAnimationWindow);
            // 
            // txt_BackgroundARGB
            // 
            this.txt_BackgroundARGB.Location = new System.Drawing.Point(110, 23);
            this.txt_BackgroundARGB.Name = "txt_BackgroundARGB";
            this.txt_BackgroundARGB.Size = new System.Drawing.Size(67, 20);
            this.txt_BackgroundARGB.TabIndex = 11;
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
            this.numud_AnimationWindowSizeY.TabIndex = 9;
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
            this.label2.TabIndex = 7;
            this.label2.Text = "&Window Dimensions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 10;
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
            this.numud_AnimationWindowSizeX.TabIndex = 8;
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
            this.label1.TabIndex = 1;
            this.label1.Text = "x";
            // 
            // btn_SaveManifest
            // 
            this.btn_SaveManifest.Location = new System.Drawing.Point(8, 196);
            this.btn_SaveManifest.Name = "btn_SaveManifest";
            this.btn_SaveManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_SaveManifest.TabIndex = 2;
            this.btn_SaveManifest.Text = "Save Alerts";
            this.btn_SaveManifest.UseVisualStyleBackColor = true;
            // 
            // btn_LoadManifest
            // 
            this.btn_LoadManifest.Location = new System.Drawing.Point(133, 196);
            this.btn_LoadManifest.Name = "btn_LoadManifest";
            this.btn_LoadManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_LoadManifest.TabIndex = 3;
            this.btn_LoadManifest.Text = "Load Alerts";
            this.btn_LoadManifest.UseVisualStyleBackColor = true;
            // 
            // btn_MergeManifest
            // 
            this.btn_MergeManifest.Location = new System.Drawing.Point(258, 196);
            this.btn_MergeManifest.Name = "btn_MergeManifest";
            this.btn_MergeManifest.Size = new System.Drawing.Size(120, 23);
            this.btn_MergeManifest.TabIndex = 4;
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
            this.dg_Alerts.TabIndex = 5;
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
            // Edit
            // 
            this.Edit.HeaderText = "New / Edit";
            this.Edit.MinimumWidth = 75;
            this.Edit.Name = "Edit";
            this.Edit.Text = "Edit";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 75;
            // 
            // alertBindingSource
            // 
            this.alertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlert);
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
            this.groupBox2.Location = new System.Drawing.Point(7, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 127);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Twitch Settings";
            // 
            // btn_LoadChannelDetails
            // 
            this.btn_LoadChannelDetails.Location = new System.Drawing.Point(326, 73);
            this.btn_LoadChannelDetails.Name = "btn_LoadChannelDetails";
            this.btn_LoadChannelDetails.Size = new System.Drawing.Size(156, 23);
            this.btn_LoadChannelDetails.TabIndex = 23;
            this.btn_LoadChannelDetails.Text = "Load Channel Details";
            this.btn_LoadChannelDetails.UseVisualStyleBackColor = true;
            // 
            // btn_SaveChannelDetails
            // 
            this.btn_SaveChannelDetails.Location = new System.Drawing.Point(10, 73);
            this.btn_SaveChannelDetails.Name = "btn_SaveChannelDetails";
            this.btn_SaveChannelDetails.Size = new System.Drawing.Size(156, 23);
            this.btn_SaveChannelDetails.TabIndex = 22;
            this.btn_SaveChannelDetails.Text = "Save Channel Details";
            this.btn_SaveChannelDetails.UseVisualStyleBackColor = true;
            // 
            // btn_RevokeAccessToken
            // 
            this.btn_RevokeAccessToken.Location = new System.Drawing.Point(372, 43);
            this.btn_RevokeAccessToken.Name = "btn_RevokeAccessToken";
            this.btn_RevokeAccessToken.Size = new System.Drawing.Size(110, 23);
            this.btn_RevokeAccessToken.TabIndex = 21;
            this.btn_RevokeAccessToken.Text = "Revoke Token";
            this.btn_RevokeAccessToken.UseVisualStyleBackColor = true;
            // 
            // txt_AccessToken
            // 
            this.txt_AccessToken.Location = new System.Drawing.Point(127, 44);
            this.txt_AccessToken.Name = "txt_AccessToken";
            this.txt_AccessToken.Size = new System.Drawing.Size(239, 20);
            this.txt_AccessToken.TabIndex = 20;
            this.txt_AccessToken.Text = "Paste Access Token Here";
            // 
            // btn_GetAccessToken
            // 
            this.btn_GetAccessToken.Location = new System.Drawing.Point(10, 43);
            this.btn_GetAccessToken.Name = "btn_GetAccessToken";
            this.btn_GetAccessToken.Size = new System.Drawing.Size(110, 23);
            this.btn_GetAccessToken.TabIndex = 19;
            this.btn_GetAccessToken.Text = "Get Access Token";
            this.btn_GetAccessToken.UseVisualStyleBackColor = true;
            // 
            // txt_ChannelName
            // 
            this.txt_ChannelName.Location = new System.Drawing.Point(127, 17);
            this.txt_ChannelName.Name = "txt_ChannelName";
            this.txt_ChannelName.Size = new System.Drawing.Size(355, 20);
            this.txt_ChannelName.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Channel Name";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(383, 196);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(120, 23);
            this.btn_Connect.TabIndex = 17;
            this.btn_Connect.Text = "Connect!";
            this.btn_Connect.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Manifest);
            this.tabControl1.Controls.Add(this.tab_Other);
            this.tabControl1.Controls.Add(this.tab_Banned);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(516, 254);
            this.tabControl1.TabIndex = 19;
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
            // tab_Other
            // 
            this.tab_Other.Controls.Add(this.groupBox1);
            this.tab_Other.Controls.Add(this.groupBox2);
            this.tab_Other.Location = new System.Drawing.Point(4, 22);
            this.tab_Other.Name = "tab_Other";
            this.tab_Other.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Other.Size = new System.Drawing.Size(508, 228);
            this.tab_Other.TabIndex = 1;
            this.tab_Other.Text = "Animation Window and Twitch Settings";
            this.tab_Other.UseVisualStyleBackColor = true;
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
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 82);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Window Settings";
            // 
            // btn_ColourPicker
            // 
            this.btn_ColourPicker.Image = ((System.Drawing.Image)(resources.GetObject("btn_ColourPicker.Image")));
            this.btn_ColourPicker.Location = new System.Drawing.Point(184, 23);
            this.btn_ColourPicker.Name = "btn_ColourPicker";
            this.btn_ColourPicker.Size = new System.Drawing.Size(22, 23);
            this.btn_ColourPicker.TabIndex = 16;
            this.btn_ColourPicker.UseVisualStyleBackColor = true;
            // 
            // tab_Banned
            // 
            this.tab_Banned.Controls.Add(this.checkBox3);
            this.tab_Banned.Controls.Add(this.checkBox2);
            this.tab_Banned.Controls.Add(this.checkBox1);
            this.tab_Banned.Controls.Add(this.textBox1);
            this.tab_Banned.Controls.Add(this.label9);
            this.tab_Banned.Controls.Add(this.label8);
            this.tab_Banned.Location = new System.Drawing.Point(4, 22);
            this.tab_Banned.Name = "tab_Banned";
            this.tab_Banned.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Banned.Size = new System.Drawing.Size(508, 228);
            this.tab_Banned.TabIndex = 2;
            this.tab_Banned.Text = "Banned Words";
            this.tab_Banned.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(119, 159);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(118, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Don\'t Read Emotes";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(119, 205);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(193, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Block my Cheermote (Partners only)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(119, 182);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Block Cheermotes";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(119, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(367, 59);
            this.textBox1.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(6, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 32);
            this.label9.TabIndex = 1;
            this.label9.Text = "Banned Terms (comma separated)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(47, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(401, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Banned words will be removed from all viewer messages, and will not be read aloud" +
    ".";
            // 
            // rad_30FPS
            // 
            this.rad_30FPS.AutoSize = true;
            this.rad_30FPS.Checked = true;
            this.rad_30FPS.Location = new System.Drawing.Point(392, 269);
            this.rad_30FPS.Name = "rad_30FPS";
            this.rad_30FPS.Size = new System.Drawing.Size(60, 17);
            this.rad_30FPS.TabIndex = 20;
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
            this.rad_60FPS.TabIndex = 21;
            this.rad_60FPS.Text = "60 FPS";
            this.rad_60FPS.UseVisualStyleBackColor = true;
            // 
            // MainTADAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 291);
            this.Controls.Add(this.rad_60FPS);
            this.Controls.Add(this.rad_30FPS);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainTADAForm";
            this.Text = "TADA Advanced Desktop Alerts";
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tab_Manifest.ResumeLayout(false);
            this.tab_Other.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tab_Banned.ResumeLayout(false);
            this.tab_Banned.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Manifest;
        private System.Windows.Forms.TabPage tab_Other;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ColourPicker;
        private System.Windows.Forms.TabPage tab_Banned;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox3;
        internal System.Windows.Forms.RadioButton rad_30FPS;
        internal System.Windows.Forms.RadioButton rad_60FPS;
    }
}

