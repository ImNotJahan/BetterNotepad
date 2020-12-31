using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace notepad
{
    public partial class FindBox : Form
    {
        RichTextBox textBox;

        int properSelectionStart = 0;
        int properSelectionLength = 0;

        public FindBox()
        {
            InitializeComponent();

            textBox = Form1.instance.textBox;

            findInput.Text = textBox.SelectedText;

            properSelectionStart = textBox.SelectionStart;
            properSelectionLength = textBox.SelectionLength;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            textBox.SelectionBackColor = Color.FromArgb(40, 40, 40);

            textBox.SelectionStart = properSelectionStart + properSelectionLength;

            int findThing;
            if (matchCaseCheckbox.Checked && wholeWordCheckbox.Checked)
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
            } else if (matchCaseCheckbox.Checked)
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.MatchCase);
            }
            else if (wholeWordCheckbox.Checked)
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.WholeWord);
            }
            else
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
            }

            if(findThing != -1)
            {
                textBox.SelectionStart = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
                textBox.SelectionLength = findInput.Text.Length;
                textBox.SelectionBackColor = Color.FromArgb(50, 143, 254);

                properSelectionStart = textBox.SelectionStart;
                properSelectionLength = textBox.SelectionLength;
            }
            else
            {
                if (wrapAroundCheckbox.Checked)
                {
                    textBox.SelectionStart = 0;
                }
                else
                {
                    MessageBox.Show($"Cannot find {findInput.Text}", "Notepad");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findBox_Deactivate(object sender, EventArgs e)
        {
            textBox.SelectionStart = properSelectionStart;
            textBox.SelectionLength = properSelectionLength;
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private void FindInputTextChanged(object sender, EventArgs e)
        {
            if (findInput.Text == string.Empty)
            {
                findNextButton.Enabled = false;
            }
            else
            {
                findNextButton.Enabled = true;
            }
        }
    }
}
