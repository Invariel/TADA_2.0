namespace TADA_Advanced_Desktop_Alerts
{
    partial class Played_Alerts
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dg_Played = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dg_Pending = new System.Windows.Forms.DataGridView();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_Replay = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.txt_Status = new System.Windows.Forms.TextBox();
            this.viewerNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertMessageDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playedAlertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertMessageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pendingAlertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Played)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Pending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedAlertBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingAlertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Pause, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Replay, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Stop, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_Status, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 309);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dg_Played);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(548, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Played Alerts";
            // 
            // dg_Played
            // 
            this.dg_Played.AllowUserToAddRows = false;
            this.dg_Played.AutoGenerateColumns = false;
            this.dg_Played.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Played.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.viewerNameDataGridViewTextBoxColumn1,
            this.alertTypeDataGridViewTextBoxColumn1,
            this.alertMessageDataGridViewTextBoxColumn1});
            this.dg_Played.DataSource = this.playedAlertBindingSource;
            this.dg_Played.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Played.Location = new System.Drawing.Point(0, 13);
            this.dg_Played.Margin = new System.Windows.Forms.Padding(0);
            this.dg_Played.Name = "dg_Played";
            this.dg_Played.Size = new System.Drawing.Size(548, 120);
            this.dg_Played.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg_Pending);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(548, 133);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pending Alerts";
            // 
            // dg_Pending
            // 
            this.dg_Pending.AllowUserToAddRows = false;
            this.dg_Pending.AutoGenerateColumns = false;
            this.dg_Pending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Pending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.viewerNameDataGridViewTextBoxColumn,
            this.alertTypeDataGridViewTextBoxColumn,
            this.alertMessageDataGridViewTextBoxColumn});
            this.dg_Pending.DataSource = this.pendingAlertBindingSource;
            this.dg_Pending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Pending.Location = new System.Drawing.Point(0, 13);
            this.dg_Pending.Margin = new System.Windows.Forms.Padding(0);
            this.dg_Pending.Name = "dg_Pending";
            this.dg_Pending.Size = new System.Drawing.Size(548, 120);
            this.dg_Pending.TabIndex = 0;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Pause.Location = new System.Drawing.Point(557, 142);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(69, 133);
            this.btn_Pause.TabIndex = 3;
            this.btn_Pause.Text = "Pause Alerts";
            this.btn_Pause.UseVisualStyleBackColor = true;
            // 
            // btn_Replay
            // 
            this.btn_Replay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Replay.Location = new System.Drawing.Point(557, 3);
            this.btn_Replay.Name = "btn_Replay";
            this.btn_Replay.Size = new System.Drawing.Size(69, 133);
            this.btn_Replay.TabIndex = 4;
            this.btn_Replay.Text = "Replay Alert";
            this.btn_Replay.UseVisualStyleBackColor = true;
            // 
            // btn_Stop
            // 
            this.btn_Stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Stop.Location = new System.Drawing.Point(557, 281);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(69, 25);
            this.btn_Stop.TabIndex = 6;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            // 
            // txt_Status
            // 
            this.txt_Status.BackColor = System.Drawing.SystemColors.Control;
            this.txt_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Status.Enabled = false;
            this.txt_Status.Location = new System.Drawing.Point(0, 283);
            this.txt_Status.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.Size = new System.Drawing.Size(554, 20);
            this.txt_Status.TabIndex = 5;
            // 
            // viewerNameDataGridViewTextBoxColumn1
            // 
            this.viewerNameDataGridViewTextBoxColumn1.DataPropertyName = "ViewerName";
            this.viewerNameDataGridViewTextBoxColumn1.HeaderText = "Viewer Name";
            this.viewerNameDataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.viewerNameDataGridViewTextBoxColumn1.Name = "viewerNameDataGridViewTextBoxColumn1";
            // 
            // alertTypeDataGridViewTextBoxColumn1
            // 
            this.alertTypeDataGridViewTextBoxColumn1.DataPropertyName = "AlertType";
            this.alertTypeDataGridViewTextBoxColumn1.HeaderText = "Alert Type";
            this.alertTypeDataGridViewTextBoxColumn1.MinimumWidth = 80;
            this.alertTypeDataGridViewTextBoxColumn1.Name = "alertTypeDataGridViewTextBoxColumn1";
            // 
            // alertMessageDataGridViewTextBoxColumn1
            // 
            this.alertMessageDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.alertMessageDataGridViewTextBoxColumn1.DataPropertyName = "AlertMessage";
            this.alertMessageDataGridViewTextBoxColumn1.HeaderText = "Alert Message";
            this.alertMessageDataGridViewTextBoxColumn1.Name = "alertMessageDataGridViewTextBoxColumn1";
            // 
            // playedAlertBindingSource
            // 
            this.playedAlertBindingSource.AllowNew = true;
            this.playedAlertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.ListedAlert);
            // 
            // viewerNameDataGridViewTextBoxColumn
            // 
            this.viewerNameDataGridViewTextBoxColumn.DataPropertyName = "ViewerName";
            this.viewerNameDataGridViewTextBoxColumn.HeaderText = "Viewer Name";
            this.viewerNameDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.viewerNameDataGridViewTextBoxColumn.Name = "viewerNameDataGridViewTextBoxColumn";
            // 
            // alertTypeDataGridViewTextBoxColumn
            // 
            this.alertTypeDataGridViewTextBoxColumn.DataPropertyName = "AlertType";
            this.alertTypeDataGridViewTextBoxColumn.HeaderText = "Alert Type";
            this.alertTypeDataGridViewTextBoxColumn.MinimumWidth = 80;
            this.alertTypeDataGridViewTextBoxColumn.Name = "alertTypeDataGridViewTextBoxColumn";
            this.alertTypeDataGridViewTextBoxColumn.Width = 80;
            // 
            // alertMessageDataGridViewTextBoxColumn
            // 
            this.alertMessageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.alertMessageDataGridViewTextBoxColumn.DataPropertyName = "AlertMessage";
            this.alertMessageDataGridViewTextBoxColumn.HeaderText = "Alert Message";
            this.alertMessageDataGridViewTextBoxColumn.Name = "alertMessageDataGridViewTextBoxColumn";
            // 
            // pendingAlertBindingSource
            // 
            this.pendingAlertBindingSource.AllowNew = true;
            this.pendingAlertBindingSource.DataSource = typeof(TADA_Advanced_Desktop_Alerts.ListedAlert);
            // 
            // Played_Alerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 309);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Played_Alerts";
            this.Text = "Alert Controls";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Played)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Pending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedAlertBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingAlertBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dg_Played;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dg_Pending;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Button btn_Replay;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.TextBox txt_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertMessageDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource playedAlertBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertMessageDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource pendingAlertBindingSource;
    }
}