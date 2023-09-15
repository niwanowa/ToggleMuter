namespace ToggreMuter
{
    partial class HotKeySettingForm
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
            this.labelHotKey = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelHotKey
            // 
            this.labelHotKey.AutoSize = true;
            this.labelHotKey.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.labelHotKey.Location = new System.Drawing.Point(71, 78);
            this.labelHotKey.Name = "labelHotKey";
            this.labelHotKey.Size = new System.Drawing.Size(264, 12);
            this.labelHotKey.TabIndex = 0;
            this.labelHotKey.Text = "設定したいキーを押下してください(例: ctrl + shift + M)";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(279, 281);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 1;
            this.buttonSubmit.Text = "OK";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // HotKeySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 318);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.labelHotKey);
            this.KeyPreview = true;
            this.Name = "HotKeySettingForm";
            this.Text = "HotKeySettingForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotKeySettingForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HotKeySettingForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHotKey;
        private System.Windows.Forms.Button buttonSubmit;
    }
}