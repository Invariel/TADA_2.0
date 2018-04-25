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
            this.label1 = new System.Windows.Forms.Label();
            this.numud_AnimationWindowSizeY = new System.Windows.Forms.NumericUpDown();
            this.numud_AnimationWindowSizeX = new System.Windows.Forms.NumericUpDown();
            this.btn_CreateAlert = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Text = new System.Windows.Forms.Button();
            this.btn_Audio = new System.Windows.Forms.Button();
            this.btn_CreateAlertForm = new System.Windows.Forms.Button();
            this.dg_Alerts = new System.Windows.Forms.DataGridView();
            this.btn_SaveManifest = new System.Windows.Forms.Button();
            this.btn_LoadManifest = new System.Windows.Forms.Button();
            this.btn_MergeManifest = new System.Windows.Forms.Button();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AlertName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.audioCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.streamerCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewerCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeY);
            this.groupBox1.Controls.Add(this.numud_AnimationWindowSizeX);
            this.groupBox1.Location = new System.Drawing.Point(630, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Window";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "x";
            // 
            // numud_AnimationWindowSizeY
            // 
            this.numud_AnimationWindowSizeY.Location = new System.Drawing.Point(91, 29);
            this.numud_AnimationWindowSizeY.Name = "numud_AnimationWindowSizeY";
            this.numud_AnimationWindowSizeY.Size = new System.Drawing.Size(72, 20);
            this.numud_AnimationWindowSizeY.TabIndex = 0;
            // 
            // numud_AnimationWindowSizeX
            // 
            this.numud_AnimationWindowSizeX.Location = new System.Drawing.Point(7, 29);
            this.numud_AnimationWindowSizeX.Name = "numud_AnimationWindowSizeX";
            this.numud_AnimationWindowSizeX.Size = new System.Drawing.Size(72, 20);
            this.numud_AnimationWindowSizeX.TabIndex = 0;
            // 
            // btn_CreateAlert
            // 
            this.btn_CreateAlert.Location = new System.Drawing.Point(13, 13);
            this.btn_CreateAlert.Name = "btn_CreateAlert";
            this.btn_CreateAlert.Size = new System.Drawing.Size(122, 23);
            this.btn_CreateAlert.TabIndex = 1;
            this.btn_CreateAlert.Text = "&Create Alert";
            this.btn_CreateAlert.UseVisualStyleBackColor = true;
            this.btn_CreateAlert.Click += new System.EventHandler(this.btn_CreateAlert_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Text
            // 
            this.btn_Text.Location = new System.Drawing.Point(13, 73);
            this.btn_Text.Name = "btn_Text";
            this.btn_Text.Size = new System.Drawing.Size(122, 23);
            this.btn_Text.TabIndex = 3;
            this.btn_Text.Text = "Create Text";
            this.btn_Text.UseVisualStyleBackColor = true;
            this.btn_Text.Click += new System.EventHandler(this.btn_Text_Click);
            // 
            // btn_Audio
            // 
            this.btn_Audio.Location = new System.Drawing.Point(12, 102);
            this.btn_Audio.Name = "btn_Audio";
            this.btn_Audio.Size = new System.Drawing.Size(122, 23);
            this.btn_Audio.TabIndex = 3;
            this.btn_Audio.Text = "Create Audio";
            this.btn_Audio.UseVisualStyleBackColor = true;
            this.btn_Audio.Click += new System.EventHandler(this.btn_Audio_Click);
            // 
            // btn_CreateAlertForm
            // 
            this.btn_CreateAlertForm.Location = new System.Drawing.Point(171, 12);
            this.btn_CreateAlertForm.Name = "btn_CreateAlertForm";
            this.btn_CreateAlertForm.Size = new System.Drawing.Size(122, 23);
            this.btn_CreateAlertForm.TabIndex = 4;
            this.btn_CreateAlertForm.Text = "New Alert";
            this.btn_CreateAlertForm.UseVisualStyleBackColor = true;
            this.btn_CreateAlertForm.Click += new System.EventHandler(this.btn_CreateAlertForm_Click);
            // 
            // dg_Alerts
            // 
            this.dg_Alerts.AutoGenerateColumns = false;
            this.dg_Alerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Alerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlertName,
            this.alertTypeDataGridViewTextBoxColumn,
            this.imageCountDataGridViewTextBoxColumn,
            this.audioCountDataGridViewTextBoxColumn,
            this.streamerCountDataGridViewTextBoxColumn,
            this.viewerCountDataGridViewTextBoxColumn});
            this.dg_Alerts.DataSource = this.alertBindingSource;
            this.dg_Alerts.Location = new System.Drawing.Point(171, 43);
            this.dg_Alerts.Name = "dg_Alerts";
            this.dg_Alerts.Size = new System.Drawing.Size(612, 150);
            this.dg_Alerts.TabIndex = 5;
            // 
            // btn_SaveManifest
            // 
            this.btn_SaveManifest.Location = new System.Drawing.Point(403, 12);
            this.btn_SaveManifest.Name = "btn_SaveManifest";
            this.btn_SaveManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_SaveManifest.TabIndex = 6;
            this.btn_SaveManifest.Text = "Save Manifest";
            this.btn_SaveManifest.UseVisualStyleBackColor = true;
            // 
            // btn_LoadManifest
            // 
            this.btn_LoadManifest.Location = new System.Drawing.Point(532, 12);
            this.btn_LoadManifest.Name = "btn_LoadManifest";
            this.btn_LoadManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_LoadManifest.TabIndex = 7;
            this.btn_LoadManifest.Text = "Load Manifest";
            this.btn_LoadManifest.UseVisualStyleBackColor = true;
            // 
            // btn_MergeManifest
            // 
            this.btn_MergeManifest.Location = new System.Drawing.Point(661, 12);
            this.btn_MergeManifest.Name = "btn_MergeManifest";
            this.btn_MergeManifest.Size = new System.Drawing.Size(122, 23);
            this.btn_MergeManifest.TabIndex = 8;
            this.btn_MergeManifest.Text = "Merge Manifest";
            this.btn_MergeManifest.UseVisualStyleBackColor = true;
            // 
            // alertBindingSource
            // 
            this.alertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.TADAAlert);
            // 
            // AlertName
            // 
            this.AlertName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AlertName.DataPropertyName = "AlertName";
            this.AlertName.HeaderText = "AlertName";
            this.AlertName.Name = "AlertName";
            // 
            // alertTypeDataGridViewTextBoxColumn
            // 
            this.alertTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.alertTypeDataGridViewTextBoxColumn.DataPropertyName = "AlertType";
            this.alertTypeDataGridViewTextBoxColumn.FillWeight = 50F;
            this.alertTypeDataGridViewTextBoxColumn.HeaderText = "Alert Type";
            this.alertTypeDataGridViewTextBoxColumn.MinimumWidth = 55;
            this.alertTypeDataGridViewTextBoxColumn.Name = "alertTypeDataGridViewTextBoxColumn";
            // 
            // imageCountDataGridViewTextBoxColumn
            // 
            this.imageCountDataGridViewTextBoxColumn.DataPropertyName = "ImageCount";
            this.imageCountDataGridViewTextBoxColumn.FillWeight = 25F;
            this.imageCountDataGridViewTextBoxColumn.HeaderText = "Images";
            this.imageCountDataGridViewTextBoxColumn.MinimumWidth = 55;
            this.imageCountDataGridViewTextBoxColumn.Name = "imageCountDataGridViewTextBoxColumn";
            this.imageCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.imageCountDataGridViewTextBoxColumn.Width = 55;
            // 
            // audioCountDataGridViewTextBoxColumn
            // 
            this.audioCountDataGridViewTextBoxColumn.DataPropertyName = "AudioCount";
            this.audioCountDataGridViewTextBoxColumn.FillWeight = 25F;
            this.audioCountDataGridViewTextBoxColumn.HeaderText = "Audio";
            this.audioCountDataGridViewTextBoxColumn.MinimumWidth = 55;
            this.audioCountDataGridViewTextBoxColumn.Name = "audioCountDataGridViewTextBoxColumn";
            this.audioCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.audioCountDataGridViewTextBoxColumn.Width = 55;
            // 
            // streamerCountDataGridViewTextBoxColumn
            // 
            this.streamerCountDataGridViewTextBoxColumn.DataPropertyName = "StreamerCount";
            this.streamerCountDataGridViewTextBoxColumn.FillWeight = 25F;
            this.streamerCountDataGridViewTextBoxColumn.HeaderText = "Your Text";
            this.streamerCountDataGridViewTextBoxColumn.MinimumWidth = 55;
            this.streamerCountDataGridViewTextBoxColumn.Name = "streamerCountDataGridViewTextBoxColumn";
            this.streamerCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.streamerCountDataGridViewTextBoxColumn.Width = 55;
            // 
            // viewerCountDataGridViewTextBoxColumn
            // 
            this.viewerCountDataGridViewTextBoxColumn.DataPropertyName = "ViewerCount";
            this.viewerCountDataGridViewTextBoxColumn.FillWeight = 25F;
            this.viewerCountDataGridViewTextBoxColumn.HeaderText = "Viewer Text";
            this.viewerCountDataGridViewTextBoxColumn.MinimumWidth = 55;
            this.viewerCountDataGridViewTextBoxColumn.Name = "viewerCountDataGridViewTextBoxColumn";
            this.viewerCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.viewerCountDataGridViewTextBoxColumn.Width = 55;
            // 
            // MainTADAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 294);
            this.Controls.Add(this.btn_MergeManifest);
            this.Controls.Add(this.btn_LoadManifest);
            this.Controls.Add(this.btn_SaveManifest);
            this.Controls.Add(this.dg_Alerts);
            this.Controls.Add(this.btn_CreateAlertForm);
            this.Controls.Add(this.btn_Audio);
            this.Controls.Add(this.btn_Text);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_CreateAlert);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainTADAForm";
            this.Text = "TADA Advanced Desktop Alerts";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_AnimationWindowSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Alerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeY;
        private System.Windows.Forms.NumericUpDown numud_AnimationWindowSizeX;
        private System.Windows.Forms.Button btn_CreateAlert;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Text;
        private System.Windows.Forms.Button btn_Audio;
        private System.Windows.Forms.Button btn_CreateAlertForm;
        private System.Windows.Forms.BindingSource alertBindingSource;
        private System.Windows.Forms.DataGridView dg_Alerts;
        private System.Windows.Forms.Button btn_SaveManifest;
        private System.Windows.Forms.Button btn_LoadManifest;
        private System.Windows.Forms.Button btn_MergeManifest;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlertName;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn audioCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn streamerCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerCountDataGridViewTextBoxColumn;
    }
}

