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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.btn_CreateAlertForm = new System.Windows.Forms.Button();
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
            this.alertNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.audioCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.streamerCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewerCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numud_bgB);
            this.groupBox1.Controls.Add(this.numud_bgG);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numud_bgR);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_ShowHideAnimationWindow);
            this.groupBox1.Controls.Add(this.txt_BackgroundARGB);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 141);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Window";
            // 
            // numud_bgB
            // 
            this.numud_bgB.Location = new System.Drawing.Point(54, 111);
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
            this.numud_bgG.Location = new System.Drawing.Point(54, 85);
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
            this.label6.Location = new System.Drawing.Point(21, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Blue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Green";
            // 
            // numud_bgR
            // 
            this.numud_bgR.Location = new System.Drawing.Point(54, 59);
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
            this.label4.Location = new System.Drawing.Point(22, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Red";
            // 
            // btn_ShowHideAnimationWindow
            // 
            this.btn_ShowHideAnimationWindow.Location = new System.Drawing.Point(118, 110);
            this.btn_ShowHideAnimationWindow.Name = "btn_ShowHideAnimationWindow";
            this.btn_ShowHideAnimationWindow.Size = new System.Drawing.Size(156, 23);
            this.btn_ShowHideAnimationWindow.TabIndex = 15;
            this.btn_ShowHideAnimationWindow.Text = "Hide &Animation Window";
            this.btn_ShowHideAnimationWindow.UseVisualStyleBackColor = true;
            this.btn_ShowHideAnimationWindow.Click += new System.EventHandler(this.HideAnimationWindow);
            // 
            // txt_BackgroundARGB
            // 
            this.txt_BackgroundARGB.Location = new System.Drawing.Point(24, 33);
            this.txt_BackgroundARGB.Name = "txt_BackgroundARGB";
            this.txt_BackgroundARGB.Size = new System.Drawing.Size(72, 20);
            this.txt_BackgroundARGB.TabIndex = 11;
            this.txt_BackgroundARGB.Text = "#FFFFFF";
            // 
            // numud_AnimationWindowSizeY
            // 
            this.numud_AnimationWindowSizeY.Location = new System.Drawing.Point(202, 34);
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
            this.label2.Location = new System.Drawing.Point(145, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "&Window Dimensions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "&Background Colour";
            // 
            // numud_AnimationWindowSizeX
            // 
            this.numud_AnimationWindowSizeX.Location = new System.Drawing.Point(118, 34);
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
            this.label1.Location = new System.Drawing.Point(190, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "x";
            // 
            // btn_CreateAlertForm
            // 
            this.btn_CreateAlertForm.Location = new System.Drawing.Point(12, 12);
            this.btn_CreateAlertForm.Name = "btn_CreateAlertForm";
            this.btn_CreateAlertForm.Size = new System.Drawing.Size(122, 23);
            this.btn_CreateAlertForm.TabIndex = 1;
            this.btn_CreateAlertForm.Text = "New Alert";
            this.btn_CreateAlertForm.UseVisualStyleBackColor = true;
            this.btn_CreateAlertForm.Click += new System.EventHandler(this.btn_CreateAlertForm_Click);
            // 
            // btn_SaveManifest
            // 
            this.btn_SaveManifest.Location = new System.Drawing.Point(353, 12);
            this.btn_SaveManifest.Name = "btn_SaveManifest";
            this.btn_SaveManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_SaveManifest.TabIndex = 2;
            this.btn_SaveManifest.Text = "Save Manifest";
            this.btn_SaveManifest.UseVisualStyleBackColor = true;
            // 
            // btn_LoadManifest
            // 
            this.btn_LoadManifest.Location = new System.Drawing.Point(482, 12);
            this.btn_LoadManifest.Name = "btn_LoadManifest";
            this.btn_LoadManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_LoadManifest.TabIndex = 3;
            this.btn_LoadManifest.Text = "Load Manifest";
            this.btn_LoadManifest.UseVisualStyleBackColor = true;
            // 
            // btn_MergeManifest
            // 
            this.btn_MergeManifest.Location = new System.Drawing.Point(611, 12);
            this.btn_MergeManifest.Name = "btn_MergeManifest";
            this.btn_MergeManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_MergeManifest.TabIndex = 4;
            this.btn_MergeManifest.Text = "Merge Manifest";
            this.btn_MergeManifest.UseVisualStyleBackColor = true;
            // 
            // dg_Alerts
            // 
            this.dg_Alerts.AutoGenerateColumns = false;
            this.dg_Alerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Alerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alertNameDataGridViewTextBoxColumn,
            this.alertTypeDataGridViewTextBoxColumn,
            this.imageCountDataGridViewTextBoxColumn,
            this.audioCountDataGridViewTextBoxColumn,
            this.streamerCountDataGridViewTextBoxColumn,
            this.viewerCountDataGridViewTextBoxColumn,
            this.Edit});
            this.dg_Alerts.DataSource = this.alertBindingSource;
            this.dg_Alerts.Location = new System.Drawing.Point(12, 43);
            this.dg_Alerts.Name = "dg_Alerts";
            this.dg_Alerts.Size = new System.Drawing.Size(721, 184);
            this.dg_Alerts.TabIndex = 5;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 40;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
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
            this.groupBox2.Location = new System.Drawing.Point(299, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 102);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Twitch Settings";
            // 
            // btn_LoadChannelDetails
            // 
            this.btn_LoadChannelDetails.Location = new System.Drawing.Point(272, 73);
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
            this.btn_RevokeAccessToken.Location = new System.Drawing.Point(318, 43);
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
            this.txt_AccessToken.Size = new System.Drawing.Size(185, 20);
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
            this.txt_ChannelName.Size = new System.Drawing.Size(301, 20);
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
            this.btn_Connect.Location = new System.Drawing.Point(577, 343);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(156, 23);
            this.btn_Connect.TabIndex = 17;
            this.btn_Connect.Text = "Connect!";
            this.btn_Connect.UseVisualStyleBackColor = true;
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
            // imageCountDataGridViewTextBoxColumn
            // 
            this.imageCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageCountDataGridViewTextBoxColumn.DataPropertyName = "ImageCount";
            this.imageCountDataGridViewTextBoxColumn.HeaderText = "Images";
            this.imageCountDataGridViewTextBoxColumn.MinimumWidth = 90;
            this.imageCountDataGridViewTextBoxColumn.Name = "imageCountDataGridViewTextBoxColumn";
            this.imageCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.imageCountDataGridViewTextBoxColumn.Width = 90;
            // 
            // audioCountDataGridViewTextBoxColumn
            // 
            this.audioCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.audioCountDataGridViewTextBoxColumn.DataPropertyName = "AudioCount";
            this.audioCountDataGridViewTextBoxColumn.HeaderText = "Audio";
            this.audioCountDataGridViewTextBoxColumn.MinimumWidth = 90;
            this.audioCountDataGridViewTextBoxColumn.Name = "audioCountDataGridViewTextBoxColumn";
            this.audioCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.audioCountDataGridViewTextBoxColumn.Width = 90;
            // 
            // streamerCountDataGridViewTextBoxColumn
            // 
            this.streamerCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.streamerCountDataGridViewTextBoxColumn.DataPropertyName = "StreamerCount";
            this.streamerCountDataGridViewTextBoxColumn.HeaderText = "Your Text";
            this.streamerCountDataGridViewTextBoxColumn.MinimumWidth = 90;
            this.streamerCountDataGridViewTextBoxColumn.Name = "streamerCountDataGridViewTextBoxColumn";
            this.streamerCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.streamerCountDataGridViewTextBoxColumn.Width = 90;
            // 
            // viewerCountDataGridViewTextBoxColumn
            // 
            this.viewerCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.viewerCountDataGridViewTextBoxColumn.DataPropertyName = "ViewerCount";
            this.viewerCountDataGridViewTextBoxColumn.HeaderText = "Viewer Text";
            this.viewerCountDataGridViewTextBoxColumn.MinimumWidth = 90;
            this.viewerCountDataGridViewTextBoxColumn.Name = "viewerCountDataGridViewTextBoxColumn";
            this.viewerCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.viewerCountDataGridViewTextBoxColumn.Width = 90;
            // 
            // alertBindingSource
            // 
            this.alertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlert);
            // 
            // MainTADAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 378);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dg_Alerts);
            this.Controls.Add(this.btn_MergeManifest);
            this.Controls.Add(this.btn_LoadManifest);
            this.Controls.Add(this.btn_SaveManifest);
            this.Controls.Add(this.btn_CreateAlertForm);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainTADAForm";
            this.Text = "TADA Advanced Desktop Alerts";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_bgR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeY;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeX;
        private System.Windows.Forms.Button btn_ShowHideAnimationWindow;
        private System.Windows.Forms.Button btn_CreateAlertForm;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn alertNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn audioCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn streamerCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.Button btn_LoadChannelDetails;
        private System.Windows.Forms.Button btn_SaveChannelDetails;
    }
}

