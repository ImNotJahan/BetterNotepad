using System;
using System.Drawing;
using System.Windows.Forms;

namespace notepad
{
    public partial class findBox : Form
    {
        RichTextBox textBox;

        int properSelectionStart = 0;
        int properSelectionLength = 0;

        public findBox()
        {
            InitializeComponent();

            textBox = Form1.instance.richTextBox1;

            textBox1.Text = textBox.SelectedText;

            properSelectionStart = textBox.SelectionStart;
            properSelectionLength = textBox.SelectionLength;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            textBox.SelectionBackColor = Color.FromArgb(40, 40, 40);

            textBox.SelectionStart = properSelectionStart + properSelectionLength;

            int findThing;
            if (checkBox1.Checked && checkBox3.Checked)
            {
                findThing = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
            } else if (checkBox1.Checked)
            {
                findThing = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.MatchCase);
            }
            else if (checkBox3.Checked)
            {
                findThing = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.WholeWord);
            }
            else
            {
                findThing = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
            }

            if(findThing != -1)
            {
                textBox.SelectionStart = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
                textBox.SelectionLength = textBox1.Text.Length;
                textBox.SelectionBackColor = Color.FromArgb(50, 143, 254);

                properSelectionStart = textBox.SelectionStart;
                properSelectionLength = textBox.SelectionLength;
            }
            else
            {
                if (checkBox2.Checked)
                {
                    textBox.SelectionStart = 0;
                }
                else
                {
                    MessageBox.Show($"Cannot find {textBox1.Text}", "Notepad");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void findBox_Deactivate(object sender, EventArgs e)
        {
            textBox.SelectionStart = properSelectionStart;
            textBox.SelectionLength = properSelectionLength;
        }
    }
}
