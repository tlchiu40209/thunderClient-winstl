namespace thunderClient
{
    partial class SettingsForm
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
            labelUrl = new Label();
            txtUrl = new TextBox();
            labelRange = new Label();
            labelLon = new Label();
            numLon = new NumericUpDown();
            labelLat = new Label();
            numLat = new NumericUpDown();
            labelTime = new Label();
            numTime = new NumericUpDown();
            cmbGBox = new GroupBox();
            labelCmd = new Label();
            txtCmd = new TextBox();
            cmbAction = new ComboBox();
            btnSave = new Button();
            numRange = new NumericUpDown();
            numCk = new NumericUpDown();
            labelCk = new Label();
            ckbNotice = new CheckBox();
            ckbStartup = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numLon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTime).BeginInit();
            cmbGBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCk).BeginInit();
            SuspendLayout();
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.Location = new Point(18, 15);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(115, 15);
            labelUrl.TabIndex = 0;
            labelUrl.Text = "雷擊及資訊參照來源";
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(139, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(466, 23);
            txtUrl.TabIndex = 1;
            // 
            // labelRange
            // 
            labelRange.AutoSize = true;
            labelRange.Location = new Point(18, 80);
            labelRange.Name = "labelRange";
            labelRange.Size = new Size(109, 15);
            labelRange.TabIndex = 2;
            labelRange.Text = "雷擊防護範圍 (KM)";
            // 
            // labelLon
            // 
            labelLon.AutoSize = true;
            labelLon.Location = new Point(18, 109);
            labelLon.Name = "labelLon";
            labelLon.Size = new Size(79, 15);
            labelLon.TabIndex = 4;
            labelLon.Text = "本機位置經度";
            // 
            // numLon
            // 
            numLon.DecimalPlaces = 6;
            numLon.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numLon.Location = new Point(139, 107);
            numLon.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numLon.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numLon.Name = "numLon";
            numLon.Size = new Size(120, 23);
            numLon.TabIndex = 5;
            // 
            // labelLat
            // 
            labelLat.AutoSize = true;
            labelLat.Location = new Point(18, 138);
            labelLat.Name = "labelLat";
            labelLat.Size = new Size(79, 15);
            labelLat.TabIndex = 6;
            labelLat.Text = "本機位置緯度";
            // 
            // numLat
            // 
            numLat.DecimalPlaces = 6;
            numLat.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numLat.Location = new Point(139, 136);
            numLat.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            numLat.Minimum = new decimal(new int[] { 90, 0, 0, int.MinValue });
            numLat.Name = "numLat";
            numLat.Size = new Size(120, 23);
            numLat.TabIndex = 7;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Location = new Point(18, 167);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(102, 15);
            labelTime.TabIndex = 8;
            labelTime.Text = "防護反應時間 (秒)";
            // 
            // numTime
            // 
            numTime.Location = new Point(139, 165);
            numTime.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numTime.Name = "numTime";
            numTime.Size = new Size(120, 23);
            numTime.TabIndex = 9;
            // 
            // cmbGBox
            // 
            cmbGBox.Controls.Add(labelCmd);
            cmbGBox.Controls.Add(txtCmd);
            cmbGBox.Controls.Add(cmbAction);
            cmbGBox.Location = new Point(305, 49);
            cmbGBox.Name = "cmbGBox";
            cmbGBox.Size = new Size(309, 104);
            cmbGBox.TabIndex = 10;
            cmbGBox.TabStop = false;
            cmbGBox.Text = "雷擊發生執行行為";
            // 
            // labelCmd
            // 
            labelCmd.AutoSize = true;
            labelCmd.Location = new Point(6, 48);
            labelCmd.Name = "labelCmd";
            labelCmd.Size = new Size(55, 15);
            labelCmd.TabIndex = 11;
            labelCmd.Text = "執行程式";
            // 
            // txtCmd
            // 
            txtCmd.Location = new Point(6, 66);
            txtCmd.Name = "txtCmd";
            txtCmd.Size = new Size(294, 23);
            txtCmd.TabIndex = 11;
            // 
            // cmbAction
            // 
            cmbAction.FormattingEnabled = true;
            cmbAction.Items.AddRange(new object[] { "只提醒", "立刻關機", "提醒關機", "提醒拔除輸入電源 (筆電或UPS 模式)", "執行指定程式" });
            cmbAction.Location = new Point(6, 22);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(294, 23);
            cmbAction.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(539, 172);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 11;
            btnSave.Text = "儲存設定";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // numRange
            // 
            numRange.DecimalPlaces = 1;
            numRange.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numRange.Location = new Point(139, 78);
            numRange.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numRange.Name = "numRange";
            numRange.Size = new Size(120, 23);
            numRange.TabIndex = 3;
            // 
            // numCk
            // 
            numCk.Location = new Point(139, 49);
            numCk.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            numCk.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCk.Name = "numCk";
            numCk.Size = new Size(120, 23);
            numCk.TabIndex = 13;
            numCk.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelCk
            // 
            labelCk.AutoSize = true;
            labelCk.Location = new Point(18, 51);
            labelCk.Name = "labelCk";
            labelCk.Size = new Size(78, 15);
            labelCk.TabIndex = 12;
            labelCk.Text = "查詢頻率 (秒)";
            // 
            // ckbNotice
            // 
            ckbNotice.AutoSize = true;
            ckbNotice.Location = new Point(305, 159);
            ckbNotice.Name = "ckbNotice";
            ckbNotice.Size = new Size(184, 19);
            ckbNotice.TabIndex = 14;
            ckbNotice.Text = "強制通知 (使用 MessageBox)";
            ckbNotice.UseVisualStyleBackColor = true;
            // 
            // ckbStartup
            // 
            ckbStartup.AutoSize = true;
            ckbStartup.Location = new Point(305, 184);
            ckbStartup.Name = "ckbStartup";
            ckbStartup.Size = new Size(158, 19);
            ckbStartup.TabIndex = 15;
            ckbStartup.Text = "電腦帳戶登入後啟動程式";
            ckbStartup.UseVisualStyleBackColor = true;
            ckbStartup.CheckedChanged += ckbStartup_Changed;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 209);
            Controls.Add(ckbStartup);
            Controls.Add(ckbNotice);
            Controls.Add(numCk);
            Controls.Add(labelCk);
            Controls.Add(btnSave);
            Controls.Add(cmbGBox);
            Controls.Add(numTime);
            Controls.Add(labelTime);
            Controls.Add(numLat);
            Controls.Add(labelLat);
            Controls.Add(numLon);
            Controls.Add(labelLon);
            Controls.Add(numRange);
            Controls.Add(labelRange);
            Controls.Add(txtUrl);
            Controls.Add(labelUrl);
            Name = "SettingsForm";
            Text = "Wayne 雷擊防護程式設定";
            Load += SettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)numLon).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTime).EndInit();
            cmbGBox.ResumeLayout(false);
            cmbGBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCk).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUrl;
        private TextBox txtUrl;
        private Label labelRange;
        private Label labelLon;
        private NumericUpDown numLon;
        private Label labelLat;
        private NumericUpDown numLat;
        private Label labelTime;
        private NumericUpDown numTime;
        private GroupBox cmbGBox;
        private ComboBox cmbAction;
        private TextBox txtCmd;
        private Label labelCmd;
        private Button btnSave;
        private NumericUpDown numRange;
        private NumericUpDown numCk;
        private Label labelCk;
        private CheckBox ckbNotice;
        private CheckBox ckbStartup;
    }
}