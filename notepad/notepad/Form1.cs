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

            settingsMenuItem.Click += new EventHandler(SettingsMenu);

            aboutMenuItem.Click += new EventHandler(AboutBoxThing);
            sendFeedbackMenuItem.Click += new EventHandler(SendFeeback);

            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());

            CheckSettings(true);
        }

        private void SettingsMenu(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();

            settings.FormClosed += new FormClosedEventHandler(CheckSettingsE);
        }

        private void CheckSettings(bool startupCall)
        {
            string[] settingsRaw;
            string settingsFolderPath;
            string defaultSettings = "false";
            string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BetterNotepadING\\settings.txt";
            if (File.Exists(settingsPath))
            {
                settingsRaw = File.ReadAllLines(settingsPath);
            }
            else
            {
                settingsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BetterNotepadING";
                Directory.CreateDirectory(settingsFolderPath);

                FileStream fParameter = new FileStream(settingsPath, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);

                m_WriterParameter.Write(defaultSettings);
                m_WriterParameter.Flush();
                m_WriterParameter.Close();

                settingsRaw = defaultSettings.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }

            if (startupCall)
            {
                formattingMenuItem.Checked = bool.Parse(settingsRaw[0]);
                formattingEnabled = bool.Parse(settingsRaw[0]);
            }
        }

        private void CheckSettingsE(object sender, EventArgs e)
        {
            CheckSettings(false);
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
            textBox.Redo();
        }

        private void TimeDate(object sender, EventArgs e)
        {
            textBox.Text = textBox.Text.Insert(textBox.SelectionStart, DateTime.Now.ToString("hh:mm tt MM/dd/yyyy"));
        }

        private void SelectAll(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        ReplaceBox replaceBox;
        private void Replace(object sender, EventArgs e)
        {
            replaceBox = new ReplaceBox();
            replaceBox.Show(this);
        }

        FindBox findBox;
        private void Find(object sender, EventArgs e)
        {
            findBox = new FindBox();
            findBox.Show(this);
        }

        AboutBox1 about;
        private void AboutBoxThing(object sender, EventArgs e)
        {
            about = new AboutBox1();
            about.ShowDialog(this);
        }

        private void Search(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"http://google.com/search?q={RemoveSpecialCharacters(textBox.SelectedText).Replace(" ", "+")}");
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
            if(textBox.SelectedText == string.Empty)
            {
                int index = textBox.SelectionStart;

                if(index < textBox.Text.Length)
                {
                    textBox.Text = textBox.Text.Remove(index, 1);
                    textBox.SelectionStart = index;
                }
            }
            else
            {
                textBox.SelectedText = string.Empty;
            }
        }

        private void Paste(object sender, EventArgs e)
        {
            int prevSelectionStart = textBox.SelectionStart;
            textBox.SelectedText = Clipboard.GetText();

            if (!formattingEnabled)
            {
                textBox.SelectionStart = prevSelectionStart;
                textBox.SelectionLength = Clipboard.GetText().Length;
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Regular);
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Regular);
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Regular);
                textBox.SelectionStart = textBox.SelectionLength + prevSelectionStart;
            }
        }

        private void Copy(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
        }

        private void Cut(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
            textBox.SelectedText = string.Empty;
        }

        private void Undo(object sender, EventArgs e)
        {
            textBox.Undo();
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
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Regular);
            } else if(formattingThings.SequenceEqual(bFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Bold);
            } else if(formattingThings.SequenceEqual(iFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Italic);
            } else if(formattingThings.SequenceEqual(uFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Underline);
            } else if(formattingThings.SequenceEqual(bIFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Italic);
            } else if(formattingThings.SequenceEqual(iUFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Italic | FontStyle.Underline);
            } else if(formattingThings.SequenceEqual(bUFormatting))
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Underline);
            } else
            {
                textBox.SelectionFont = new Font("Consolas", 11, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            }
        }

        private void WordWrap(object sender, EventArgs e)
        {
            textBox.WordWrap = wordWrapMenuItem.Checked;
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
            Application.Exit();
        }

        private void SaveFileLow(object sender, EventArgs e)
        {
            if (fileLoaded && File.Exists(path))
            {
                FileStream fParameter = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);

                m_WriterParameter.Write(textBox.Text);
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

            textBox.Text = text;
            originalText = text;
            fileLoaded = true;
            fileName = path.Split('\\')[path.Split('\\').Length - 1];
            hasUnsavedProgress = false;

            ActiveForm.Text = $"{fileName} - Notepad";
        }

        private void WinResizeEnd(object sender, EventArgs e)
        {
            if (loaded)
            {
                textBox.Size = new Size(ActiveForm.Size.Width - 16, ActiveForm.Size.Height - 98);
            }
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
                DialogResult saveDialog = MessageBox.Show($"Do you want to save changes to {fileName}", "Notepad", MessageBoxButtons.YesNoCancel);
                if (saveDialog == DialogResult.Yes)
                {
                    SaveFile();
                } else if(saveDialog == DialogResult.No)
                {
                    textBox.Clear();

                    hasUnsavedProgress = false;
                }
            }
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

            DialogResult saveDialog = saveFileDialog1.ShowDialog();

            if (saveDialog == DialogResult.OK)
            {
                FileStream fParameter = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);

                m_WriterParameter.Write(textBox.Text);
                m_WriterParameter.Flush();
                m_WriterParameter.Close();

                fileLoaded = true;
                path = saveFileDialog1.FileName;

                fileName = path.Split('\\')[path.Split('\\').Length - 1];
                ActiveForm.Text = $"{fileName} - Notepad";

                hasUnsavedProgress = false;
            }
        }

        private void TextChange(object sender, EventArgs e)
        {
            if(textBox.Text != originalText)
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
                DialogResult saveDialog = MessageBox.Show($"Do you want to save changes to {fileName}", "Notepad", MessageBoxButtons.YesNoCancel);
                if (saveDialog == DialogResult.Yes)
                {
                    SaveFile();
                } else if(saveDialog == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox.Text, new Font("Consolas", 11), Brushes.Black, 120, 150);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormFocused(object sender, EventArgs e)
        {
            int properSelectionStart = textBox.SelectionStart;
            int properSelectionLength = textBox.SelectionLength;

            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectionBackColor = Color.FromArgb(30, 30, 30);

            textBox.SelectionStart = properSelectionStart;
            textBox.SelectionLength = properSelectionLength;
        }

        private void textBox_SelectionChanged(object sender, EventArgs e)
        {
            if (textBox.SelectionFont.Bold)
            {
                boldMenuItem.Checked = true;
                formattingThings[0] = true;
            }
            else
            {
                boldMenuItem.Checked = false;
                formattingThings[0] = false;
            }

            if (textBox.SelectionFont.Italic)
            {
                italicMenuItem.Checked = true;
                formattingThings[1] = true;
            }
            else
            {
                italicMenuItem.Checked = false;
                formattingThings[1] = false;
            }

            if (textBox.SelectionFont.Underline)
            {
                underlineMenuItem.Checked = true;
                formattingThings[2] = true;
            }
            else
            {
                underlineMenuItem.Checked = false;
                formattingThings[2] = false;
            }
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