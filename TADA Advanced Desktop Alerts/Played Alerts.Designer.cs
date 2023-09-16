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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Played_Alerts));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            dg_Played = new System.Windows.Forms.DataGridView();
            viewerNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            alertTypeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            alertMessageDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            playedAlertBindingSource = new System.Windows.Forms.BindingSource(components);
            groupBox2 = new System.Windows.Forms.GroupBox();
            dg_Pending = new System.Windows.Forms.DataGridView();
            viewerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            alertTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ReceivedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            alertMessageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pendingAlertBindingSource = new System.Windows.Forms.BindingSource(components);
            btn_Pause = new System.Windows.Forms.Button();
            btn_Replay = new System.Windows.Forms.Button();
            btn_Stop = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            lbl_Websocket = new System.Windows.Forms.Label();
            pb_FollowerList = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            pb_Websocket = new System.Windows.Forms.PictureBox();
            pb_TwitchApi = new System.Windows.Forms.PictureBox();
            label2 = new System.Windows.Forms.Label();
            pb_PubSub = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_Played).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playedAlertBindingSource).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_Pending).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pendingAlertBindingSource).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_FollowerList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_Websocket).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_TwitchApi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_PubSub).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel1.Controls.Add(btn_Pause, 1, 1);
            tableLayoutPanel1.Controls.Add(btn_Replay, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_Stop, 1, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            tableLayoutPanel1.Size = new System.Drawing.Size(734, 380);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dg_Played);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(4, 3);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(0);
            groupBox1.Size = new System.Drawing.Size(638, 149);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Played Alerts";
            // 
            // dg_Played
            // 
            dg_Played.AllowUserToAddRows = false;
            dg_Played.AutoGenerateColumns = false;
            dg_Played.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_Played.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { viewerNameDataGridViewTextBoxColumn1, alertTypeDataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, alertMessageDataGridViewTextBoxColumn1 });
            dg_Played.DataSource = playedAlertBindingSource;
            dg_Played.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_Played.Location = new System.Drawing.Point(0, 16);
            dg_Played.Margin = new System.Windows.Forms.Padding(0);
            dg_Played.Name = "dg_Played";
            dg_Played.Size = new System.Drawing.Size(638, 133);
            dg_Played.TabIndex = 0;
            // 
            // viewerNameDataGridViewTextBoxColumn1
            // 
            viewerNameDataGridViewTextBoxColumn1.DataPropertyName = "ViewerName";
            viewerNameDataGridViewTextBoxColumn1.HeaderText = "Viewer Name";
            viewerNameDataGridViewTextBoxColumn1.MinimumWidth = 100;
            viewerNameDataGridViewTextBoxColumn1.Name = "viewerNameDataGridViewTextBoxColumn1";
            // 
            // alertTypeDataGridViewTextBoxColumn1
            // 
            alertTypeDataGridViewTextBoxColumn1.DataPropertyName = "AlertType";
            alertTypeDataGridViewTextBoxColumn1.HeaderText = "Alert Type";
            alertTypeDataGridViewTextBoxColumn1.MinimumWidth = 80;
            alertTypeDataGridViewTextBoxColumn1.Name = "alertTypeDataGridViewTextBoxColumn1";
            alertTypeDataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewTextBoxColumn2.DataPropertyName = "ReceivedTime";
            dataGridViewTextBoxColumn2.HeaderText = "Received Time";
            dataGridViewTextBoxColumn2.MinimumWidth = 110;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 110;
            // 
            // alertMessageDataGridViewTextBoxColumn1
            // 
            alertMessageDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            alertMessageDataGridViewTextBoxColumn1.DataPropertyName = "AlertMessage";
            alertMessageDataGridViewTextBoxColumn1.HeaderText = "Alert Message";
            alertMessageDataGridViewTextBoxColumn1.Name = "alertMessageDataGridViewTextBoxColumn1";
            // 
            // playedAlertBindingSource
            // 
            playedAlertBindingSource.AllowNew = true;
            playedAlertBindingSource.DataSource = typeof(ListedAlert);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dg_Pending);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(4, 158);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(0);
            groupBox2.Size = new System.Drawing.Size(638, 149);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pending Alerts";
            // 
            // dg_Pending
            // 
            dg_Pending.AllowUserToAddRows = false;
            dg_Pending.AutoGenerateColumns = false;
            dg_Pending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_Pending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { viewerNameDataGridViewTextBoxColumn, alertTypeDataGridViewTextBoxColumn, ReceivedTime, alertMessageDataGridViewTextBoxColumn });
            dg_Pending.DataSource = pendingAlertBindingSource;
            dg_Pending.Dock = System.Windows.Forms.DockStyle.Fill;
            dg_Pending.Location = new System.Drawing.Point(0, 16);
            dg_Pending.Margin = new System.Windows.Forms.Padding(0);
            dg_Pending.Name = "dg_Pending";
            dg_Pending.Size = new System.Drawing.Size(638, 133);
            dg_Pending.TabIndex = 0;
            // 
            // viewerNameDataGridViewTextBoxColumn
            // 
            viewerNameDataGridViewTextBoxColumn.DataPropertyName = "ViewerName";
            viewerNameDataGridViewTextBoxColumn.HeaderText = "Viewer Name";
            viewerNameDataGridViewTextBoxColumn.MinimumWidth = 100;
            viewerNameDataGridViewTextBoxColumn.Name = "viewerNameDataGridViewTextBoxColumn";
            // 
            // alertTypeDataGridViewTextBoxColumn
            // 
            alertTypeDataGridViewTextBoxColumn.DataPropertyName = "AlertType";
            alertTypeDataGridViewTextBoxColumn.HeaderText = "Alert Type";
            alertTypeDataGridViewTextBoxColumn.MinimumWidth = 80;
            alertTypeDataGridViewTextBoxColumn.Name = "alertTypeDataGridViewTextBoxColumn";
            alertTypeDataGridViewTextBoxColumn.Width = 80;
            // 
            // ReceivedTime
            // 
            ReceivedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            ReceivedTime.DataPropertyName = "ReceivedTime";
            ReceivedTime.HeaderText = "Received Time";
            ReceivedTime.MinimumWidth = 110;
            ReceivedTime.Name = "ReceivedTime";
            ReceivedTime.Width = 110;
            // 
            // alertMessageDataGridViewTextBoxColumn
            // 
            alertMessageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            alertMessageDataGridViewTextBoxColumn.DataPropertyName = "AlertMessage";
            alertMessageDataGridViewTextBoxColumn.HeaderText = "Alert Message";
            alertMessageDataGridViewTextBoxColumn.Name = "alertMessageDataGridViewTextBoxColumn";
            // 
            // pendingAlertBindingSource
            // 
            pendingAlertBindingSource.AllowNew = true;
            pendingAlertBindingSource.DataSource = typeof(ListedAlert);
            // 
            // btn_Pause
            // 
            btn_Pause.Dock = System.Windows.Forms.DockStyle.Right;
            btn_Pause.Location = new System.Drawing.Point(650, 158);
            btn_Pause.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Pause.Name = "btn_Pause";
            btn_Pause.Size = new System.Drawing.Size(80, 149);
            btn_Pause.TabIndex = 40;
            btn_Pause.Text = "Pause Alerts";
            btn_Pause.UseVisualStyleBackColor = true;
            // 
            // btn_Replay
            // 
            btn_Replay.Dock = System.Windows.Forms.DockStyle.Right;
            btn_Replay.Location = new System.Drawing.Point(650, 3);
            btn_Replay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Replay.Name = "btn_Replay";
            btn_Replay.Size = new System.Drawing.Size(80, 149);
            btn_Replay.TabIndex = 30;
            btn_Replay.Text = "Replay Alert";
            btn_Replay.UseVisualStyleBackColor = true;
            // 
            // btn_Stop
            // 
            btn_Stop.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_Stop.Location = new System.Drawing.Point(650, 313);
            btn_Stop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new System.Drawing.Size(80, 64);
            btn_Stop.TabIndex = 50;
            btn_Stop.Text = "Stop Alert";
            btn_Stop.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(lbl_Websocket);
            panel1.Controls.Add(pb_FollowerList);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(pb_Websocket);
            panel1.Controls.Add(pb_TwitchApi);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pb_PubSub);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(4, 313);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(638, 64);
            panel1.TabIndex = 60;
            // 
            // lbl_Websocket
            // 
            lbl_Websocket.AutoSize = true;
            lbl_Websocket.Location = new System.Drawing.Point(321, 36);
            lbl_Websocket.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_Websocket.Name = "lbl_Websocket";
            lbl_Websocket.Size = new System.Drawing.Size(202, 15);
            lbl_Websocket.TabIndex = 6;
            lbl_Websocket.Text = "WebSocket Connected on Port 21573";
            // 
            // pb_FollowerList
            // 
            pb_FollowerList.Image = Properties.Resources.circle_red;
            pb_FollowerList.InitialImage = null;
            pb_FollowerList.Location = new System.Drawing.Point(271, 35);
            pb_FollowerList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pb_FollowerList.Name = "pb_FollowerList";
            pb_FollowerList.Size = new System.Drawing.Size(19, 18);
            pb_FollowerList.TabIndex = 5;
            pb_FollowerList.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 37);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(121, 15);
            label3.TabIndex = 4;
            label3.Text = "Follower List Updated";
            // 
            // pb_Websocket
            // 
            pb_Websocket.ErrorImage = null;
            pb_Websocket.Image = Properties.Resources.circle_red;
            pb_Websocket.InitialImage = null;
            pb_Websocket.Location = new System.Drawing.Point(617, 33);
            pb_Websocket.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pb_Websocket.Name = "pb_Websocket";
            pb_Websocket.Size = new System.Drawing.Size(19, 18);
            pb_Websocket.TabIndex = 3;
            pb_Websocket.TabStop = false;
            // 
            // pb_TwitchApi
            // 
            pb_TwitchApi.ErrorImage = null;
            pb_TwitchApi.Image = Properties.Resources.circle_red;
            pb_TwitchApi.InitialImage = null;
            pb_TwitchApi.Location = new System.Drawing.Point(617, 5);
            pb_TwitchApi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pb_TwitchApi.Name = "pb_TwitchApi";
            pb_TwitchApi.Size = new System.Drawing.Size(19, 18);
            pb_TwitchApi.TabIndex = 3;
            pb_TwitchApi.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(321, 7);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(140, 15);
            label2.TabIndex = 2;
            label2.Text = "Chat Messages and Raids";
            // 
            // pb_PubSub
            // 
            pb_PubSub.Image = Properties.Resources.circle_red;
            pb_PubSub.InitialImage = null;
            pb_PubSub.Location = new System.Drawing.Point(271, 5);
            pb_PubSub.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pb_PubSub.Name = "pb_PubSub";
            pb_PubSub.Size = new System.Drawing.Size(19, 18);
            pb_PubSub.TabIndex = 1;
            pb_PubSub.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 7);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(242, 15);
            label1.TabIndex = 0;
            label1.Text = "Bits, Follows, Game Purchases, Subscriptions";
            // 
            // Played_Alerts
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(734, 380);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Played_Alerts";
            Text = "Played Alerts";
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_Played).EndInit();
            ((System.ComponentModel.ISupportInitialize)playedAlertBindingSource).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_Pending).EndInit();
            ((System.ComponentModel.ISupportInitialize)pendingAlertBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_FollowerList).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_Websocket).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_TwitchApi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_PubSub).EndInit();
            ResumeLayout(false);
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
        private System.Windows.Forms.BindingSource playedAlertBindingSource;
        private System.Windows.Forms.BindingSource pendingAlertBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pb_FollowerList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pb_TwitchApi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pb_PubSub;
        private System.Windows.Forms.Label lbl_Websocket;
        private System.Windows.Forms.PictureBox pb_Websocket;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertMessageDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertMessageDataGridViewTextBoxColumn;
    }
}