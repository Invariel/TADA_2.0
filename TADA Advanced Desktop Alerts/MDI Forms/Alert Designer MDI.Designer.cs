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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Designer_MDI));
            menuStrip = new System.Windows.Forms.MenuStrip();
            fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            ts_NewImageVideo = new System.Windows.Forms.ToolStripMenuItem();
            ts_NewStreamerAudio = new System.Windows.Forms.ToolStripMenuItem();
            ts_NewStreamerText = new System.Windows.Forms.ToolStripMenuItem();
            ts_NewViewerText = new System.Windows.Forms.ToolStripMenuItem();
            ts_NewCondition = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            ts_Exit = new System.Windows.Forms.ToolStripMenuItem();
            ts_Save = new System.Windows.Forms.ToolStripMenuItem();
            helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            grp_AlertSettings = new System.Windows.Forms.GroupBox();
            txt_AlertName = new System.Windows.Forms.TextBox();
            lbl_AlertName = new System.Windows.Forms.Label();
            grp_Macro = new System.Windows.Forms.GroupBox();
            cmb_MacroKey = new System.Windows.Forms.ComboBox();
            chk_MacroCtrl = new System.Windows.Forms.CheckBox();
            cml_MacroKey = new System.Windows.Forms.Label();
            chk_MacroAlt = new System.Windows.Forms.CheckBox();
            chk_MacroShift = new System.Windows.Forms.CheckBox();
            lbl_AlertType = new System.Windows.Forms.Label();
            cmb_AlertType = new System.Windows.Forms.ComboBox();
            grp_Preview = new System.Windows.Forms.GroupBox();
            cmb_Cooldowns = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            mtxt_GlobalCooldown = new System.Windows.Forms.MaskedTextBox();
            mtxt_UserCooldown = new System.Windows.Forms.MaskedTextBox();
            lbl_UserCooldown = new System.Windows.Forms.Label();
            grp_AlertDetails = new System.Windows.Forms.GroupBox();
            lbl_MaximumDurationCount = new System.Windows.Forms.Label();
            lbl_CombinationsCount = new System.Windows.Forms.Label();
            lbl_MinimumDurationCount = new System.Windows.Forms.Label();
            lbl_Combinations = new System.Windows.Forms.Label();
            lbl_MaximumDuration = new System.Windows.Forms.Label();
            lbl_MinimumDuration = new System.Windows.Forms.Label();
            grp_PreviewControls = new System.Windows.Forms.GroupBox();
            numud_PreviewViewer = new System.Windows.Forms.NumericUpDown();
            numud_PreviewStreamer = new System.Windows.Forms.NumericUpDown();
            numud_PreviewImage = new System.Windows.Forms.NumericUpDown();
            chk_PreviewViewerText = new System.Windows.Forms.CheckBox();
            chk_PreviewStreamerText = new System.Windows.Forms.CheckBox();
            chk_PreviewImage = new System.Windows.Forms.CheckBox();
            lbl_Time = new System.Windows.Forms.Label();
            trk_Time = new System.Windows.Forms.TrackBar();
            pb_Preview = new System.Windows.Forms.PictureBox();
            tab_AlertPieces = new System.Windows.Forms.TabControl();
            tab_Image = new System.Windows.Forms.TabPage();
            dg_Images = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dimensionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            Images_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            alertImageBindingSource = new System.Windows.Forms.BindingSource(components);
            tab_Audio = new System.Windows.Forms.TabPage();
            dg_Audio = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Audio_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            alertAudioBindingSource = new System.Windows.Forms.BindingSource(components);
            tab_Streamer = new System.Windows.Forms.TabPage();
            dg_StreamerMessages = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Your_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            streamerAlertTextBindingSource = new System.Windows.Forms.BindingSource(components);
            tab_Viewer = new System.Windows.Forms.TabPage();
            dg_ViewerMessages = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Viewer_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            viewerAlertTextBindingSource = new System.Windows.Forms.BindingSource(components);
            tab_Conditions = new System.Windows.Forms.TabPage();
            dg_Conditions = new System.Windows.Forms.DataGridView();
            ViewerSubTier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            SubscriptionTier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            BadgeString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            viewerSubTierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            subscriptionTierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            badgeStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            emptyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasViewerTierDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasSubscriptionTierDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasBadgesDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasUsersDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasBannedNamesDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasKeywordsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hasAmountsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            alertConditionBindingSource = new System.Windows.Forms.BindingSource(components);
            tab_PreviewControls = new System.Windows.Forms.TabPage();
            txt_ViewerMessage = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            numud_Amount = new System.Windows.Forms.NumericUpDown();
            dom_SubTier = new System.Windows.Forms.DomainUpDown();
            label11 = new System.Windows.Forms.Label();
            lbl_SubPlan = new System.Windows.Forms.Label();
            txt_SubTierName = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            txt_RecipientName = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            txt_ViewerName = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            btn_TestAlert = new System.Windows.Forms.Button();
            numud_TestViewerMessage = new System.Windows.Forms.NumericUpDown();
            numud_TestImage = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            numud_TestStreamerMessage = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            numud_TestAudio = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tab_ChildMDIForms = new System.Windows.Forms.TabControl();
            waypointBindingSource = new System.Windows.Forms.BindingSource(components);
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            grp_AlertSettings.SuspendLayout();
            grp_Macro.SuspendLayout();
            grp_Preview.SuspendLayout();
            cmb_Cooldowns.SuspendLayout();
            grp_AlertDetails.SuspendLayout();
            grp_PreviewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewViewer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewStreamer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trk_Time).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_Preview).BeginInit();
            tab_AlertPieces.SuspendLayout();
            tab_Image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_Images).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alertImageBindingSource).BeginInit();
            tab_Audio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_Audio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alertAudioBindingSource).BeginInit();
            tab_Streamer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_StreamerMessages).BeginInit();
            ((System.ComponentModel.ISupportInitialize)streamerAlertTextBindingSource).BeginInit();
            tab_Viewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_ViewerMessages).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewerAlertTextBindingSource).BeginInit();
            tab_Conditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_Conditions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alertConditionBindingSource).BeginInit();
            tab_PreviewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numud_Amount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestViewerMessage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestStreamerMessage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestAudio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)waypointBindingSource).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileMenu, helpMenu });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip.Size = new System.Drawing.Size(894, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ts_NewImageVideo, ts_NewStreamerAudio, ts_NewStreamerText, ts_NewViewerText, ts_NewCondition, toolStripSeparator5, ts_Save, ts_Exit });
            fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new System.Drawing.Size(37, 20);
            fileMenu.Text = "&File";
            // 
            // ts_NewImageVideo
            // 
            ts_NewImageVideo.Image = (System.Drawing.Image)resources.GetObject("ts_NewImageVideo.Image");
            ts_NewImageVideo.ImageTransparentColor = System.Drawing.Color.Black;
            ts_NewImageVideo.Name = "ts_NewImageVideo";
            ts_NewImageVideo.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.I;
            ts_NewImageVideo.Size = new System.Drawing.Size(244, 22);
            ts_NewImageVideo.Text = "New &Image/Video";
            // 
            // ts_NewStreamerAudio
            // 
            ts_NewStreamerAudio.Name = "ts_NewStreamerAudio";
            ts_NewStreamerAudio.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.U;
            ts_NewStreamerAudio.Size = new System.Drawing.Size(244, 22);
            ts_NewStreamerAudio.Text = "New A&udio";
            // 
            // ts_NewStreamerText
            // 
            ts_NewStreamerText.Name = "ts_NewStreamerText";
            ts_NewStreamerText.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
            ts_NewStreamerText.Size = new System.Drawing.Size(244, 22);
            ts_NewStreamerText.Text = "New &Streamer Text";
            // 
            // ts_NewViewerText
            // 
            ts_NewViewerText.Name = "ts_NewViewerText";
            ts_NewViewerText.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.E;
            ts_NewViewerText.Size = new System.Drawing.Size(244, 22);
            ts_NewViewerText.Text = "New Vi&ewer Text";
            // 
            // ts_NewCondition
            // 
            ts_NewCondition.Name = "ts_NewCondition";
            ts_NewCondition.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.O;
            ts_NewCondition.Size = new System.Drawing.Size(244, 22);
            ts_NewCondition.Text = "New C&ondition";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(241, 6);
            // 
            // ts_Exit
            // 
            ts_Exit.Name = "ts_Exit";
            ts_Exit.Size = new System.Drawing.Size(244, 22);
            ts_Exit.Text = "E&xit";
            ts_Exit.Click += ExitToolsStripMenuItem_Click;
            // 
            // ts_Save
            // 
            ts_Save.AccessibleDescription = "Save";
            ts_Save.Name = "ts_Save";
            ts_Save.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            ts_Save.Size = new System.Drawing.Size(244, 22);
            ts_Save.Text = "Save";
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator8, aboutToolStripMenuItem });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new System.Drawing.Size(44, 20);
            helpMenu.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            contentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1;
            contentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            indexToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("indexToolStripMenuItem.Image");
            indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            indexToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("searchToolStripMenuItem.Image");
            searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(165, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            aboutToolStripMenuItem.Text = "&About ... ...";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new System.Drawing.Point(0, 764);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip.Size = new System.Drawing.Size(894, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            toolStripStatusLabel.Text = "Status";
            // 
            // grp_AlertSettings
            // 
            grp_AlertSettings.Controls.Add(txt_AlertName);
            grp_AlertSettings.Controls.Add(lbl_AlertName);
            grp_AlertSettings.Controls.Add(grp_Macro);
            grp_AlertSettings.Controls.Add(lbl_AlertType);
            grp_AlertSettings.Controls.Add(cmb_AlertType);
            grp_AlertSettings.Dock = System.Windows.Forms.DockStyle.Top;
            grp_AlertSettings.Location = new System.Drawing.Point(0, 24);
            grp_AlertSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_AlertSettings.Name = "grp_AlertSettings";
            grp_AlertSettings.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_AlertSettings.Size = new System.Drawing.Size(894, 63);
            grp_AlertSettings.TabIndex = 1;
            grp_AlertSettings.TabStop = false;
            grp_AlertSettings.Text = "Alert Settings";
            // 
            // txt_AlertName
            // 
            txt_AlertName.Location = new System.Drawing.Point(383, 21);
            txt_AlertName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_AlertName.Name = "txt_AlertName";
            txt_AlertName.Size = new System.Drawing.Size(156, 23);
            txt_AlertName.TabIndex = 14;
            // 
            // lbl_AlertName
            // 
            lbl_AlertName.AutoSize = true;
            lbl_AlertName.Location = new System.Drawing.Point(310, 25);
            lbl_AlertName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_AlertName.Name = "lbl_AlertName";
            lbl_AlertName.Size = new System.Drawing.Size(67, 15);
            lbl_AlertName.TabIndex = 13;
            lbl_AlertName.Text = "Alert Name";
            // 
            // grp_Macro
            // 
            grp_Macro.Controls.Add(cmb_MacroKey);
            grp_Macro.Controls.Add(chk_MacroCtrl);
            grp_Macro.Controls.Add(cml_MacroKey);
            grp_Macro.Controls.Add(chk_MacroAlt);
            grp_Macro.Controls.Add(chk_MacroShift);
            grp_Macro.Location = new System.Drawing.Point(556, 8);
            grp_Macro.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_Macro.Name = "grp_Macro";
            grp_Macro.Padding = new System.Windows.Forms.Padding(0);
            grp_Macro.Size = new System.Drawing.Size(318, 48);
            grp_Macro.TabIndex = 15;
            grp_Macro.TabStop = false;
            grp_Macro.Text = "Macro Settings";
            grp_Macro.Visible = false;
            // 
            // cmb_MacroKey
            // 
            cmb_MacroKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb_MacroKey.FormattingEnabled = true;
            cmb_MacroKey.Location = new System.Drawing.Point(230, 13);
            cmb_MacroKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmb_MacroKey.Name = "cmb_MacroKey";
            cmb_MacroKey.Size = new System.Drawing.Size(76, 23);
            cmb_MacroKey.TabIndex = 9;
            // 
            // chk_MacroCtrl
            // 
            chk_MacroCtrl.AutoSize = true;
            chk_MacroCtrl.Location = new System.Drawing.Point(14, 16);
            chk_MacroCtrl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_MacroCtrl.Name = "chk_MacroCtrl";
            chk_MacroCtrl.Size = new System.Drawing.Size(45, 19);
            chk_MacroCtrl.TabIndex = 5;
            chk_MacroCtrl.Text = "Ctrl";
            chk_MacroCtrl.UseVisualStyleBackColor = true;
            // 
            // cml_MacroKey
            // 
            cml_MacroKey.AutoSize = true;
            cml_MacroKey.Location = new System.Drawing.Point(197, 17);
            cml_MacroKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            cml_MacroKey.Name = "cml_MacroKey";
            cml_MacroKey.Size = new System.Drawing.Size(26, 15);
            cml_MacroKey.TabIndex = 8;
            cml_MacroKey.Text = "Key";
            // 
            // chk_MacroAlt
            // 
            chk_MacroAlt.AutoSize = true;
            chk_MacroAlt.Location = new System.Drawing.Point(75, 16);
            chk_MacroAlt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_MacroAlt.Name = "chk_MacroAlt";
            chk_MacroAlt.Size = new System.Drawing.Size(41, 19);
            chk_MacroAlt.TabIndex = 6;
            chk_MacroAlt.Text = "Alt";
            chk_MacroAlt.UseVisualStyleBackColor = true;
            // 
            // chk_MacroShift
            // 
            chk_MacroShift.AutoSize = true;
            chk_MacroShift.Location = new System.Drawing.Point(132, 16);
            chk_MacroShift.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_MacroShift.Name = "chk_MacroShift";
            chk_MacroShift.Size = new System.Drawing.Size(50, 19);
            chk_MacroShift.TabIndex = 7;
            chk_MacroShift.Text = "Shift";
            chk_MacroShift.UseVisualStyleBackColor = true;
            // 
            // lbl_AlertType
            // 
            lbl_AlertType.AutoSize = true;
            lbl_AlertType.Location = new System.Drawing.Point(7, 25);
            lbl_AlertType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_AlertType.Name = "lbl_AlertType";
            lbl_AlertType.Size = new System.Drawing.Size(60, 15);
            lbl_AlertType.TabIndex = 11;
            lbl_AlertType.Text = "Alert Type";
            // 
            // cmb_AlertType
            // 
            cmb_AlertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb_AlertType.DropDownWidth = 225;
            cmb_AlertType.FormattingEnabled = true;
            cmb_AlertType.Location = new System.Drawing.Point(79, 21);
            cmb_AlertType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmb_AlertType.Name = "cmb_AlertType";
            cmb_AlertType.Size = new System.Drawing.Size(207, 23);
            cmb_AlertType.TabIndex = 12;
            // 
            // grp_Preview
            // 
            grp_Preview.Controls.Add(cmb_Cooldowns);
            grp_Preview.Controls.Add(grp_AlertDetails);
            grp_Preview.Controls.Add(grp_PreviewControls);
            grp_Preview.Controls.Add(lbl_Time);
            grp_Preview.Controls.Add(trk_Time);
            grp_Preview.Controls.Add(pb_Preview);
            grp_Preview.Dock = System.Windows.Forms.DockStyle.Right;
            grp_Preview.Location = new System.Drawing.Point(894, 0);
            grp_Preview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_Preview.Name = "grp_Preview";
            grp_Preview.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_Preview.Size = new System.Drawing.Size(610, 786);
            grp_Preview.TabIndex = 220;
            grp_Preview.TabStop = false;
            grp_Preview.Text = "Path Preview";
            // 
            // cmb_Cooldowns
            // 
            cmb_Cooldowns.Controls.Add(label1);
            cmb_Cooldowns.Controls.Add(mtxt_GlobalCooldown);
            cmb_Cooldowns.Controls.Add(mtxt_UserCooldown);
            cmb_Cooldowns.Controls.Add(lbl_UserCooldown);
            cmb_Cooldowns.Location = new System.Drawing.Point(7, 600);
            cmb_Cooldowns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmb_Cooldowns.Name = "cmb_Cooldowns";
            cmb_Cooldowns.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmb_Cooldowns.Size = new System.Drawing.Size(596, 87);
            cmb_Cooldowns.TabIndex = 219;
            cmb_Cooldowns.TabStop = false;
            cmb_Cooldowns.Text = "Alert Cooldowns";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(107, 55);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(282, 15);
            label1.TabIndex = 4;
            label1.Text = "must pass before anyone can trigger this alert again.";
            // 
            // mtxt_GlobalCooldown
            // 
            mtxt_GlobalCooldown.Location = new System.Drawing.Point(8, 51);
            mtxt_GlobalCooldown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mtxt_GlobalCooldown.Mask = "###:##:##.###";
            mtxt_GlobalCooldown.Name = "mtxt_GlobalCooldown";
            mtxt_GlobalCooldown.PromptChar = '0';
            mtxt_GlobalCooldown.Size = new System.Drawing.Size(92, 23);
            mtxt_GlobalCooldown.TabIndex = 3;
            mtxt_GlobalCooldown.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // mtxt_UserCooldown
            // 
            mtxt_UserCooldown.Location = new System.Drawing.Point(8, 20);
            mtxt_UserCooldown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mtxt_UserCooldown.Mask = "###:##:##.###";
            mtxt_UserCooldown.Name = "mtxt_UserCooldown";
            mtxt_UserCooldown.PromptChar = '0';
            mtxt_UserCooldown.Size = new System.Drawing.Size(92, 23);
            mtxt_UserCooldown.TabIndex = 2;
            mtxt_UserCooldown.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // lbl_UserCooldown
            // 
            lbl_UserCooldown.AutoSize = true;
            lbl_UserCooldown.Location = new System.Drawing.Point(107, 24);
            lbl_UserCooldown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_UserCooldown.Name = "lbl_UserCooldown";
            lbl_UserCooldown.Size = new System.Drawing.Size(330, 15);
            lbl_UserCooldown.TabIndex = 1;
            lbl_UserCooldown.Text = "must pass before the same person can trigger this alert again.";
            // 
            // grp_AlertDetails
            // 
            grp_AlertDetails.Controls.Add(lbl_MaximumDurationCount);
            grp_AlertDetails.Controls.Add(lbl_CombinationsCount);
            grp_AlertDetails.Controls.Add(lbl_MinimumDurationCount);
            grp_AlertDetails.Controls.Add(lbl_Combinations);
            grp_AlertDetails.Controls.Add(lbl_MaximumDuration);
            grp_AlertDetails.Controls.Add(lbl_MinimumDuration);
            grp_AlertDetails.Location = new System.Drawing.Point(7, 693);
            grp_AlertDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_AlertDetails.Name = "grp_AlertDetails";
            grp_AlertDetails.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_AlertDetails.Size = new System.Drawing.Size(596, 85);
            grp_AlertDetails.TabIndex = 218;
            grp_AlertDetails.TabStop = false;
            grp_AlertDetails.Text = "Alert Details";
            // 
            // lbl_MaximumDurationCount
            // 
            lbl_MaximumDurationCount.AutoSize = true;
            lbl_MaximumDurationCount.Location = new System.Drawing.Point(518, 23);
            lbl_MaximumDurationCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_MaximumDurationCount.Name = "lbl_MaximumDurationCount";
            lbl_MaximumDurationCount.Size = new System.Drawing.Size(21, 15);
            lbl_MaximumDurationCount.TabIndex = 5;
            lbl_MaximumDurationCount.Text = "0 s";
            // 
            // lbl_CombinationsCount
            // 
            lbl_CombinationsCount.AutoSize = true;
            lbl_CombinationsCount.Location = new System.Drawing.Point(164, 58);
            lbl_CombinationsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_CombinationsCount.Name = "lbl_CombinationsCount";
            lbl_CombinationsCount.Size = new System.Drawing.Size(13, 15);
            lbl_CombinationsCount.TabIndex = 4;
            lbl_CombinationsCount.Text = "0";
            // 
            // lbl_MinimumDurationCount
            // 
            lbl_MinimumDurationCount.AutoSize = true;
            lbl_MinimumDurationCount.Location = new System.Drawing.Point(164, 23);
            lbl_MinimumDurationCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_MinimumDurationCount.Name = "lbl_MinimumDurationCount";
            lbl_MinimumDurationCount.Size = new System.Drawing.Size(21, 15);
            lbl_MinimumDurationCount.TabIndex = 3;
            lbl_MinimumDurationCount.Text = "0 s";
            // 
            // lbl_Combinations
            // 
            lbl_Combinations.AutoSize = true;
            lbl_Combinations.Location = new System.Drawing.Point(8, 58);
            lbl_Combinations.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_Combinations.Name = "lbl_Combinations";
            lbl_Combinations.Size = new System.Drawing.Size(142, 15);
            lbl_Combinations.TabIndex = 2;
            lbl_Combinations.Text = "Total Alert Combinations:";
            // 
            // lbl_MaximumDuration
            // 
            lbl_MaximumDuration.AutoSize = true;
            lbl_MaximumDuration.Location = new System.Drawing.Point(369, 23);
            lbl_MaximumDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_MaximumDuration.Name = "lbl_MaximumDuration";
            lbl_MaximumDuration.Size = new System.Drawing.Size(141, 15);
            lbl_MaximumDuration.TabIndex = 1;
            lbl_MaximumDuration.Text = "Maximum Alert Duration:";
            // 
            // lbl_MinimumDuration
            // 
            lbl_MinimumDuration.AutoSize = true;
            lbl_MinimumDuration.Location = new System.Drawing.Point(8, 23);
            lbl_MinimumDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_MinimumDuration.Name = "lbl_MinimumDuration";
            lbl_MinimumDuration.Size = new System.Drawing.Size(140, 15);
            lbl_MinimumDuration.TabIndex = 0;
            lbl_MinimumDuration.Text = "Minimum Alert Duration:";
            // 
            // grp_PreviewControls
            // 
            grp_PreviewControls.Controls.Add(numud_PreviewViewer);
            grp_PreviewControls.Controls.Add(numud_PreviewStreamer);
            grp_PreviewControls.Controls.Add(numud_PreviewImage);
            grp_PreviewControls.Controls.Add(chk_PreviewViewerText);
            grp_PreviewControls.Controls.Add(chk_PreviewStreamerText);
            grp_PreviewControls.Controls.Add(chk_PreviewImage);
            grp_PreviewControls.Location = new System.Drawing.Point(7, 410);
            grp_PreviewControls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_PreviewControls.Name = "grp_PreviewControls";
            grp_PreviewControls.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grp_PreviewControls.Size = new System.Drawing.Size(596, 63);
            grp_PreviewControls.TabIndex = 217;
            grp_PreviewControls.TabStop = false;
            grp_PreviewControls.Text = "Preview Controls";
            // 
            // numud_PreviewViewer
            // 
            numud_PreviewViewer.Location = new System.Drawing.Point(516, 23);
            numud_PreviewViewer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_PreviewViewer.Name = "numud_PreviewViewer";
            numud_PreviewViewer.Size = new System.Drawing.Size(74, 23);
            numud_PreviewViewer.TabIndex = 218;
            // 
            // numud_PreviewStreamer
            // 
            numud_PreviewStreamer.Location = new System.Drawing.Point(301, 23);
            numud_PreviewStreamer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_PreviewStreamer.Name = "numud_PreviewStreamer";
            numud_PreviewStreamer.Size = new System.Drawing.Size(74, 23);
            numud_PreviewStreamer.TabIndex = 218;
            // 
            // numud_PreviewImage
            // 
            numud_PreviewImage.Location = new System.Drawing.Point(76, 23);
            numud_PreviewImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_PreviewImage.Name = "numud_PreviewImage";
            numud_PreviewImage.Size = new System.Drawing.Size(74, 23);
            numud_PreviewImage.TabIndex = 218;
            // 
            // chk_PreviewViewerText
            // 
            chk_PreviewViewerText.AutoSize = true;
            chk_PreviewViewerText.Location = new System.Drawing.Point(416, 25);
            chk_PreviewViewerText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_PreviewViewerText.Name = "chk_PreviewViewerText";
            chk_PreviewViewerText.Size = new System.Drawing.Size(85, 19);
            chk_PreviewViewerText.TabIndex = 219;
            chk_PreviewViewerText.Text = "Viewer Text";
            chk_PreviewViewerText.UseVisualStyleBackColor = true;
            // 
            // chk_PreviewStreamerText
            // 
            chk_PreviewStreamerText.AutoSize = true;
            chk_PreviewStreamerText.Location = new System.Drawing.Point(190, 25);
            chk_PreviewStreamerText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_PreviewStreamerText.Name = "chk_PreviewStreamerText";
            chk_PreviewStreamerText.Size = new System.Drawing.Size(97, 19);
            chk_PreviewStreamerText.TabIndex = 218;
            chk_PreviewStreamerText.Text = "Streamer Text";
            chk_PreviewStreamerText.UseVisualStyleBackColor = true;
            // 
            // chk_PreviewImage
            // 
            chk_PreviewImage.AutoSize = true;
            chk_PreviewImage.Location = new System.Drawing.Point(8, 25);
            chk_PreviewImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chk_PreviewImage.Name = "chk_PreviewImage";
            chk_PreviewImage.Size = new System.Drawing.Size(59, 19);
            chk_PreviewImage.TabIndex = 217;
            chk_PreviewImage.Text = "Image";
            chk_PreviewImage.UseVisualStyleBackColor = true;
            // 
            // lbl_Time
            // 
            lbl_Time.AutoSize = true;
            lbl_Time.Location = new System.Drawing.Point(558, 369);
            lbl_Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_Time.Name = "lbl_Time";
            lbl_Time.Size = new System.Drawing.Size(18, 15);
            lbl_Time.TabIndex = 213;
            lbl_Time.Text = "0s";
            // 
            // trk_Time
            // 
            trk_Time.LargeChange = 50;
            trk_Time.Location = new System.Drawing.Point(9, 351);
            trk_Time.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trk_Time.Maximum = 1000;
            trk_Time.Name = "trk_Time";
            trk_Time.Size = new System.Drawing.Size(534, 45);
            trk_Time.TabIndex = 212;
            trk_Time.TickFrequency = 25;
            // 
            // pb_Preview
            // 
            pb_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pb_Preview.Location = new System.Drawing.Point(7, 17);
            pb_Preview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pb_Preview.Name = "pb_Preview";
            pb_Preview.Size = new System.Drawing.Size(597, 332);
            pb_Preview.TabIndex = 3;
            pb_Preview.TabStop = false;
            // 
            // tab_AlertPieces
            // 
            tab_AlertPieces.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            tab_AlertPieces.Controls.Add(tab_Image);
            tab_AlertPieces.Controls.Add(tab_Audio);
            tab_AlertPieces.Controls.Add(tab_Streamer);
            tab_AlertPieces.Controls.Add(tab_Viewer);
            tab_AlertPieces.Controls.Add(tab_Conditions);
            tab_AlertPieces.Controls.Add(tab_PreviewControls);
            tab_AlertPieces.Dock = System.Windows.Forms.DockStyle.Top;
            tab_AlertPieces.Location = new System.Drawing.Point(0, 87);
            tab_AlertPieces.Margin = new System.Windows.Forms.Padding(0);
            tab_AlertPieces.Name = "tab_AlertPieces";
            tab_AlertPieces.Padding = new System.Drawing.Point(0, 0);
            tab_AlertPieces.SelectedIndex = 0;
            tab_AlertPieces.Size = new System.Drawing.Size(894, 168);
            tab_AlertPieces.TabIndex = 222;
            // 
            // tab_Image
            // 
            tab_Image.BackColor = System.Drawing.SystemColors.Control;
            tab_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            tab_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tab_Image.Controls.Add(dg_Images);
            tab_Image.Location = new System.Drawing.Point(4, 27);
            tab_Image.Margin = new System.Windows.Forms.Padding(0);
            tab_Image.Name = "tab_Image";
            tab_Image.Size = new System.Drawing.Size(886, 137);
            tab_Image.TabIndex = 0;
            tab_Image.Text = "Images and Video";
            // 
            // dg_Images
            // 
            dg_Images.AutoGenerateColumns = false;
            dg_Images.BackgroundColor = System.Drawing.SystemColors.Control;
            dg_Images.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_Images.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn1, dimensionsDataGridViewTextBoxColumn, dataGridViewCheckBoxColumn1, Images_Edit });
            dg_Images.DataSource = alertImageBindingSource;
            dg_Images.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_Images.Location = new System.Drawing.Point(0, 0);
            dg_Images.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dg_Images.MultiSelect = false;
            dg_Images.Name = "dg_Images";
            dg_Images.ReadOnly = true;
            dg_Images.Size = new System.Drawing.Size(884, 135);
            dg_Images.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn1.DataPropertyName = "Filename";
            dataGridViewTextBoxColumn1.HeaderText = "Filename";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dimensionsDataGridViewTextBoxColumn
            // 
            dimensionsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dimensionsDataGridViewTextBoxColumn.DataPropertyName = "Dimensions";
            dimensionsDataGridViewTextBoxColumn.HeaderText = "Size";
            dimensionsDataGridViewTextBoxColumn.MinimumWidth = 75;
            dimensionsDataGridViewTextBoxColumn.Name = "dimensionsDataGridViewTextBoxColumn";
            dimensionsDataGridViewTextBoxColumn.ReadOnly = true;
            dimensionsDataGridViewTextBoxColumn.Width = 75;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCheckBoxColumn1.DataPropertyName = "IsVideo";
            dataGridViewCheckBoxColumn1.HeaderText = "Video ?";
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            dataGridViewCheckBoxColumn1.ReadOnly = true;
            dataGridViewCheckBoxColumn1.Width = 51;
            // 
            // Images_Edit
            // 
            Images_Edit.HeaderText = "New / Edit";
            Images_Edit.MinimumWidth = 75;
            Images_Edit.Name = "Images_Edit";
            Images_Edit.ReadOnly = true;
            Images_Edit.Text = "Edit";
            Images_Edit.ToolTipText = "Create a new image/video alert piece, or edit an existing one.";
            Images_Edit.UseColumnTextForButtonValue = true;
            Images_Edit.Width = 75;
            // 
            // alertImageBindingSource
            // 
            alertImageBindingSource.DataSource = typeof(TADAAlertImage);
            // 
            // tab_Audio
            // 
            tab_Audio.BackColor = System.Drawing.SystemColors.Control;
            tab_Audio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tab_Audio.Controls.Add(dg_Audio);
            tab_Audio.Location = new System.Drawing.Point(4, 27);
            tab_Audio.Margin = new System.Windows.Forms.Padding(0);
            tab_Audio.Name = "tab_Audio";
            tab_Audio.Size = new System.Drawing.Size(886, 137);
            tab_Audio.TabIndex = 1;
            tab_Audio.Text = "Audio";
            // 
            // dg_Audio
            // 
            dg_Audio.AutoGenerateColumns = false;
            dg_Audio.BackgroundColor = System.Drawing.SystemColors.Control;
            dg_Audio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_Audio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn2, Audio_Edit });
            dg_Audio.DataSource = alertAudioBindingSource;
            dg_Audio.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_Audio.Location = new System.Drawing.Point(0, 0);
            dg_Audio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dg_Audio.Name = "dg_Audio";
            dg_Audio.Size = new System.Drawing.Size(884, 135);
            dg_Audio.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn2.DataPropertyName = "Filename";
            dataGridViewTextBoxColumn2.HeaderText = "Filename";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Audio_Edit
            // 
            Audio_Edit.HeaderText = "New / Edit";
            Audio_Edit.MinimumWidth = 75;
            Audio_Edit.Name = "Audio_Edit";
            Audio_Edit.Text = "Edit";
            Audio_Edit.ToolTipText = "Create a new audio piece or edit an existing one.";
            Audio_Edit.UseColumnTextForButtonValue = true;
            Audio_Edit.Width = 75;
            // 
            // alertAudioBindingSource
            // 
            alertAudioBindingSource.DataSource = typeof(TADAAlertAudio);
            // 
            // tab_Streamer
            // 
            tab_Streamer.BackColor = System.Drawing.SystemColors.Control;
            tab_Streamer.Controls.Add(dg_StreamerMessages);
            tab_Streamer.Location = new System.Drawing.Point(4, 27);
            tab_Streamer.Margin = new System.Windows.Forms.Padding(0);
            tab_Streamer.Name = "tab_Streamer";
            tab_Streamer.Size = new System.Drawing.Size(886, 137);
            tab_Streamer.TabIndex = 2;
            tab_Streamer.Text = "Streamer's Text";
            // 
            // dg_StreamerMessages
            // 
            dg_StreamerMessages.AutoGenerateColumns = false;
            dg_StreamerMessages.BackgroundColor = System.Drawing.SystemColors.Control;
            dg_StreamerMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_StreamerMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, Your_Edit });
            dg_StreamerMessages.DataSource = streamerAlertTextBindingSource;
            dg_StreamerMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_StreamerMessages.Location = new System.Drawing.Point(0, 0);
            dg_StreamerMessages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dg_StreamerMessages.Name = "dg_StreamerMessages";
            dg_StreamerMessages.Size = new System.Drawing.Size(886, 137);
            dg_StreamerMessages.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn3.DataPropertyName = "Message";
            dataGridViewTextBoxColumn3.HeaderText = "Displayed Message";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn4.DataPropertyName = "ReadMessage";
            dataGridViewTextBoxColumn4.HeaderText = "Narrated Message";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // Your_Edit
            // 
            Your_Edit.HeaderText = "New / Edit";
            Your_Edit.MinimumWidth = 75;
            Your_Edit.Name = "Your_Edit";
            Your_Edit.Text = "Edit";
            Your_Edit.ToolTipText = "Create a new piece of displayed text, or edit an existing one.";
            Your_Edit.UseColumnTextForButtonValue = true;
            Your_Edit.Width = 75;
            // 
            // streamerAlertTextBindingSource
            // 
            streamerAlertTextBindingSource.DataSource = typeof(TADAAlertText);
            // 
            // tab_Viewer
            // 
            tab_Viewer.BackColor = System.Drawing.SystemColors.Control;
            tab_Viewer.Controls.Add(dg_ViewerMessages);
            tab_Viewer.Location = new System.Drawing.Point(4, 27);
            tab_Viewer.Margin = new System.Windows.Forms.Padding(0);
            tab_Viewer.Name = "tab_Viewer";
            tab_Viewer.Size = new System.Drawing.Size(886, 137);
            tab_Viewer.TabIndex = 3;
            tab_Viewer.Text = "Viewer's Text";
            // 
            // dg_ViewerMessages
            // 
            dg_ViewerMessages.AutoGenerateColumns = false;
            dg_ViewerMessages.BackgroundColor = System.Drawing.SystemColors.Control;
            dg_ViewerMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_ViewerMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, Viewer_Edit });
            dg_ViewerMessages.DataSource = viewerAlertTextBindingSource;
            dg_ViewerMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_ViewerMessages.Location = new System.Drawing.Point(0, 0);
            dg_ViewerMessages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dg_ViewerMessages.Name = "dg_ViewerMessages";
            dg_ViewerMessages.Size = new System.Drawing.Size(886, 137);
            dg_ViewerMessages.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn5.DataPropertyName = "Message";
            dataGridViewTextBoxColumn5.HeaderText = "Displayed Message";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn6.DataPropertyName = "ReadMessage";
            dataGridViewTextBoxColumn6.HeaderText = "Narrated Message";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Viewer_Edit
            // 
            Viewer_Edit.HeaderText = "New / Edit";
            Viewer_Edit.MinimumWidth = 75;
            Viewer_Edit.Name = "Viewer_Edit";
            Viewer_Edit.Text = "Edit";
            Viewer_Edit.ToolTipText = "Create a new style for a viewer's text, or edit an existing one.";
            Viewer_Edit.UseColumnTextForButtonValue = true;
            Viewer_Edit.Width = 75;
            // 
            // viewerAlertTextBindingSource
            // 
            viewerAlertTextBindingSource.DataSource = typeof(TADAAlertText);
            // 
            // tab_Conditions
            // 
            tab_Conditions.Controls.Add(dg_Conditions);
            tab_Conditions.Location = new System.Drawing.Point(4, 27);
            tab_Conditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tab_Conditions.Name = "tab_Conditions";
            tab_Conditions.Size = new System.Drawing.Size(886, 137);
            tab_Conditions.TabIndex = 5;
            tab_Conditions.Text = "Trigger Conditions";
            tab_Conditions.UseVisualStyleBackColor = true;
            // 
            // dg_Conditions
            // 
            dg_Conditions.AutoGenerateColumns = false;
            dg_Conditions.BackgroundColor = System.Drawing.SystemColors.Control;
            dg_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_Conditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ViewerSubTier, SubscriptionTier, BadgeString, Edit, viewerSubTierDataGridViewTextBoxColumn, subscriptionTierDataGridViewTextBoxColumn, badgeStringDataGridViewTextBoxColumn, emptyDataGridViewCheckBoxColumn, hasViewerTierDataGridViewCheckBoxColumn, hasSubscriptionTierDataGridViewCheckBoxColumn, hasBadgesDataGridViewCheckBoxColumn, hasUsersDataGridViewCheckBoxColumn, hasBannedNamesDataGridViewCheckBoxColumn, hasKeywordsDataGridViewCheckBoxColumn, hasAmountsDataGridViewCheckBoxColumn });
            dg_Conditions.DataSource = alertConditionBindingSource;
            dg_Conditions.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_Conditions.Location = new System.Drawing.Point(0, 0);
            dg_Conditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dg_Conditions.Name = "dg_Conditions";
            dg_Conditions.Size = new System.Drawing.Size(886, 137);
            dg_Conditions.TabIndex = 1;
            // 
            // ViewerSubTier
            // 
            ViewerSubTier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ViewerSubTier.DataPropertyName = "ViewerSubTier";
            ViewerSubTier.HeaderText = "Viewer's Sub Tier";
            ViewerSubTier.Name = "ViewerSubTier";
            ViewerSubTier.ReadOnly = true;
            // 
            // SubscriptionTier
            // 
            SubscriptionTier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            SubscriptionTier.DataPropertyName = "SubscriptionTier";
            SubscriptionTier.HeaderText = "Active Subscription Tier";
            SubscriptionTier.Name = "SubscriptionTier";
            SubscriptionTier.ReadOnly = true;
            // 
            // BadgeString
            // 
            BadgeString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            BadgeString.DataPropertyName = "BadgeString";
            BadgeString.HeaderText = "Badge Requirements";
            BadgeString.Name = "BadgeString";
            BadgeString.ReadOnly = true;
            // 
            // Edit
            // 
            Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            Edit.FillWeight = 10F;
            Edit.HeaderText = "New / Edit";
            Edit.Name = "Edit";
            Edit.Width = 43;
            // 
            // viewerSubTierDataGridViewTextBoxColumn
            // 
            viewerSubTierDataGridViewTextBoxColumn.DataPropertyName = "ViewerSubTier";
            viewerSubTierDataGridViewTextBoxColumn.HeaderText = "Viewer's Subscription Tier";
            viewerSubTierDataGridViewTextBoxColumn.Name = "viewerSubTierDataGridViewTextBoxColumn";
            viewerSubTierDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // subscriptionTierDataGridViewTextBoxColumn
            // 
            subscriptionTierDataGridViewTextBoxColumn.DataPropertyName = "SubscriptionTier";
            subscriptionTierDataGridViewTextBoxColumn.HeaderText = "Subscription Tier";
            subscriptionTierDataGridViewTextBoxColumn.Name = "subscriptionTierDataGridViewTextBoxColumn";
            subscriptionTierDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // badgeStringDataGridViewTextBoxColumn
            // 
            badgeStringDataGridViewTextBoxColumn.DataPropertyName = "BadgeString";
            badgeStringDataGridViewTextBoxColumn.HeaderText = "Viewer Badges";
            badgeStringDataGridViewTextBoxColumn.Name = "badgeStringDataGridViewTextBoxColumn";
            badgeStringDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emptyDataGridViewCheckBoxColumn
            // 
            emptyDataGridViewCheckBoxColumn.DataPropertyName = "Empty";
            emptyDataGridViewCheckBoxColumn.HeaderText = "Empty";
            emptyDataGridViewCheckBoxColumn.Name = "emptyDataGridViewCheckBoxColumn";
            emptyDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasViewerTierDataGridViewCheckBoxColumn
            // 
            hasViewerTierDataGridViewCheckBoxColumn.DataPropertyName = "HasViewerTier";
            hasViewerTierDataGridViewCheckBoxColumn.HeaderText = "HasViewerTier";
            hasViewerTierDataGridViewCheckBoxColumn.Name = "hasViewerTierDataGridViewCheckBoxColumn";
            hasViewerTierDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasSubscriptionTierDataGridViewCheckBoxColumn
            // 
            hasSubscriptionTierDataGridViewCheckBoxColumn.DataPropertyName = "HasSubscriptionTier";
            hasSubscriptionTierDataGridViewCheckBoxColumn.HeaderText = "HasSubscriptionTier";
            hasSubscriptionTierDataGridViewCheckBoxColumn.Name = "hasSubscriptionTierDataGridViewCheckBoxColumn";
            hasSubscriptionTierDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasBadgesDataGridViewCheckBoxColumn
            // 
            hasBadgesDataGridViewCheckBoxColumn.DataPropertyName = "HasBadges";
            hasBadgesDataGridViewCheckBoxColumn.HeaderText = "HasBadges";
            hasBadgesDataGridViewCheckBoxColumn.Name = "hasBadgesDataGridViewCheckBoxColumn";
            hasBadgesDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasUsersDataGridViewCheckBoxColumn
            // 
            hasUsersDataGridViewCheckBoxColumn.DataPropertyName = "HasUsers";
            hasUsersDataGridViewCheckBoxColumn.HeaderText = "HasUsers";
            hasUsersDataGridViewCheckBoxColumn.Name = "hasUsersDataGridViewCheckBoxColumn";
            hasUsersDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasBannedNamesDataGridViewCheckBoxColumn
            // 
            hasBannedNamesDataGridViewCheckBoxColumn.DataPropertyName = "HasBannedNames";
            hasBannedNamesDataGridViewCheckBoxColumn.HeaderText = "HasBannedNames";
            hasBannedNamesDataGridViewCheckBoxColumn.Name = "hasBannedNamesDataGridViewCheckBoxColumn";
            hasBannedNamesDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasKeywordsDataGridViewCheckBoxColumn
            // 
            hasKeywordsDataGridViewCheckBoxColumn.DataPropertyName = "HasKeywords";
            hasKeywordsDataGridViewCheckBoxColumn.HeaderText = "HasKeywords";
            hasKeywordsDataGridViewCheckBoxColumn.Name = "hasKeywordsDataGridViewCheckBoxColumn";
            hasKeywordsDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hasAmountsDataGridViewCheckBoxColumn
            // 
            hasAmountsDataGridViewCheckBoxColumn.DataPropertyName = "HasAmounts";
            hasAmountsDataGridViewCheckBoxColumn.HeaderText = "HasAmounts";
            hasAmountsDataGridViewCheckBoxColumn.Name = "hasAmountsDataGridViewCheckBoxColumn";
            hasAmountsDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // alertConditionBindingSource
            // 
            alertConditionBindingSource.DataSource = typeof(TADAAlertCondition);
            // 
            // tab_PreviewControls
            // 
            tab_PreviewControls.BackColor = System.Drawing.SystemColors.Control;
            tab_PreviewControls.Controls.Add(txt_ViewerMessage);
            tab_PreviewControls.Controls.Add(label12);
            tab_PreviewControls.Controls.Add(numud_Amount);
            tab_PreviewControls.Controls.Add(dom_SubTier);
            tab_PreviewControls.Controls.Add(label11);
            tab_PreviewControls.Controls.Add(lbl_SubPlan);
            tab_PreviewControls.Controls.Add(txt_SubTierName);
            tab_PreviewControls.Controls.Add(label9);
            tab_PreviewControls.Controls.Add(txt_RecipientName);
            tab_PreviewControls.Controls.Add(label8);
            tab_PreviewControls.Controls.Add(txt_ViewerName);
            tab_PreviewControls.Controls.Add(label7);
            tab_PreviewControls.Controls.Add(btn_TestAlert);
            tab_PreviewControls.Controls.Add(numud_TestViewerMessage);
            tab_PreviewControls.Controls.Add(numud_TestImage);
            tab_PreviewControls.Controls.Add(label6);
            tab_PreviewControls.Controls.Add(numud_TestStreamerMessage);
            tab_PreviewControls.Controls.Add(label3);
            tab_PreviewControls.Controls.Add(numud_TestAudio);
            tab_PreviewControls.Controls.Add(label5);
            tab_PreviewControls.Controls.Add(label4);
            tab_PreviewControls.Location = new System.Drawing.Point(4, 27);
            tab_PreviewControls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tab_PreviewControls.Name = "tab_PreviewControls";
            tab_PreviewControls.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tab_PreviewControls.Size = new System.Drawing.Size(886, 137);
            tab_PreviewControls.TabIndex = 4;
            tab_PreviewControls.Text = "Preview Controls";
            // 
            // txt_ViewerMessage
            // 
            txt_ViewerMessage.Location = new System.Drawing.Point(362, 97);
            txt_ViewerMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_ViewerMessage.Name = "txt_ViewerMessage";
            txt_ViewerMessage.Size = new System.Drawing.Size(388, 23);
            txt_ViewerMessage.TabIndex = 90;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(254, 102);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(91, 15);
            label12.TabIndex = 89;
            label12.Text = "Viewer Message";
            // 
            // numud_Amount
            // 
            numud_Amount.Location = new System.Drawing.Point(656, 37);
            numud_Amount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_Amount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numud_Amount.Name = "numud_Amount";
            numud_Amount.Size = new System.Drawing.Size(140, 23);
            numud_Amount.TabIndex = 94;
            // 
            // dom_SubTier
            // 
            dom_SubTier.Items.Add("No Tier");
            dom_SubTier.Items.Add("Twitch Prime");
            dom_SubTier.Items.Add("Tier One");
            dom_SubTier.Items.Add("Tier Two");
            dom_SubTier.Items.Add("Tier Three");
            dom_SubTier.Location = new System.Drawing.Point(656, 9);
            dom_SubTier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dom_SubTier.Name = "dom_SubTier";
            dom_SubTier.Size = new System.Drawing.Size(140, 23);
            dom_SubTier.TabIndex = 92;
            dom_SubTier.Text = "No Tier";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(544, 42);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(99, 15);
            label11.TabIndex = 93;
            label11.Text = "Relevant Amount";
            // 
            // lbl_SubPlan
            // 
            lbl_SubPlan.AutoSize = true;
            lbl_SubPlan.Location = new System.Drawing.Point(544, 14);
            lbl_SubPlan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_SubPlan.Name = "lbl_SubPlan";
            lbl_SubPlan.Size = new System.Drawing.Size(99, 15);
            lbl_SubPlan.TabIndex = 91;
            lbl_SubPlan.Text = "Viewer's Sub Plan";
            // 
            // txt_SubTierName
            // 
            txt_SubTierName.Location = new System.Drawing.Point(362, 67);
            txt_SubTierName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_SubTierName.Name = "txt_SubTierName";
            txt_SubTierName.Size = new System.Drawing.Size(148, 23);
            txt_SubTierName.TabIndex = 88;
            txt_SubTierName.Text = "Tier Awesome";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(262, 72);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(85, 15);
            label9.TabIndex = 87;
            label9.Text = "Sub Tier Name";
            // 
            // txt_RecipientName
            // 
            txt_RecipientName.Location = new System.Drawing.Point(362, 37);
            txt_RecipientName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_RecipientName.Name = "txt_RecipientName";
            txt_RecipientName.Size = new System.Drawing.Size(148, 23);
            txt_RecipientName.TabIndex = 86;
            txt_RecipientName.Text = "Leiravni";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(245, 42);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(101, 15);
            label8.TabIndex = 85;
            label8.Text = "Sub Gift Recipient";
            // 
            // txt_ViewerName
            // 
            txt_ViewerName.Location = new System.Drawing.Point(362, 7);
            txt_ViewerName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_ViewerName.Name = "txt_ViewerName";
            txt_ViewerName.Size = new System.Drawing.Size(148, 23);
            txt_ViewerName.TabIndex = 84;
            txt_ViewerName.Text = "Invariel";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(272, 12);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(77, 15);
            label7.TabIndex = 83;
            label7.Text = "Viewer Name";
            // 
            // btn_TestAlert
            // 
            btn_TestAlert.Location = new System.Drawing.Point(775, 95);
            btn_TestAlert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_TestAlert.Name = "btn_TestAlert";
            btn_TestAlert.Size = new System.Drawing.Size(96, 27);
            btn_TestAlert.TabIndex = 110;
            btn_TestAlert.Text = "Test Alert";
            btn_TestAlert.UseVisualStyleBackColor = true;
            // 
            // numud_TestViewerMessage
            // 
            numud_TestViewerMessage.Location = new System.Drawing.Point(125, 97);
            numud_TestViewerMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_TestViewerMessage.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numud_TestViewerMessage.Name = "numud_TestViewerMessage";
            numud_TestViewerMessage.Size = new System.Drawing.Size(96, 23);
            numud_TestViewerMessage.TabIndex = 82;
            // 
            // numud_TestImage
            // 
            numud_TestImage.Location = new System.Drawing.Point(125, 7);
            numud_TestImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_TestImage.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numud_TestImage.Name = "numud_TestImage";
            numud_TestImage.Size = new System.Drawing.Size(96, 23);
            numud_TestImage.TabIndex = 76;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(18, 102);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(91, 15);
            label6.TabIndex = 81;
            label6.Text = "Viewer Message";
            // 
            // numud_TestStreamerMessage
            // 
            numud_TestStreamerMessage.Location = new System.Drawing.Point(124, 67);
            numud_TestStreamerMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_TestStreamerMessage.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numud_TestStreamerMessage.Name = "numud_TestStreamerMessage";
            numud_TestStreamerMessage.Size = new System.Drawing.Size(96, 23);
            numud_TestStreamerMessage.TabIndex = 80;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(75, 12);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 15);
            label3.TabIndex = 75;
            label3.Text = "Image";
            // 
            // numud_TestAudio
            // 
            numud_TestAudio.Location = new System.Drawing.Point(125, 37);
            numud_TestAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numud_TestAudio.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numud_TestAudio.Name = "numud_TestAudio";
            numud_TestAudio.Size = new System.Drawing.Size(96, 23);
            numud_TestAudio.TabIndex = 78;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 72);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(103, 15);
            label5.TabIndex = 79;
            label5.Text = "Streamer Message";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(77, 42);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(39, 15);
            label4.TabIndex = 77;
            label4.Text = "Audio";
            // 
            // tab_ChildMDIForms
            // 
            tab_ChildMDIForms.Dock = System.Windows.Forms.DockStyle.Top;
            tab_ChildMDIForms.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            tab_ChildMDIForms.Location = new System.Drawing.Point(0, 255);
            tab_ChildMDIForms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tab_ChildMDIForms.Name = "tab_ChildMDIForms";
            tab_ChildMDIForms.SelectedIndex = 0;
            tab_ChildMDIForms.Size = new System.Drawing.Size(894, 29);
            tab_ChildMDIForms.TabIndex = 224;
            tab_ChildMDIForms.Visible = false;
            tab_ChildMDIForms.DrawItem += tab_ChildMDIForms_DrawItem;
            tab_ChildMDIForms.MouseUp += tab_ChildMDIForms_MouseUp;
            // 
            // waypointBindingSource
            // 
            waypointBindingSource.DataSource = typeof(Waypoint);
            // 
            // Alert_Designer_MDI
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1504, 786);
            Controls.Add(tab_ChildMDIForms);
            Controls.Add(tab_AlertPieces);
            Controls.Add(statusStrip);
            Controls.Add(grp_AlertSettings);
            Controls.Add(menuStrip);
            Controls.Add(grp_Preview);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximumSize = new System.Drawing.Size(1520, 825);
            MinimumSize = new System.Drawing.Size(1520, 825);
            Name = "Alert_Designer_MDI";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = " ";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            grp_AlertSettings.ResumeLayout(false);
            grp_AlertSettings.PerformLayout();
            grp_Macro.ResumeLayout(false);
            grp_Macro.PerformLayout();
            grp_Preview.ResumeLayout(false);
            grp_Preview.PerformLayout();
            cmb_Cooldowns.ResumeLayout(false);
            cmb_Cooldowns.PerformLayout();
            grp_AlertDetails.ResumeLayout(false);
            grp_AlertDetails.PerformLayout();
            grp_PreviewControls.ResumeLayout(false);
            grp_PreviewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewViewer).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewStreamer).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_PreviewImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)trk_Time).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_Preview).EndInit();
            tab_AlertPieces.ResumeLayout(false);
            tab_Image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_Images).EndInit();
            ((System.ComponentModel.ISupportInitialize)alertImageBindingSource).EndInit();
            tab_Audio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_Audio).EndInit();
            ((System.ComponentModel.ISupportInitialize)alertAudioBindingSource).EndInit();
            tab_Streamer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_StreamerMessages).EndInit();
            ((System.ComponentModel.ISupportInitialize)streamerAlertTextBindingSource).EndInit();
            tab_Viewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_ViewerMessages).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewerAlertTextBindingSource).EndInit();
            tab_Conditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_Conditions).EndInit();
            ((System.ComponentModel.ISupportInitialize)alertConditionBindingSource).EndInit();
            tab_PreviewControls.ResumeLayout(false);
            tab_PreviewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numud_Amount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestViewerMessage).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestStreamerMessage).EndInit();
            ((System.ComponentModel.ISupportInitialize)numud_TestAudio).EndInit();
            ((System.ComponentModel.ISupportInitialize)waypointBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.DataGridViewTextBoxColumn ViewerSubTier;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubscriptionTier;
        private System.Windows.Forms.DataGridViewTextBoxColumn BadgeString;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.ToolStripMenuItem ts_Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerSubTierDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subscriptionTierDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn emptyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasViewerTierDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasSubscriptionTierDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasBadgesDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasUsersDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasBannedNamesDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasKeywordsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasAmountsDataGridViewCheckBoxColumn;
    }
}



