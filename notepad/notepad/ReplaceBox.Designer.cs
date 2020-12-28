namespace notepad
{
    partial class ReplaceBox
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.findInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.replaceInput = new System.Windows.Forms.TextBox();
            this.wrapAroundCheckbox = new System.Windows.Forms.CheckBox();
            this.matchCaseCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(351, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find next";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(351, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Replace";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(351, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Replace all";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(351, 102);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // findInput
            // 
            this.findInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.findInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.findInput.ForeColor = System.Drawing.SystemColors.Window;
            this.findInput.Location = new System.Drawing.Point(110, 25);
            this.findInput.Name = "findInput";
            this.findInput.Size = new System.Drawing.Size(235, 22);
            this.findInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Find what:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Snow;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Replace with:";
            // 
            // replaceInput
            // 
            this.replaceInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.replaceInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replaceInput.ForeColor = System.Drawing.SystemColors.Window;
            this.replaceInput.Location = new System.Drawing.Point(110, 53);
            this.replaceInput.Name = "replaceInput";
            this.replaceInput.Size = new System.Drawing.Size(235, 22);
            this.replaceInput.TabIndex = 7;
            // 
            // wrapAroundCheckbox
            // 
            this.wrapAroundCheckbox.AutoSize = true;
            this.wrapAroundCheckbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wrapAroundCheckbox.Location = new System.Drawing.Point(12, 151);
            this.wrapAroundCheckbox.Name = "wrapAroundCheckbox";
            this.wrapAroundCheckbox.Size = new System.Drawing.Size(113, 21);
            this.wrapAroundCheckbox.TabIndex = 8;
            this.wrapAroundCheckbox.Text = "Wrap around";
            this.wrapAroundCheckbox.UseVisualStyleBackColor = true;
            // 
            // matchCaseCheckbox
            // 
            this.matchCaseCheckbox.AutoSize = true;
            this.matchCaseCheckbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.matchCaseCheckbox.Location = new System.Drawing.Point(12, 124);
            this.matchCaseCheckbox.Name = "matchCaseCheckbox";
            this.matchCaseCheckbox.Size = new System.Drawing.Size(102, 21);
            this.matchCaseCheckbox.TabIndex = 9;
            this.matchCaseCheckbox.Text = "Match case";
            this.matchCaseCheckbox.UseVisualStyleBackColor = true;
            // 
            // ReplaceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(452, 180);
            this.Controls.Add(this.matchCaseCheckbox);
            this.Controls.Add(this.wrapAroundCheckbox);
            this.Controls.Add(this.replaceInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.findInput);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceBox";
            this.ShowIcon = false;
            this.Text = "Replace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox findInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox replaceInput;
        private System.Windows.Forms.CheckBox wrapAroundCheckbox;
        private System.Windows.Forms.CheckBox matchCaseCheckbox;
    }
}