using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace notepad
{
    public partial class ReplaceBox : Form
    {
        RichTextBox textBox;

        int properSelectionStart = 0;
        int properSelectionLength = 0;

        public ReplaceBox()
        {
            InitializeComponent();

            textBox = Form1.instance.textBox;
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

        private void button1_Click(object sender, EventArgs e)
        {
            SelectNextWord();
        }

        private void SelectNextWord()
        {
            textBox.SelectionBackColor = Color.FromArgb(40, 40, 40);

            textBox.SelectionStart = properSelectionStart + properSelectionLength;

            int findThing;
            if (matchCaseCheckbox.Checked)
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.MatchCase);
            }
            else
            {
                findThing = textBox.Find(findInput.Text, textBox.SelectionStart, textBox.Text.Length, RichTextBoxFinds.None);
            }

            if (findThing != -1)
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
            if(textBox.SelectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength)
                    .Insert(textBox.SelectionStart, replaceInput.Text);
            }

            SelectNextWord();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectNextWord();
            while(textBox.SelectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength)
                    .Insert(textBox.SelectionStart, replaceInput.Text);
                SelectNextWord();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
