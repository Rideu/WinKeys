namespace WinKeys
{
    partial class WinKeys
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            this.labelKeys = new System.Windows.Forms.Label();
            this.checkedWhiteListBox = new System.Windows.Forms.CheckedListBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMouse = new System.Windows.Forms.Label();
            this.mousePad = new WinKeysС.MousePad();
            this.SuspendLayout();
            // 
            // labelKeys
            // 
            this.labelKeys.BackColor = System.Drawing.Color.Black;
            this.labelKeys.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelKeys.ForeColor = System.Drawing.Color.White;
            this.labelKeys.Location = new System.Drawing.Point(9, 5);
            this.labelKeys.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelKeys.Name = "labelKeys";
            this.labelKeys.Padding = new System.Windows.Forms.Padding(5);
            this.labelKeys.Size = new System.Drawing.Size(318, 56);
            this.labelKeys.TabIndex = 1;
            this.labelKeys.Text = "Wink";
            // 
            // checkedWhiteListBox
            // 
            this.checkedWhiteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedWhiteListBox.FormattingEnabled = true;
            this.checkedWhiteListBox.Location = new System.Drawing.Point(554, 44);
            this.checkedWhiteListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkedWhiteListBox.Name = "checkedWhiteListBox";
            this.checkedWhiteListBox.Size = new System.Drawing.Size(220, 238);
            this.checkedWhiteListBox.TabIndex = 4;
            this.checkedWhiteListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedWhiteListBox_ItemCheck);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackColor = System.Drawing.Color.White;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRefresh.ForeColor = System.Drawing.Color.Black;
            this.buttonRefresh.Location = new System.Drawing.Point(748, 7);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(26, 30);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "⭮";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Window Whitelist";
            // 
            // labelMouse
            // 
            this.labelMouse.BackColor = System.Drawing.Color.Black;
            this.labelMouse.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMouse.ForeColor = System.Drawing.Color.White;
            this.labelMouse.Location = new System.Drawing.Point(9, 61);
            this.labelMouse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMouse.Name = "labelMouse";
            this.labelMouse.Padding = new System.Windows.Forms.Padding(5);
            this.labelMouse.Size = new System.Drawing.Size(318, 209);
            this.labelMouse.TabIndex = 7;
            this.labelMouse.Text = "Wink";
            // 
            // mousePad
            // 
            this.mousePad.BackColor = System.Drawing.Color.Black;
            this.mousePad.Location = new System.Drawing.Point(9, 120);
            this.mousePad.Name = "mousePad";
            this.mousePad.Size = new System.Drawing.Size(150, 150);
            this.mousePad.TabIndex = 8;
            // 
            // WinKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(783, 294);
            this.Controls.Add(this.mousePad);
            this.Controls.Add(this.labelMouse);
            this.Controls.Add(this.labelKeys);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.checkedWhiteListBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WinKeys";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WinKeys";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinKeys_FormClosing);
            this.Load += new System.EventHandler(this.WinKeys_Load);
            this.SizeChanged += new System.EventHandler(this.WinKeys_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private WinHeader winHeader1;
        private System.Windows.Forms.Label labelKeys;
        private System.Windows.Forms.CheckedListBox checkedWhiteListBox;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMouse;
        private WinKeysС.MousePad mousePad;
    }
}

