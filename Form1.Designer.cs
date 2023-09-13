namespace ToggleMuter
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.appList = new System.Windows.Forms.ListBox();
            this.testlabel = new System.Windows.Forms.Label();
            this.labelProcessList = new System.Windows.Forms.Label();
            this.IgnoredList = new System.Windows.Forms.ListBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.muteButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonSettingHotKey = new System.Windows.Forms.Button();
            this.labelIgnoredList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // appList
            // 
            this.appList.FormattingEnabled = true;
            this.appList.ItemHeight = 12;
            this.appList.Location = new System.Drawing.Point(14, 27);
            this.appList.Name = "appList";
            this.appList.Size = new System.Drawing.Size(518, 472);
            this.appList.TabIndex = 0;
            this.appList.SelectedIndexChanged += new System.EventHandler(this.appList_SelectedIndexChanged);
            this.appList.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PressSettingHotKey);
            // 
            // testlabel
            // 
            this.testlabel.AutoSize = true;
            this.testlabel.Location = new System.Drawing.Point(416, 9);
            this.testlabel.Name = "testlabel";
            this.testlabel.Size = new System.Drawing.Size(116, 12);
            this.testlabel.TabIndex = 1;
            this.testlabel.Text = "ホットキー設定：未設定";
            // 
            // labelProcessList
            // 
            this.labelProcessList.AutoSize = true;
            this.labelProcessList.Location = new System.Drawing.Point(12, 9);
            this.labelProcessList.Name = "labelProcessList";
            this.labelProcessList.Size = new System.Drawing.Size(66, 12);
            this.labelProcessList.TabIndex = 2;
            this.labelProcessList.Text = "プロセス一覧";
            // 
            // IgnoredList
            // 
            this.IgnoredList.FormattingEnabled = true;
            this.IgnoredList.ItemHeight = 12;
            this.IgnoredList.Location = new System.Drawing.Point(667, 24);
            this.IgnoredList.Name = "IgnoredList";
            this.IgnoredList.Size = new System.Drawing.Size(222, 472);
            this.IgnoredList.TabIndex = 3;
            // 
            // moveButton
            // 
            this.moveButton.AutoSize = true;
            this.moveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.moveButton.Location = new System.Drawing.Point(566, 231);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(87, 23);
            this.moveButton.TabIndex = 4;
            this.moveButton.Text = "追加/削除";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // muteButton
            // 
            this.muteButton.AutoSize = true;
            this.muteButton.Location = new System.Drawing.Point(566, 175);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(87, 23);
            this.muteButton.TabIndex = 5;
            this.muteButton.Text = "ミュート";
            this.muteButton.UseVisualStyleBackColor = true;
            this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonSettingHotKey
            // 
            this.buttonSettingHotKey.AutoSize = true;
            this.buttonSettingHotKey.Location = new System.Drawing.Point(566, 129);
            this.buttonSettingHotKey.Name = "buttonSettingHotKey";
            this.buttonSettingHotKey.Size = new System.Drawing.Size(87, 23);
            this.buttonSettingHotKey.TabIndex = 6;
            this.buttonSettingHotKey.Text = "ホットキー設定";
            this.buttonSettingHotKey.UseVisualStyleBackColor = true;
            this.buttonSettingHotKey.Click += new System.EventHandler(this.buttonSettingHotKey_Click);
            // 
            // labelIgnoredList
            // 
            this.labelIgnoredList.AutoSize = true;
            this.labelIgnoredList.Location = new System.Drawing.Point(665, 9);
            this.labelIgnoredList.Name = "labelIgnoredList";
            this.labelIgnoredList.Size = new System.Drawing.Size(86, 12);
            this.labelIgnoredList.TabIndex = 7;
            this.labelIgnoredList.Text = "ミュート除外リスト";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(900, 519);
            this.Controls.Add(this.labelIgnoredList);
            this.Controls.Add(this.buttonSettingHotKey);
            this.Controls.Add(this.muteButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.IgnoredList);
            this.Controls.Add(this.labelProcessList);
            this.Controls.Add(this.testlabel);
            this.Controls.Add(this.appList);
            this.Name = "MainForm";
            this.Text = "ToggleMuter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox appList;
        private System.Windows.Forms.Label testlabel;
        private System.Windows.Forms.Label labelProcessList;
        private System.Windows.Forms.ListBox IgnoredList;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button muteButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonSettingHotKey;
        private System.Windows.Forms.Label labelIgnoredList;
    }
}

