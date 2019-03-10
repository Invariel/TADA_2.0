namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    partial class Alert_Designer_MDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Designer_MDI));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewImageVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewStreamerAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewStreamerText = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewViewerText = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewCondition = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grp_AlertSettings = new System.Windows.Forms.GroupBox();
            this.txt_AlertName = new System.Windows.Forms.TextBox();
            this.lbl_AlertName = new System.Windows.Forms.Label();
            this.grp_Macro = new System.Windows.Forms.GroupBox();
            this.cmb_MacroKey = new System.Windows.Forms.ComboBox();
            this.chk_MacroCtrl = new System.Windows.Forms.CheckBox();
            this.cml_MacroKey = new System.Windows.Forms.Label();
            this.chk_MacroAlt = new System.Windows.Forms.CheckBox();
            this.chk_MacroShift = new System.Windows.Forms.CheckBox();
            this.lbl_AlertType = new System.Windows.Forms.Label();
            this.cmb_AlertType = new System.Windows.Forms.ComboBox();
            this.grp_Preview = new System.Windows.Forms.GroupBox();
            this.cmb_Cooldowns = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxt_GlobalCooldown = new System.Windows.Forms.MaskedTextBox();
            this.mtxt_UserCooldown = new System.Windows.Forms.MaskedTextBox();
            this.lbl_UserCooldown = new System.Windows.Forms.Label();
            this.grp_AlertDetails = new System.Windows.Forms.GroupBox();
            this.lbl_MaximumDurationCount = new System.Windows.Forms.Label();
            this.lbl_CombinationsCount = new System.Windows.Forms.Label();
            this.lbl_MinimumDurationCount = new System.Windows.Forms.Label();
            this.lbl_Combinations = new System.Windows.Forms.Label();
            this.lbl_MaximumDuration = new System.Windows.Forms.Label();
            this.lbl_MinimumDuration = new System.Windows.Forms.Label();
            this.grp_PreviewControls = new System.Windows.Forms.GroupBox();
            this.numud_PreviewViewer = new System.Windows.Forms.NumericUpDown();
            this.numud_PreviewStreamer = new System.Windows.Forms.NumericUpDown();
            this.numud_PreviewImage = new System.Windows.Forms.NumericUpDown();
            this.chk_PreviewViewerText = new System.Windows.Forms.CheckBox();
            this.chk_PreviewStreamerText = new System.Windows.Forms.CheckBox();
            this.chk_PreviewImage = new System.Windows.Forms.CheckBox();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.trk_Time = new System.Windows.Forms.TrackBar();
            this.pb_Preview = new System.Windows.Forms.PictureBox();
            this.tab_AlertPieces = new System.Windows.Forms.TabControl();
            this.tab_Image = new System.Windows.Forms.TabPage();
            this.dg_Images = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dimensionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Images_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.alertImageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tab_Audio = new System.Windows.Forms.TabPage();
            this.dg_Audio = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Audio_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.alertAudioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tab_Streamer = new System.Windows.Forms.TabPage();
            this.dg_StreamerMessages = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Your_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.streamerAlertTextBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tab_Viewer = new System.Windows.Forms.TabPage();
            this.dg_ViewerMessages = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Viewer_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.viewerAlertTextBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tab_Conditions = new System.Windows.Forms.TabPage();
            this.dg_Conditions = new System.Windows.Forms.DataGridView();
            this.alertConditionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tab_PreviewControls = new System.Windows.Forms.TabPage();
            this.txt_ViewerMessage = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numud_Amount = new System.Windows.Forms.NumericUpDown();
            this.dom_SubTier = new System.Windows.Forms.DomainUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_SubPlan = new System.Windows.Forms.Label();
            this.txt_SubTierName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_RecipientName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ViewerName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_TestAlert = new System.Windows.Forms.Button();
            this.numud_TestViewerMessage = new System.Windows.Forms.NumericUpDown();
            this.numud_TestImage = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numud_TestStreamerMessage = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numud_TestAudio = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tab_ChildMDIForms = new System.Windows.Forms.TabControl();
            this.waypointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.grp_AlertSettings.SuspendLayout();
            this.grp_Macro.SuspendLayout();
            this.grp_Preview.SuspendLayout();
            this.cmb_Cooldowns.SuspendLayout();
            this.grp_AlertDetails.SuspendLayout();
            this.grp_PreviewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewStreamer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).BeginInit();
            this.tab_AlertPieces.SuspendLayout();
            this.tab_Image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertImageBindingSource)).BeginInit();
            this.tab_Audio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Audio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertAudioBindingSource)).BeginInit();
            this.tab_Streamer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_StreamerMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streamerAlertTextBindingSource)).BeginInit();
            this.tab_Viewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_ViewerMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewerAlertTextBindingSource)).BeginInit();
            this.tab_Conditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Conditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertConditionBindingSource)).BeginInit();
            this.tab_PreviewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_Amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestViewerMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestStreamerMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(766, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_NewImageVideo,
            this.ts_NewStreamerAudio,
            this.ts_NewStreamerText,
            this.ts_NewViewerText,
            this.ts_NewCondition,
            this.toolStripSeparator5,
            this.ts_Exit});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // ts_NewImageVideo
            // 
            this.ts_NewImageVideo.Image = ((System.Drawing.Image)(resources.GetObject("ts_NewImageVideo.Image")));
            this.ts_NewImageVideo.ImageTransparentColor = System.Drawing.Color.Black;
            this.ts_NewImageVideo.Name = "ts_NewImageVideo";
            this.ts_NewImageVideo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.ts_NewImageVideo.Size = new System.Drawing.Size(212, 22);
            this.ts_NewImageVideo.Text = "New &Image/Video";
            // 
            // ts_NewStreamerAudio
            // 
            this.ts_NewStreamerAudio.Name = "ts_NewStreamerAudio";
            this.ts_NewStreamerAudio.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.ts_NewStreamerAudio.Size = new System.Drawing.Size(212, 22);
            this.ts_NewStreamerAudio.Text = "New A&udio";
            // 
            // ts_NewStreamerText
            // 
            this.ts_NewStreamerText.Name = "ts_NewStreamerText";
            this.ts_NewStreamerText.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ts_NewStreamerText.Size = new System.Drawing.Size(212, 22);
            this.ts_NewStreamerText.Text = "New &Streamer Text";
            // 
            // ts_NewViewerText
            // 
            this.ts_NewViewerText.Name = "ts_NewViewerText";
            this.ts_NewViewerText.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ts_NewViewerText.Size = new System.Drawing.Size(212, 22);
            this.ts_NewViewerText.Text = "New Vi&ewer Text";
            // 
            // ts_NewCondition
            // 
            this.ts_NewCondition.Name = "ts_NewCondition";
            this.ts_NewCondition.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ts_NewCondition.Size = new System.Drawing.Size(212, 22);
            this.ts_NewCondition.Text = "New C&ondition";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(209, 6);
            // 
            // ts_Exit
            // 
            this.ts_Exit.Name = "ts_Exit";
            this.ts_Exit.Size = new System.Drawing.Size(212, 22);
            this.ts_Exit.Text = "E&xit";
            this.ts_Exit.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(165, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.aboutToolStripMenuItem.Text = "&About ... ...";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 659);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(766, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // grp_AlertSettings
            // 
            this.grp_AlertSettings.Controls.Add(this.txt_AlertName);
            this.grp_AlertSettings.Controls.Add(this.lbl_AlertName);
            this.grp_AlertSettings.Controls.Add(this.grp_Macro);
            this.grp_AlertSettings.Controls.Add(this.lbl_AlertType);
            this.grp_AlertSettings.Controls.Add(this.cmb_AlertType);
            this.grp_AlertSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_AlertSettings.Location = new System.Drawing.Point(0, 24);
            this.grp_AlertSettings.Name = "grp_AlertSettings";
            this.grp_AlertSettings.Size = new System.Drawing.Size(766, 55);
            this.grp_AlertSettings.TabIndex = 1;
            this.grp_AlertSettings.TabStop = false;
            this.grp_AlertSettings.Text = "Alert Settings";
            // 
            // txt_AlertName
            // 
            this.txt_AlertName.Location = new System.Drawing.Point(328, 18);
            this.txt_AlertName.Name = "txt_AlertName";
            this.txt_AlertName.Size = new System.Drawing.Size(134, 20);
            this.txt_AlertName.TabIndex = 14;
            // 
            // lbl_AlertName
            // 
            this.lbl_AlertName.AutoSize = true;
            this.lbl_AlertName.Location = new System.Drawing.Point(266, 22);
            this.lbl_AlertName.Name = "lbl_AlertName";
            this.lbl_AlertName.Size = new System.Drawing.Size(59, 13);
            this.lbl_AlertName.TabIndex = 13;
            this.lbl_AlertName.Text = "Alert Name";
            // 
            // grp_Macro
            // 
            this.grp_Macro.Controls.Add(this.cmb_MacroKey);
            this.grp_Macro.Controls.Add(this.chk_MacroCtrl);
            this.grp_Macro.Controls.Add(this.cml_MacroKey);
            this.grp_Macro.Controls.Add(this.chk_MacroAlt);
            this.grp_Macro.Controls.Add(this.chk_MacroShift);
            this.grp_Macro.Location = new System.Drawing.Point(477, 7);
            this.grp_Macro.Name = "grp_Macro";
            this.grp_Macro.Padding = new System.Windows.Forms.Padding(0);
            this.grp_Macro.Size = new System.Drawing.Size(273, 42);
            this.grp_Macro.TabIndex = 15;
            this.grp_Macro.TabStop = false;
            this.grp_Macro.Text = "Macro Settings";
            this.grp_Macro.Visible = false;
            // 
            // cmb_MacroKey
            // 
            this.cmb_MacroKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MacroKey.FormattingEnabled = true;
            this.cmb_MacroKey.Location = new System.Drawing.Point(197, 11);
            this.cmb_MacroKey.Name = "cmb_MacroKey";
            this.cmb_MacroKey.Size = new System.Drawing.Size(66, 21);
            this.cmb_MacroKey.TabIndex = 9;
            // 
            // chk_MacroCtrl
            // 
            this.chk_MacroCtrl.AutoSize = true;
            this.chk_MacroCtrl.Location = new System.Drawing.Point(12, 14);
            this.chk_MacroCtrl.Name = "chk_MacroCtrl";
            this.chk_MacroCtrl.Size = new System.Drawing.Size(41, 17);
            this.chk_MacroCtrl.TabIndex = 5;
            this.chk_MacroCtrl.Text = "Ctrl";
            this.chk_MacroCtrl.UseVisualStyleBackColor = true;
            // 
            // cml_MacroKey
            // 
            this.cml_MacroKey.AutoSize = true;
            this.cml_MacroKey.Location = new System.Drawing.Point(169, 15);
            this.cml_MacroKey.Name = "cml_MacroKey";
            this.cml_MacroKey.Size = new System.Drawing.Size(25, 13);
            this.cml_MacroKey.TabIndex = 8;
            this.cml_MacroKey.Text = "Key";
            // 
            // chk_MacroAlt
            // 
            this.chk_MacroAlt.AutoSize = true;
            this.chk_MacroAlt.Location = new System.Drawing.Point(64, 14);
            this.chk_MacroAlt.Name = "chk_MacroAlt";
            this.chk_MacroAlt.Size = new System.Drawing.Size(38, 17);
            this.chk_MacroAlt.TabIndex = 6;
            this.chk_MacroAlt.Text = "Alt";
            this.chk_MacroAlt.UseVisualStyleBackColor = true;
            // 
            // chk_MacroShift
            // 
            this.chk_MacroShift.AutoSize = true;
            this.chk_MacroShift.Location = new System.Drawing.Point(113, 14);
            this.chk_MacroShift.Name = "chk_MacroShift";
            this.chk_MacroShift.Size = new System.Drawing.Size(47, 17);
            this.chk_MacroShift.TabIndex = 7;
            this.chk_MacroShift.Text = "Shift";
            this.chk_MacroShift.UseVisualStyleBackColor = true;
            // 
            // lbl_AlertType
            // 
            this.lbl_AlertType.AutoSize = true;
            this.lbl_AlertType.Location = new System.Drawing.Point(6, 22);
            this.lbl_AlertType.Name = "lbl_AlertType";
            this.lbl_AlertType.Size = new System.Drawing.Size(55, 13);
            this.lbl_AlertType.TabIndex = 11;
            this.lbl_AlertType.Text = "Alert Type";
            // 
            // cmb_AlertType
            // 
            this.cmb_AlertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_AlertType.DropDownWidth = 225;
            this.cmb_AlertType.FormattingEnabled = true;
            this.cmb_AlertType.Location = new System.Drawing.Point(68, 18);
            this.cmb_AlertType.Name = "cmb_AlertType";
            this.cmb_AlertType.Size = new System.Drawing.Size(178, 21);
            this.cmb_AlertType.TabIndex = 12;
            // 
            // grp_Preview
            // 
            this.grp_Preview.Controls.Add(this.cmb_Cooldowns);
            this.grp_Preview.Controls.Add(this.grp_AlertDetails);
            this.grp_Preview.Controls.Add(this.grp_PreviewControls);
            this.grp_Preview.Controls.Add(this.lbl_Time);
            this.grp_Preview.Controls.Add(this.trk_Time);
            this.grp_Preview.Controls.Add(this.pb_Preview);
            this.grp_Preview.Dock = System.Windows.Forms.DockStyle.Right;
            this.grp_Preview.Location = new System.Drawing.Point(766, 0);
            this.grp_Preview.Name = "grp_Preview";
            this.grp_Preview.Size = new System.Drawing.Size(523, 681);
            this.grp_Preview.TabIndex = 220;
            this.grp_Preview.TabStop = false;
            this.grp_Preview.Text = "Path Preview";
            // 
            // cmb_Cooldowns
            // 
            this.cmb_Cooldowns.Controls.Add(this.label1);
            this.cmb_Cooldowns.Controls.Add(this.mtxt_GlobalCooldown);
            this.cmb_Cooldowns.Controls.Add(this.mtxt_UserCooldown);
            this.cmb_Cooldowns.Controls.Add(this.lbl_UserCooldown);
            this.cmb_Cooldowns.Location = new System.Drawing.Point(6, 520);
            this.cmb_Cooldowns.Name = "cmb_Cooldowns";
            this.cmb_Cooldowns.Size = new System.Drawing.Size(511, 75);
            this.cmb_Cooldowns.TabIndex = 219;
            this.cmb_Cooldowns.TabStop = false;
            this.cmb_Cooldowns.Text = "Alert Cooldowns";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "must pass before anyone can trigger this alert again.";
            // 
            // mtxt_GlobalCooldown
            // 
            this.mtxt_GlobalCooldown.Location = new System.Drawing.Point(7, 44);
            this.mtxt_GlobalCooldown.Mask = "###:##:##.###";
            this.mtxt_GlobalCooldown.Name = "mtxt_GlobalCooldown";
            this.mtxt_GlobalCooldown.PromptChar = '0';
            this.mtxt_GlobalCooldown.Size = new System.Drawing.Size(79, 20);
            this.mtxt_GlobalCooldown.TabIndex = 3;
            this.mtxt_GlobalCooldown.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // mtxt_UserCooldown
            // 
            this.mtxt_UserCooldown.Location = new System.Drawing.Point(7, 17);
            this.mtxt_UserCooldown.Mask = "###:##:##.###";
            this.mtxt_UserCooldown.Name = "mtxt_UserCooldown";
            this.mtxt_UserCooldown.PromptChar = '0';
            this.mtxt_UserCooldown.Size = new System.Drawing.Size(79, 20);
            this.mtxt_UserCooldown.TabIndex = 2;
            this.mtxt_UserCooldown.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // lbl_UserCooldown
            // 
            this.lbl_UserCooldown.AutoSize = true;
            this.lbl_UserCooldown.Location = new System.Drawing.Point(92, 21);
            this.lbl_UserCooldown.Name = "lbl_UserCooldown";
            this.lbl_UserCooldown.Size = new System.Drawing.Size(295, 13);
            this.lbl_UserCooldown.TabIndex = 1;
            this.lbl_UserCooldown.Text = "must pass before the same person can trigger this alert again.";
            // 
            // grp_AlertDetails
            // 
            this.grp_AlertDetails.Controls.Add(this.lbl_MaximumDurationCount);
            this.grp_AlertDetails.Controls.Add(this.lbl_CombinationsCount);
            this.grp_AlertDetails.Controls.Add(this.lbl_MinimumDurationCount);
            this.grp_AlertDetails.Controls.Add(this.lbl_Combinations);
            this.grp_AlertDetails.Controls.Add(this.lbl_MaximumDuration);
            this.grp_AlertDetails.Controls.Add(this.lbl_MinimumDuration);
            this.grp_AlertDetails.Location = new System.Drawing.Point(6, 601);
            this.grp_AlertDetails.Name = "grp_AlertDetails";
            this.grp_AlertDetails.Size = new System.Drawing.Size(511, 74);
            this.grp_AlertDetails.TabIndex = 218;
            this.grp_AlertDetails.TabStop = false;
            this.grp_AlertDetails.Text = "Alert Details";
            // 
            // lbl_MaximumDurationCount
            // 
            this.lbl_MaximumDurationCount.AutoSize = true;
            this.lbl_MaximumDurationCount.Location = new System.Drawing.Point(444, 20);
            this.lbl_MaximumDurationCount.Name = "lbl_MaximumDurationCount";
            this.lbl_MaximumDurationCount.Size = new System.Drawing.Size(21, 13);
            this.lbl_MaximumDurationCount.TabIndex = 5;
            this.lbl_MaximumDurationCount.Text = "0 s";
            // 
            // lbl_CombinationsCount
            // 
            this.lbl_CombinationsCount.AutoSize = true;
            this.lbl_CombinationsCount.Location = new System.Drawing.Point(141, 50);
            this.lbl_CombinationsCount.Name = "lbl_CombinationsCount";
            this.lbl_CombinationsCount.Size = new System.Drawing.Size(13, 13);
            this.lbl_CombinationsCount.TabIndex = 4;
            this.lbl_CombinationsCount.Text = "0";
            // 
            // lbl_MinimumDurationCount
            // 
            this.lbl_MinimumDurationCount.AutoSize = true;
            this.lbl_MinimumDurationCount.Location = new System.Drawing.Point(141, 20);
            this.lbl_MinimumDurationCount.Name = "lbl_MinimumDurationCount";
            this.lbl_MinimumDurationCount.Size = new System.Drawing.Size(21, 13);
            this.lbl_MinimumDurationCount.TabIndex = 3;
            this.lbl_MinimumDurationCount.Text = "0 s";
            // 
            // lbl_Combinations
            // 
            this.lbl_Combinations.AutoSize = true;
            this.lbl_Combinations.Location = new System.Drawing.Point(7, 50);
            this.lbl_Combinations.Name = "lbl_Combinations";
            this.lbl_Combinations.Size = new System.Drawing.Size(124, 13);
            this.lbl_Combinations.TabIndex = 2;
            this.lbl_Combinations.Text = "Total Alert Combinations:";
            // 
            // lbl_MaximumDuration
            // 
            this.lbl_MaximumDuration.AutoSize = true;
            this.lbl_MaximumDuration.Location = new System.Drawing.Point(316, 20);
            this.lbl_MaximumDuration.Name = "lbl_MaximumDuration";
            this.lbl_MaximumDuration.Size = new System.Drawing.Size(121, 13);
            this.lbl_MaximumDuration.TabIndex = 1;
            this.lbl_MaximumDuration.Text = "Maximum Alert Duration:";
            // 
            // lbl_MinimumDuration
            // 
            this.lbl_MinimumDuration.AutoSize = true;
            this.lbl_MinimumDuration.Location = new System.Drawing.Point(7, 20);
            this.lbl_MinimumDuration.Name = "lbl_MinimumDuration";
            this.lbl_MinimumDuration.Size = new System.Drawing.Size(118, 13);
            this.lbl_MinimumDuration.TabIndex = 0;
            this.lbl_MinimumDuration.Text = "Minimum Alert Duration:";
            // 
            // grp_PreviewControls
            // 
            this.grp_PreviewControls.Controls.Add(this.numud_PreviewViewer);
            this.grp_PreviewControls.Controls.Add(this.numud_PreviewStreamer);
            this.grp_PreviewControls.Controls.Add(this.numud_PreviewImage);
            this.grp_PreviewControls.Controls.Add(this.chk_PreviewViewerText);
            this.grp_PreviewControls.Controls.Add(this.chk_PreviewStreamerText);
            this.grp_PreviewControls.Controls.Add(this.chk_PreviewImage);
            this.grp_PreviewControls.Location = new System.Drawing.Point(6, 355);
            this.grp_PreviewControls.Name = "grp_PreviewControls";
            this.grp_PreviewControls.Size = new System.Drawing.Size(511, 55);
            this.grp_PreviewControls.TabIndex = 217;
            this.grp_PreviewControls.TabStop = false;
            this.grp_PreviewControls.Text = "Preview Controls";
            // 
            // numud_PreviewViewer
            // 
            this.numud_PreviewViewer.Location = new System.Drawing.Point(442, 20);
            this.numud_PreviewViewer.Name = "numud_PreviewViewer";
            this.numud_PreviewViewer.Size = new System.Drawing.Size(63, 20);
            this.numud_PreviewViewer.TabIndex = 218;
            // 
            // numud_PreviewStreamer
            // 
            this.numud_PreviewStreamer.Location = new System.Drawing.Point(258, 20);
            this.numud_PreviewStreamer.Name = "numud_PreviewStreamer";
            this.numud_PreviewStreamer.Size = new System.Drawing.Size(63, 20);
            this.numud_PreviewStreamer.TabIndex = 218;
            // 
            // numud_PreviewImage
            // 
            this.numud_PreviewImage.Location = new System.Drawing.Point(65, 20);
            this.numud_PreviewImage.Name = "numud_PreviewImage";
            this.numud_PreviewImage.Size = new System.Drawing.Size(63, 20);
            this.numud_PreviewImage.TabIndex = 218;
            // 
            // chk_PreviewViewerText
            // 
            this.chk_PreviewViewerText.AutoSize = true;
            this.chk_PreviewViewerText.Location = new System.Drawing.Point(357, 22);
            this.chk_PreviewViewerText.Name = "chk_PreviewViewerText";
            this.chk_PreviewViewerText.Size = new System.Drawing.Size(82, 17);
            this.chk_PreviewViewerText.TabIndex = 219;
            this.chk_PreviewViewerText.Text = "Viewer Text";
            this.chk_PreviewViewerText.UseVisualStyleBackColor = true;
            // 
            // chk_PreviewStreamerText
            // 
            this.chk_PreviewStreamerText.AutoSize = true;
            this.chk_PreviewStreamerText.Location = new System.Drawing.Point(163, 22);
            this.chk_PreviewStreamerText.Name = "chk_PreviewStreamerText";
            this.chk_PreviewStreamerText.Size = new System.Drawing.Size(92, 17);
            this.chk_PreviewStreamerText.TabIndex = 218;
            this.chk_PreviewStreamerText.Text = "Streamer Text";
            this.chk_PreviewStreamerText.UseVisualStyleBackColor = true;
            // 
            // chk_PreviewImage
            // 
            this.chk_PreviewImage.AutoSize = true;
            this.chk_PreviewImage.Location = new System.Drawing.Point(7, 22);
            this.chk_PreviewImage.Name = "chk_PreviewImage";
            this.chk_PreviewImage.Size = new System.Drawing.Size(55, 17);
            this.chk_PreviewImage.TabIndex = 217;
            this.chk_PreviewImage.Text = "Image";
            this.chk_PreviewImage.UseVisualStyleBackColor = true;
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Location = new System.Drawing.Point(478, 320);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(18, 13);
            this.lbl_Time.TabIndex = 213;
            this.lbl_Time.Text = "0s";
            // 
            // trk_Time
            // 
            this.trk_Time.LargeChange = 50;
            this.trk_Time.Location = new System.Drawing.Point(8, 304);
            this.trk_Time.Maximum = 1000;
            this.trk_Time.Name = "trk_Time";
            this.trk_Time.Size = new System.Drawing.Size(458, 45);
            this.trk_Time.TabIndex = 212;
            this.trk_Time.TickFrequency = 25;
            // 
            // pb_Preview
            // 
            this.pb_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_Preview.Location = new System.Drawing.Point(6, 15);
            this.pb_Preview.Name = "pb_Preview";
            this.pb_Preview.Size = new System.Drawing.Size(512, 288);
            this.pb_Preview.TabIndex = 3;
            this.pb_Preview.TabStop = false;
            // 
            // tab_AlertPieces
            // 
            this.tab_AlertPieces.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tab_AlertPieces.Controls.Add(this.tab_Image);
            this.tab_AlertPieces.Controls.Add(this.tab_Audio);
            this.tab_AlertPieces.Controls.Add(this.tab_Streamer);
            this.tab_AlertPieces.Controls.Add(this.tab_Viewer);
            this.tab_AlertPieces.Controls.Add(this.tab_Conditions);
            this.tab_AlertPieces.Controls.Add(this.tab_PreviewControls);
            this.tab_AlertPieces.Dock = System.Windows.Forms.DockStyle.Top;
            this.tab_AlertPieces.Location = new System.Drawing.Point(0, 79);
            this.tab_AlertPieces.Margin = new System.Windows.Forms.Padding(0);
            this.tab_AlertPieces.Name = "tab_AlertPieces";
            this.tab_AlertPieces.Padding = new System.Drawing.Point(0, 0);
            this.tab_AlertPieces.SelectedIndex = 0;
            this.tab_AlertPieces.Size = new System.Drawing.Size(766, 146);
            this.tab_AlertPieces.TabIndex = 222;
            // 
            // tab_Image
            // 
            this.tab_Image.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tab_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_Image.Controls.Add(this.dg_Images);
            this.tab_Image.Location = new System.Drawing.Point(4, 25);
            this.tab_Image.Margin = new System.Windows.Forms.Padding(0);
            this.tab_Image.Name = "tab_Image";
            this.tab_Image.Size = new System.Drawing.Size(758, 117);
            this.tab_Image.TabIndex = 0;
            this.tab_Image.Text = "Images and Video";
            // 
            // dg_Images
            // 
            this.dg_Images.AutoGenerateColumns = false;
            this.dg_Images.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_Images.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Images.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dimensionsDataGridViewTextBoxColumn,
            this.dataGridViewCheckBoxColumn1,
            this.Images_Edit});
            this.dg_Images.DataSource = this.alertImageBindingSource;
            this.dg_Images.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Images.Location = new System.Drawing.Point(0, 0);
            this.dg_Images.MultiSelect = false;
            this.dg_Images.Name = "dg_Images";
            this.dg_Images.ReadOnly = true;
            this.dg_Images.Size = new System.Drawing.Size(756, 115);
            this.dg_Images.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Filename";
            this.dataGridViewTextBoxColumn1.HeaderText = "Filename";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dimensionsDataGridViewTextBoxColumn
            // 
            this.dimensionsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dimensionsDataGridViewTextBoxColumn.DataPropertyName = "Dimensions";
            this.dimensionsDataGridViewTextBoxColumn.HeaderText = "Size";
            this.dimensionsDataGridViewTextBoxColumn.MinimumWidth = 75;
            this.dimensionsDataGridViewTextBoxColumn.Name = "dimensionsDataGridViewTextBoxColumn";
            this.dimensionsDataGridViewTextBoxColumn.ReadOnly = true;
            this.dimensionsDataGridViewTextBoxColumn.Width = 75;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsVideo";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Video ?";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Width = 49;
            // 
            // Images_Edit
            // 
            this.Images_Edit.HeaderText = "New / Edit";
            this.Images_Edit.MinimumWidth = 75;
            this.Images_Edit.Name = "Images_Edit";
            this.Images_Edit.ReadOnly = true;
            this.Images_Edit.Text = "Edit";
            this.Images_Edit.ToolTipText = "Create a new image/video alert piece, or edit an existing one.";
            this.Images_Edit.UseColumnTextForButtonValue = true;
            this.Images_Edit.Width = 75;
            // 
            // alertImageBindingSource
            // 
            this.alertImageBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlertImage);
            // 
            // tab_Audio
            // 
            this.tab_Audio.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Audio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_Audio.Controls.Add(this.dg_Audio);
            this.tab_Audio.Location = new System.Drawing.Point(4, 25);
            this.tab_Audio.Margin = new System.Windows.Forms.Padding(0);
            this.tab_Audio.Name = "tab_Audio";
            this.tab_Audio.Size = new System.Drawing.Size(758, 117);
            this.tab_Audio.TabIndex = 1;
            this.tab_Audio.Text = "Audio";
            // 
            // dg_Audio
            // 
            this.dg_Audio.AutoGenerateColumns = false;
            this.dg_Audio.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_Audio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Audio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.Audio_Edit});
            this.dg_Audio.DataSource = this.alertAudioBindingSource;
            this.dg_Audio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Audio.Location = new System.Drawing.Point(0, 0);
            this.dg_Audio.Name = "dg_Audio";
            this.dg_Audio.Size = new System.Drawing.Size(756, 115);
            this.dg_Audio.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Filename";
            this.dataGridViewTextBoxColumn2.HeaderText = "Filename";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Audio_Edit
            // 
            this.Audio_Edit.HeaderText = "New / Edit";
            this.Audio_Edit.MinimumWidth = 75;
            this.Audio_Edit.Name = "Audio_Edit";
            this.Audio_Edit.Text = "Edit";
            this.Audio_Edit.ToolTipText = "Create a new audio piece or edit an existing one.";
            this.Audio_Edit.UseColumnTextForButtonValue = true;
            this.Audio_Edit.Width = 75;
            // 
            // alertAudioBindingSource
            // 
            this.alertAudioBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlertAudio);
            // 
            // tab_Streamer
            // 
            this.tab_Streamer.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Streamer.Controls.Add(this.dg_StreamerMessages);
            this.tab_Streamer.Location = new System.Drawing.Point(4, 25);
            this.tab_Streamer.Margin = new System.Windows.Forms.Padding(0);
            this.tab_Streamer.Name = "tab_Streamer";
            this.tab_Streamer.Size = new System.Drawing.Size(758, 117);
            this.tab_Streamer.TabIndex = 2;
            this.tab_Streamer.Text = "Streamer\'s Text";
            // 
            // dg_StreamerMessages
            // 
            this.dg_StreamerMessages.AutoGenerateColumns = false;
            this.dg_StreamerMessages.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_StreamerMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_StreamerMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Your_Edit});
            this.dg_StreamerMessages.DataSource = this.streamerAlertTextBindingSource;
            this.dg_StreamerMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_StreamerMessages.Location = new System.Drawing.Point(0, 0);
            this.dg_StreamerMessages.Name = "dg_StreamerMessages";
            this.dg_StreamerMessages.Size = new System.Drawing.Size(758, 117);
            this.dg_StreamerMessages.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Message";
            this.dataGridViewTextBoxColumn3.HeaderText = "Displayed Message";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ReadMessage";
            this.dataGridViewTextBoxColumn4.HeaderText = "Narrated Message";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // Your_Edit
            // 
            this.Your_Edit.HeaderText = "New / Edit";
            this.Your_Edit.MinimumWidth = 75;
            this.Your_Edit.Name = "Your_Edit";
            this.Your_Edit.Text = "Edit";
            this.Your_Edit.ToolTipText = "Create a new piece of displayed text, or edit an existing one.";
            this.Your_Edit.UseColumnTextForButtonValue = true;
            this.Your_Edit.Width = 75;
            // 
            // streamerAlertTextBindingSource
            // 
            this.streamerAlertTextBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlertText);
            // 
            // tab_Viewer
            // 
            this.tab_Viewer.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Viewer.Controls.Add(this.dg_ViewerMessages);
            this.tab_Viewer.Location = new System.Drawing.Point(4, 25);
            this.tab_Viewer.Margin = new System.Windows.Forms.Padding(0);
            this.tab_Viewer.Name = "tab_Viewer";
            this.tab_Viewer.Size = new System.Drawing.Size(758, 117);
            this.tab_Viewer.TabIndex = 3;
            this.tab_Viewer.Text = "Viewer\'s Text";
            // 
            // dg_ViewerMessages
            // 
            this.dg_ViewerMessages.AutoGenerateColumns = false;
            this.dg_ViewerMessages.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_ViewerMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_ViewerMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.Viewer_Edit});
            this.dg_ViewerMessages.DataSource = this.viewerAlertTextBindingSource;
            this.dg_ViewerMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_ViewerMessages.Location = new System.Drawing.Point(0, 0);
            this.dg_ViewerMessages.Name = "dg_ViewerMessages";
            this.dg_ViewerMessages.Size = new System.Drawing.Size(758, 117);
            this.dg_ViewerMessages.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Message";
            this.dataGridViewTextBoxColumn5.HeaderText = "Displayed Message";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ReadMessage";
            this.dataGridViewTextBoxColumn6.HeaderText = "Narrated Message";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Viewer_Edit
            // 
            this.Viewer_Edit.HeaderText = "New / Edit";
            this.Viewer_Edit.MinimumWidth = 75;
            this.Viewer_Edit.Name = "Viewer_Edit";
            this.Viewer_Edit.Text = "Edit";
            this.Viewer_Edit.ToolTipText = "Create a new style for a viewer\'s text, or edit an existing one.";
            this.Viewer_Edit.UseColumnTextForButtonValue = true;
            this.Viewer_Edit.Width = 75;
            // 
            // viewerAlertTextBindingSource
            // 
            this.viewerAlertTextBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlertText);
            // 
            // tab_Conditions
            // 
            this.tab_Conditions.Controls.Add(this.dg_Conditions);
            this.tab_Conditions.Location = new System.Drawing.Point(4, 25);
            this.tab_Conditions.Name = "tab_Conditions";
            this.tab_Conditions.Size = new System.Drawing.Size(758, 117);
            this.tab_Conditions.TabIndex = 5;
            this.tab_Conditions.Text = "Trigger Conditions";
            this.tab_Conditions.UseVisualStyleBackColor = true;
            // 
            // dg_Conditions
            // 
            this.dg_Conditions.AutoGenerateColumns = false;
            this.dg_Conditions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Conditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.dg_Conditions.DataSource = this.alertConditionBindingSource;
            this.dg_Conditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Conditions.Location = new System.Drawing.Point(0, 0);
            this.dg_Conditions.Name = "dg_Conditions";
            this.dg_Conditions.Size = new System.Drawing.Size(758, 117);
            this.dg_Conditions.TabIndex = 1;
            // 
            // alertConditionBindingSource
            // 
            this.alertConditionBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlertCondition);
            // 
            // tab_PreviewControls
            // 
            this.tab_PreviewControls.BackColor = System.Drawing.SystemColors.Control;
            this.tab_PreviewControls.Controls.Add(this.txt_ViewerMessage);
            this.tab_PreviewControls.Controls.Add(this.label12);
            this.tab_PreviewControls.Controls.Add(this.numud_Amount);
            this.tab_PreviewControls.Controls.Add(this.dom_SubTier);
            this.tab_PreviewControls.Controls.Add(this.label11);
            this.tab_PreviewControls.Controls.Add(this.lbl_SubPlan);
            this.tab_PreviewControls.Controls.Add(this.txt_SubTierName);
            this.tab_PreviewControls.Controls.Add(this.label9);
            this.tab_PreviewControls.Controls.Add(this.txt_RecipientName);
            this.tab_PreviewControls.Controls.Add(this.label8);
            this.tab_PreviewControls.Controls.Add(this.txt_ViewerName);
            this.tab_PreviewControls.Controls.Add(this.label7);
            this.tab_PreviewControls.Controls.Add(this.btn_TestAlert);
            this.tab_PreviewControls.Controls.Add(this.numud_TestViewerMessage);
            this.tab_PreviewControls.Controls.Add(this.numud_TestImage);
            this.tab_PreviewControls.Controls.Add(this.label6);
            this.tab_PreviewControls.Controls.Add(this.numud_TestStreamerMessage);
            this.tab_PreviewControls.Controls.Add(this.label3);
            this.tab_PreviewControls.Controls.Add(this.numud_TestAudio);
            this.tab_PreviewControls.Controls.Add(this.label5);
            this.tab_PreviewControls.Controls.Add(this.label4);
            this.tab_PreviewControls.Location = new System.Drawing.Point(4, 25);
            this.tab_PreviewControls.Name = "tab_PreviewControls";
            this.tab_PreviewControls.Padding = new System.Windows.Forms.Padding(3);
            this.tab_PreviewControls.Size = new System.Drawing.Size(758, 117);
            this.tab_PreviewControls.TabIndex = 4;
            this.tab_PreviewControls.Text = "Preview Controls";
            // 
            // txt_ViewerMessage
            // 
            this.txt_ViewerMessage.Location = new System.Drawing.Point(310, 84);
            this.txt_ViewerMessage.Name = "txt_ViewerMessage";
            this.txt_ViewerMessage.Size = new System.Drawing.Size(333, 20);
            this.txt_ViewerMessage.TabIndex = 90;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(218, 88);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 89;
            this.label12.Text = "Viewer Message";
            // 
            // numud_Amount
            // 
            this.numud_Amount.Location = new System.Drawing.Point(562, 32);
            this.numud_Amount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numud_Amount.Name = "numud_Amount";
            this.numud_Amount.Size = new System.Drawing.Size(120, 20);
            this.numud_Amount.TabIndex = 94;
            // 
            // dom_SubTier
            // 
            this.dom_SubTier.Items.Add("No Tier");
            this.dom_SubTier.Items.Add("Twitch Prime");
            this.dom_SubTier.Items.Add("Tier One");
            this.dom_SubTier.Items.Add("Tier Two");
            this.dom_SubTier.Items.Add("Tier Three");
            this.dom_SubTier.Location = new System.Drawing.Point(562, 8);
            this.dom_SubTier.Name = "dom_SubTier";
            this.dom_SubTier.Size = new System.Drawing.Size(120, 20);
            this.dom_SubTier.TabIndex = 92;
            this.dom_SubTier.Text = "No Tier";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(466, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 93;
            this.label11.Text = "Relevant Amount";
            // 
            // lbl_SubPlan
            // 
            this.lbl_SubPlan.AutoSize = true;
            this.lbl_SubPlan.Location = new System.Drawing.Point(466, 12);
            this.lbl_SubPlan.Name = "lbl_SubPlan";
            this.lbl_SubPlan.Size = new System.Drawing.Size(92, 13);
            this.lbl_SubPlan.TabIndex = 91;
            this.lbl_SubPlan.Text = "Viewer\'s Sub Plan";
            // 
            // txt_SubTierName
            // 
            this.txt_SubTierName.Location = new System.Drawing.Point(310, 58);
            this.txt_SubTierName.Name = "txt_SubTierName";
            this.txt_SubTierName.Size = new System.Drawing.Size(127, 20);
            this.txt_SubTierName.TabIndex = 88;
            this.txt_SubTierName.Text = "Tier Awesome";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(225, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 87;
            this.label9.Text = "Sub Tier Name";
            // 
            // txt_RecipientName
            // 
            this.txt_RecipientName.Location = new System.Drawing.Point(310, 32);
            this.txt_RecipientName.Name = "txt_RecipientName";
            this.txt_RecipientName.Size = new System.Drawing.Size(127, 20);
            this.txt_RecipientName.TabIndex = 86;
            this.txt_RecipientName.Text = "Leiravni";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 85;
            this.label8.Text = "Sub Gift Recipient";
            // 
            // txt_ViewerName
            // 
            this.txt_ViewerName.Location = new System.Drawing.Point(310, 6);
            this.txt_ViewerName.Name = "txt_ViewerName";
            this.txt_ViewerName.Size = new System.Drawing.Size(127, 20);
            this.txt_ViewerName.TabIndex = 84;
            this.txt_ViewerName.Text = "Invariel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 83;
            this.label7.Text = "Viewer Name";
            // 
            // btn_TestAlert
            // 
            this.btn_TestAlert.Location = new System.Drawing.Point(664, 82);
            this.btn_TestAlert.Name = "btn_TestAlert";
            this.btn_TestAlert.Size = new System.Drawing.Size(82, 23);
            this.btn_TestAlert.TabIndex = 110;
            this.btn_TestAlert.Text = "Test Alert";
            this.btn_TestAlert.UseVisualStyleBackColor = true;
            // 
            // numud_TestViewerMessage
            // 
            this.numud_TestViewerMessage.Location = new System.Drawing.Point(107, 84);
            this.numud_TestViewerMessage.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numud_TestViewerMessage.Name = "numud_TestViewerMessage";
            this.numud_TestViewerMessage.Size = new System.Drawing.Size(82, 20);
            this.numud_TestViewerMessage.TabIndex = 82;
            // 
            // numud_TestImage
            // 
            this.numud_TestImage.Location = new System.Drawing.Point(107, 6);
            this.numud_TestImage.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numud_TestImage.Name = "numud_TestImage";
            this.numud_TestImage.Size = new System.Drawing.Size(82, 20);
            this.numud_TestImage.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 81;
            this.label6.Text = "Viewer Message";
            // 
            // numud_TestStreamerMessage
            // 
            this.numud_TestStreamerMessage.Location = new System.Drawing.Point(106, 58);
            this.numud_TestStreamerMessage.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numud_TestStreamerMessage.Name = "numud_TestStreamerMessage";
            this.numud_TestStreamerMessage.Size = new System.Drawing.Size(82, 20);
            this.numud_TestStreamerMessage.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Image";
            // 
            // numud_TestAudio
            // 
            this.numud_TestAudio.Location = new System.Drawing.Point(107, 32);
            this.numud_TestAudio.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numud_TestAudio.Name = "numud_TestAudio";
            this.numud_TestAudio.Size = new System.Drawing.Size(82, 20);
            this.numud_TestAudio.TabIndex = 78;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Streamer Message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Audio";
            // 
            // tab_ChildMDIForms
            // 
            this.tab_ChildMDIForms.Dock = System.Windows.Forms.DockStyle.Top;
            this.tab_ChildMDIForms.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tab_ChildMDIForms.Location = new System.Drawing.Point(0, 225);
            this.tab_ChildMDIForms.Name = "tab_ChildMDIForms";
            this.tab_ChildMDIForms.SelectedIndex = 0;
            this.tab_ChildMDIForms.Size = new System.Drawing.Size(766, 25);
            this.tab_ChildMDIForms.TabIndex = 224;
            this.tab_ChildMDIForms.Visible = false;
            this.tab_ChildMDIForms.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tab_ChildMDIForms_DrawItem);
            this.tab_ChildMDIForms.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tab_ChildMDIForms_MouseUp);
            // 
            // waypointBindingSource
            // 
            this.waypointBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.Waypoint);
            // 
            // Edit
            // 
            this.Edit.HeaderText = "New / Edit";
            this.Edit.Name = "Edit";
            // 
            // Alert_Designer_MDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 681);
            this.Controls.Add(this.tab_ChildMDIForms);
            this.Controls.Add(this.tab_AlertPieces);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.grp_AlertSettings);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.grp_Preview);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(1305, 720);
            this.MinimumSize = new System.Drawing.Size(1305, 720);
            this.Name = "Alert_Designer_MDI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = " ";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grp_AlertSettings.ResumeLayout(false);
            this.grp_AlertSettings.PerformLayout();
            this.grp_Macro.ResumeLayout(false);
            this.grp_Macro.PerformLayout();
            this.grp_Preview.ResumeLayout(false);
            this.grp_Preview.PerformLayout();
            this.cmb_Cooldowns.ResumeLayout(false);
            this.cmb_Cooldowns.PerformLayout();
            this.grp_AlertDetails.ResumeLayout(false);
            this.grp_AlertDetails.PerformLayout();
            this.grp_PreviewControls.ResumeLayout(false);
            this.grp_PreviewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewStreamer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_PreviewImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).EndInit();
            this.tab_AlertPieces.ResumeLayout(false);
            this.tab_Image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertImageBindingSource)).EndInit();
            this.tab_Audio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Audio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertAudioBindingSource)).EndInit();
            this.tab_Streamer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_StreamerMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streamerAlertTextBindingSource)).EndInit();
            this.tab_Viewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_ViewerMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewerAlertTextBindingSource)).EndInit();
            this.tab_Conditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Conditions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertConditionBindingSource)).EndInit();
            this.tab_PreviewControls.ResumeLayout(false);
            this.tab_PreviewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_Amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestViewerMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestStreamerMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_TestAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem ts_NewImageVideo;
        private System.Windows.Forms.ToolStripMenuItem ts_Exit;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem ts_NewStreamerAudio;
        private System.Windows.Forms.ToolStripMenuItem ts_NewStreamerText;
        private System.Windows.Forms.ToolStripMenuItem ts_NewViewerText;
        private System.Windows.Forms.GroupBox grp_AlertSettings;
        private System.Windows.Forms.TextBox txt_AlertName;
        private System.Windows.Forms.Label lbl_AlertName;
        private System.Windows.Forms.GroupBox grp_Macro;
        private System.Windows.Forms.ComboBox cmb_MacroKey;
        private System.Windows.Forms.CheckBox chk_MacroCtrl;
        private System.Windows.Forms.Label cml_MacroKey;
        private System.Windows.Forms.CheckBox chk_MacroAlt;
        private System.Windows.Forms.CheckBox chk_MacroShift;
        private System.Windows.Forms.Label lbl_AlertType;
        private System.Windows.Forms.ComboBox cmb_AlertType;
        protected internal System.Windows.Forms.GroupBox grp_Preview;
        private System.Windows.Forms.GroupBox grp_PreviewControls;
        private System.Windows.Forms.CheckBox chk_PreviewViewerText;
        private System.Windows.Forms.CheckBox chk_PreviewStreamerText;
        private System.Windows.Forms.CheckBox chk_PreviewImage;
        protected internal System.Windows.Forms.Label lbl_Time;
        protected internal System.Windows.Forms.TrackBar trk_Time;
        protected internal System.Windows.Forms.PictureBox pb_Preview;
        private System.Windows.Forms.BindingSource alertAudioBindingSource;
        private System.Windows.Forms.BindingSource streamerAlertTextBindingSource;
        private System.Windows.Forms.BindingSource viewerAlertTextBindingSource;
        private System.Windows.Forms.BindingSource alertConditionBindingSource;
        private System.Windows.Forms.BindingSource alertImageBindingSource;
        private System.Windows.Forms.BindingSource waypointBindingSource;
        private System.Windows.Forms.TabControl tab_AlertPieces;
        private System.Windows.Forms.TabPage tab_Image;
        private System.Windows.Forms.DataGridView dg_Images;
        private System.Windows.Forms.TabPage tab_Audio;
        private System.Windows.Forms.DataGridView dg_Audio;
        private System.Windows.Forms.TabPage tab_Streamer;
        private System.Windows.Forms.DataGridView dg_StreamerMessages;
        private System.Windows.Forms.TabPage tab_Viewer;
        private System.Windows.Forms.DataGridView dg_ViewerMessages;
        private System.Windows.Forms.TabPage tab_Conditions;
        private System.Windows.Forms.DataGridView dg_Conditions;
        private System.Windows.Forms.TabPage tab_PreviewControls;
        private System.Windows.Forms.TextBox txt_ViewerMessage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numud_Amount;
        private System.Windows.Forms.DomainUpDown dom_SubTier;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_SubPlan;
        private System.Windows.Forms.TextBox txt_SubTierName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_RecipientName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ViewerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_TestAlert;
        private System.Windows.Forms.NumericUpDown numud_TestViewerMessage;
        private System.Windows.Forms.NumericUpDown numud_TestImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numud_TestStreamerMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numud_TestAudio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tab_ChildMDIForms;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dimensionsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn Images_Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn Audio_Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn Your_Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewButtonColumn Viewer_Edit;
        private System.Windows.Forms.GroupBox grp_AlertDetails;
        private System.Windows.Forms.Label lbl_MaximumDuration;
        private System.Windows.Forms.Label lbl_MinimumDuration;
        private System.Windows.Forms.NumericUpDown numud_PreviewViewer;
        private System.Windows.Forms.NumericUpDown numud_PreviewStreamer;
        private System.Windows.Forms.NumericUpDown numud_PreviewImage;
        private System.Windows.Forms.Label lbl_MaximumDurationCount;
        private System.Windows.Forms.Label lbl_CombinationsCount;
        private System.Windows.Forms.Label lbl_MinimumDurationCount;
        private System.Windows.Forms.Label lbl_Combinations;
        private System.Windows.Forms.ToolStripMenuItem ts_NewCondition;
        private System.Windows.Forms.GroupBox cmb_Cooldowns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtxt_GlobalCooldown;
        private System.Windows.Forms.MaskedTextBox mtxt_UserCooldown;
        private System.Windows.Forms.Label lbl_UserCooldown;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
    }
}



