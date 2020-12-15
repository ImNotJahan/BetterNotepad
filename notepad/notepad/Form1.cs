using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace notepad
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        private bool loaded = false;
        private bool hasUnsavedProgress = false;
        private bool fileLoaded = false;

        private string originalText = "";
        private string fileName = "Untitled.txt";

        bool[] formattingThings = { false, false, false }; //0 is bold 1 is italic and 2 is underlined
        bool formattingEnabled = false;

        public Form1()
        {
            InitializeComponent();

            newMenuItem.Click += new EventHandler(NewFile);
            newWindowMenuItem.Click += new EventHandler(OpenNewWindow);
            OpenMenuItem.Click += new EventHandler(OpenFile);
            saveMenuItem.Click += new EventHandler(SaveFileLow);
            exitMenuItem.Click += new EventHandler(SaveFileE);
            saveAsMenuItem.Click += new EventHandler(PrintFile);
            exitMenuItem.Click += new EventHandler(Exit);

            undoMenuItem.Click += new EventHandler(Undo);
            redoMenuItem.Click += new EventHandler(Redo);
            cutMenuItem.Click += new EventHandler(Cut);
            copyMenuItem.Click += new EventHandler(Copy);
            pasteMenuItem.Click += new EventHandler(Paste);
            deleteMenuItem.Click += new EventHandler(Delete);
            searchMenuItem.Click += new EventHandler(Search);
            findMenuItem.Click += new EventHandler(Find);
            replaceMenuItem.Click += new EventHandler(Replace);
            selectAllMenuItem.Click += new EventHandler(SelectAll);
            timeAndDateMenuItem.Click += new EventHandler(TimeDate);

            wordWrapMenuItem.Click += new EventHandler(WordWrap);
            formattingMenuItem.Click += new EventHandler(Formatting);
            boldMenuItem.Click += new EventHandler(Bold);
            italicMenuItem.Click += new EventHandler(Italic);
            underlineMenuItem.Click += new EventHandler(Underline);

            aboutMenuItem.Click += new EventHandler(AboutBoxThing);
            sendFeedbackMenuItem.Click += new EventHandler(SendFeeback);

            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
        }

        private void SendFeeback(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"https://github.com/ImNotJahan/BetterNotepad/issues");
        }

        private void Formatting(object sender, EventArgs e)
        {
            formattingEnabled = !formattingEnabled;
            formattingMenuItem.Checked = formattingEnabled;

            FormattingChange();
        }

        private void Redo(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void TimeDate(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, DateTime.Now.ToString("hh:mm tt MM/dd/yyyy"));
        }

        private void SelectAll(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void Replace(object sender, EventArgs e)
        {
            ReplaceBox replaceBox = new ReplaceBox();
            replaceBox.Show(this);
        }

        private void Find(object sender, EventArgs e)
        {
            findBox findBox0 = new findBox();
            findBox0.Show(this);
        }

        private void AboutBoxThing(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog(this);
        }

        private void Search(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"http://google.com/search?q={RemoveSpecialCharacters(richTextBox1.SelectedText).Replace(" ", "+")}");
        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void Delete(object sender, EventArgs e)
        {
            if(richTextBox1.SelectedText == string.Empty)
            {
                int index = richTextBox1.SelectionStart;

                if(index < richTextBox1.Text.Length)
                {
                    richTextBox1.Text = richTextBox1.Text.Remove(index, 1);
                    richTextBox1.SelectionStart = index;
                }
            }
            else
            {
                richTextBox1.SelectedText = string.Empty;
            }
        }

        private void Paste(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Clipboard.GetText();
        }

        private void Copy(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void Cut(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
            richTextBox1.SelectedText = string.Empty;
        }

        private void Undo(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void Bold(object sender, EventArgs e)
        {
            formattingThings[0] = !formattingThings[0];
            FormattingChange();
        }

        private void Italic(object sender, EventArgs e)
        {
            formattingThings[1] = !formattingThings[1];
            FormattingChange();
        }

        private void Underline(object sender, EventArgs e)
        {
            formattingThings[2] = !formattingThings[2];
            FormattingChange();
        }

        bool[] noFormatting = { false, false, false };
        bool[] bFormatting = { true, false, false };
        bool[] iFormatting = { false, true, false };
        bool[] uFormatting = { false, false, true };
        bool[] bIFormatting = { true, true, false };
        bool[] iUFormatting = { false, true, true };
        bool[] bUFormatting = { true, false, true };

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

        private void FormattingChange()
        {
            if (formattingThings.SequenceEqual(noFormatting) || !formattingEnabled)
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Regular);
            } else if(formattingThings.SequenceEqual(bFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Bold);
            } else if(formattingThings.SequenceEqual(iFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Italic);
            } else if(formattingThings.SequenceEqual(uFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Underline);
            } else if(formattingThings.SequenceEqual(bIFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Italic);
            } else if(formattingThings.SequenceEqual(iUFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Italic | FontStyle.Underline);
            } else if(formattingThings.SequenceEqual(bUFormatting))
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Underline);
            } else
            {
                richTextBox1.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            }
        }

        private void WordWrap(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = wordWrapMenuItem.Checked;
        }

        private void PrintFile(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;

            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            if (hasUnsavedProgress)
            {
                if (MessageBox.Show($"Do you want to save changes to {fileName}", "Notepad", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            hasUnsavedProgress = false;

            Application.Exit();
        }

        private void SaveFileLow(object sender, EventArgs e)
        {
            if (fileLoaded && File.Exists(path))
            {
                FileStream fParameter = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);

                m_WriterParameter.Write(richTextBox1.Text);
                m_WriterParameter.Flush();
                m_WriterParameter.Close();

                ActiveForm.Text = $"{fileName} - Notepad";
                hasUnsavedProgress = false;
            }
            else
            {
                SaveFile();
            }
        }

        string path;
        private void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open";

            openFileDialog1.ShowDialog();

            path = openFileDialog1.FileName;
            string text = File.ReadAllText(path);

            richTextBox1.Text = text;
            originalText = text;
            fileLoaded = true;
            fileName = path.Split('\\')[path.Split('\\').Length - 1];
            hasUnsavedProgress = false;

            ActiveForm.Text = $"{fileName} - Notepad";
        }

        private void WinResizeEnd(object sender, EventArgs e)
        {
            if(loaded)
                richTextBox1.Size = new Size(ActiveForm.Size.Width - 16, ActiveForm.Size.Height - 98);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loaded = true;
            instance = this;
        }

        private void NewFile(object sender, EventArgs e)
        {
            if (hasUnsavedProgress)
            {
                if(MessageBox.Show($"Do you want to save changes to {fileName}", "Notepad", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
            richTextBox1.Clear();

            hasUnsavedProgress = false;
        }

        private void OpenNewWindow(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        private void SaveFileE(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            saveFileDialog1.Title = "Save As";
            saveFileDialog1.FileName = $"{fileName}";

            saveFileDialog1.ShowDialog();

            FileStream fParameter = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);

            m_WriterParameter.Write(richTextBox1.Text);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();

            ActiveForm.Text = $"{fileName} - Notepad";
            hasUnsavedProgress = false;

            fileLoaded = true;
            path = saveFileDialog1.FileName;
        }

        private void TextChange(object sender, EventArgs e)
        {
            if(richTextBox1.Text != originalText)
            {
                ActiveForm.Text = $"*{fileName} - Notepad";
                hasUnsavedProgress = true;
            }
            else
            {
                ActiveForm.Text = $"{fileName} - Notepad";
                hasUnsavedProgress = false;
            }
        }

        private void WinClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedProgress)
            {
                if (MessageBox.Show($"Do you want to save changes to {fileName}", "Notepad", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Consolas", 11), Brushes.Black, 120, 150);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    public class CustomColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(60, 60, 60); }
        }

        public override Color MenuItemSelected
        {
            get { return Color.DarkSlateGray; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(40, 40, 40); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(40, 40, 40); }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(40, 40, 40); }
        }

        public override Color SeparatorDark
        {
            get { return Color.FromArgb(40, 40, 40); }
        }

        public override Color CheckBackground
        {
            get { return Color.FromArgb(100, 100, 100); }
        }
    }
}