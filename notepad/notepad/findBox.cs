using System;
using System.Windows.Forms;

namespace notepad
{
    public partial class findBox : Form
    {
        RichTextBox textBox;
        public findBox()
        {
            InitializeComponent();

            textBox = Form1.instance.richTextBox1;

            textBox1.Text = textBox.SelectedText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.SelectionStart = textBox.Find(textBox1.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
            textBox.SelectionLength = textBox1.Text.Length;
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
    }
}
