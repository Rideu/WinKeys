namespace WinKeysN
{
    partial class WinKeys
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
            this.labelKeys = new System.Windows.Forms.Label();
            this.winHeader1 = new GrayLib.WinHeader();
            this.checkedWhiteListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // labelKeys
            // 
            this.labelKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeys.Font = new System.Drawing.Font("Corbel", 12F);
            this.labelKeys.ForeColor = System.Drawing.Color.White;
            this.labelKeys.Location = new System.Drawing.Point(12, 41);
            this.labelKeys.Name = "labelKeys";
            this.labelKeys.Size = new System.Drawing.Size(208, 184);
            this.labelKeys.TabIndex = 0;
            this.labelKeys.Text = "Wink";
            // 
            // winHeader1
            // 
            this.winHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.winHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.winHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.winHeader1.Location = new System.Drawing.Point(0, 0);
            this.winHeader1.Name = "winHeader1";
            this.winHeader1.Size = new System.Drawing.Size(406, 22);
            this.winHeader1.TabIndex = 1;
            // 
            // checkedWhiteListBox
            // 
            this.checkedWhiteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedWhiteListBox.FormattingEnabled = true;
            this.checkedWhiteListBox.Location = new System.Drawing.Point(274, 41);
            this.checkedWhiteListBox.Name = "checkedWhiteListBox";
            this.checkedWhiteListBox.Size = new System.Drawing.Size(120, 184);
            this.checkedWhiteListBox.TabIndex = 2;
            this.checkedWhiteListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedWhiteListBox_ItemCheck);
            // 
            // WinKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(406, 234);
            this.Controls.Add(this.labelKeys);
            this.Controls.Add(this.checkedWhiteListBox);
            this.Controls.Add(this.winHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WinKeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WinKeys";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinKeys_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.WinKeys_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelKeys;
        private GrayLib.WinHeader winHeader1;
        private System.Windows.Forms.CheckedListBox checkedWhiteListBox;
    }
}

