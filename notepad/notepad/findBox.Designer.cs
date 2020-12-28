namespace notepad
{
    partial class FindBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.findInput = new System.Windows.Forms.TextBox();
            this.findNextButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.matchCaseCheckbox = new System.Windows.Forms.CheckBox();
            this.wrapAroundCheckbox = new System.Windows.Forms.CheckBox();
            this.wholeWordCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what";
            // 
            // findInput
            // 
            this.findInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.findInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.findInput.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.findInput.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.findInput.Location = new System.Drawing.Point(88, 13);
            this.findInput.Name = "findInput";
            this.findInput.Size = new System.Drawing.Size(214, 22);
            this.findInput.TabIndex = 1;
            this.findInput.TextChanged += new System.EventHandler(this.FindInputTextChanged);
            // 
            // findNextButton
            // 
            this.findNextButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.findNextButton.Enabled = false;
            this.findNextButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.findNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findNextButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.findNextButton.Location = new System.Drawing.Point(309, 11);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(82, 31);
            this.findNextButton.TabIndex = 2;
            this.findNextButton.Text = "Find Next";
            this.findNextButton.UseVisualStyleBackColor = false;
            this.findNextButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cancelButton.Location = new System.Drawing.Point(309, 48);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(82, 31);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // matchCaseCheckbox
            // 
            this.matchCaseCheckbox.AutoSize = true;
            this.matchCaseCheckbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.matchCaseCheckbox.Location = new System.Drawing.Point(12, 41);
            this.matchCaseCheckbox.Name = "matchCaseCheckbox";
            this.matchCaseCheckbox.Size = new System.Drawing.Size(104, 21);
            this.matchCaseCheckbox.TabIndex = 5;
            this.matchCaseCheckbox.Text = "Match Case";
            this.matchCaseCheckbox.UseVisualStyleBackColor = true;
            // 
            // wrapAroundCheckbox
            // 
            this.wrapAroundCheckbox.AutoSize = true;
            this.wrapAroundCheckbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wrapAroundCheckbox.Location = new System.Drawing.Point(12, 68);
            this.wrapAroundCheckbox.Name = "wrapAroundCheckbox";
            this.wrapAroundCheckbox.Size = new System.Drawing.Size(114, 21);
            this.wrapAroundCheckbox.TabIndex = 6;
            this.wrapAroundCheckbox.Text = "Wrap Around";
            this.wrapAroundCheckbox.UseVisualStyleBackColor = true;
            // 
            // wholeWordCheckbox
            // 
            this.wholeWordCheckbox.AutoSize = true;
            this.wholeWordCheckbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wholeWordCheckbox.Location = new System.Drawing.Point(12, 95);
            this.wholeWordCheckbox.Name = "wholeWordCheckbox";
            this.wholeWordCheckbox.Size = new System.Drawing.Size(108, 21);
            this.wholeWordCheckbox.TabIndex = 7;
            this.wholeWordCheckbox.Text = "Whole Word";
            this.wholeWordCheckbox.UseVisualStyleBackColor = true;
            // 
            // FindBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(399, 122);
            this.Controls.Add(this.wholeWordCheckbox);
            this.Controls.Add(this.wrapAroundCheckbox);
            this.Controls.Add(this.matchCaseCheckbox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.findInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindBox";
            this.ShowIcon = false;
            this.Text = "findBox";
            this.Deactivate += new System.EventHandler(this.findBox_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox findInput;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox matchCaseCheckbox;
        private System.Windows.Forms.CheckBox wrapAroundCheckbox;
        private System.Windows.Forms.CheckBox wholeWordCheckbox;
    }
}