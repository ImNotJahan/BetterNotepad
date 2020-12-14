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

        private MainMenu mainMenu;

        bool[] formattingThings = { false, false, false }; //0 is bold 1 is italic and 2 is underlined
        bool formattingEnabled = false;

        MenuItem wordWrapMenuItem;
        MenuItem formattingMenuItem;
        MenuItem boldMenuItem;
        MenuItem italicMenuItem;
        MenuItem underlineMenuItem;
        public Form1()
        {
            InitializeComponent();

            mainMenu = new MainMenu();

            MenuItem File = mainMenu.MenuItems.Add("&File");

            MenuItem newMenuItem = new MenuItem("&New");
            newMenuItem.Click += new EventHandler(NewFile);
            newMenuItem.Shortcut = Shortcut.CtrlN;

            MenuItem newWindowMenuItem = new MenuItem("&New Window");
            newWindowMenuItem.Click += new EventHandler(OpenNewWindow);
            newWindowMenuItem.Shortcut = Shortcut.CtrlShiftN;

            MenuItem OpenMenuItem = new MenuItem("&Open...");
            OpenMenuItem.Click += new EventHandler(OpenFile);
            OpenMenuItem.Shortcut = Shortcut.CtrlO;

            MenuItem saveMenuItem = new MenuItem("&Save");
            saveMenuItem.Click += new EventHandler(SaveFileLow);
            saveMenuItem.Shortcut = Shortcut.CtrlS;

            MenuItem saveAsMenuItem = new MenuItem("&Save As...");
            saveAsMenuItem.Click += new EventHandler(SaveFileE);
            saveAsMenuItem.Shortcut = Shortcut.CtrlShiftS;

            MenuItem printMenuItem = new MenuItem("&Print");
            printMenuItem.Click += new EventHandler(PrintFile);
            printMenuItem.Shortcut = Shortcut.CtrlP;

            MenuItem exitMenuItem = new MenuItem("&Exit");
            exitMenuItem.Click += new EventHandler(Exit);
            exitMenuItem.Shortcut = Shortcut.AltF4;

            File.MenuItems.Add(newMenuItem);
            File.MenuItems.Add(newWindowMenuItem);
            File.MenuItems.Add(OpenMenuItem);
            File.MenuItems.Add(saveMenuItem);
            File.MenuItems.Add(saveAsMenuItem);
            File.MenuItems.Add("-");

            File.MenuItems.Add(printMenuItem);
            File.MenuItems.Add("-");

            File.MenuItems.Add(exitMenuItem);

            MenuItem Edit = mainMenu.MenuItems.Add("&Edit");

            MenuItem undoMenuItem = new MenuItem("&Undo");
            undoMenuItem.Click += new EventHandler(Undo);
            undoMenuItem.Shortcut = Shortcut.CtrlZ;

            MenuItem redoMenuItem = new MenuItem("&Redo");
            redoMenuItem.Click += new EventHandler(Redo);
            redoMenuItem.Shortcut = Shortcut.CtrlShiftZ;

            MenuItem cutMenuItem = new MenuItem("&Cut");
            cutMenuItem.Click += new EventHandler(Cut);
            cutMenuItem.Shortcut = Shortcut.CtrlX;
       
            MenuItem copyMenuItem = new MenuItem("&Copy");
            copyMenuItem.Click += new EventHandler(Copy);
            copyMenuItem.Shortcut = Shortcut.CtrlC;

            MenuItem pasteMenuItem = new MenuItem("&Paste");
            pasteMenuItem.Click += new EventHandler(Paste);
            pasteMenuItem.Shortcut = Shortcut.CtrlV;

            MenuItem deleteMenuItem = new MenuItem("&Delete");
            deleteMenuItem.Click += new EventHandler(Delete);
            deleteMenuItem.Shortcut = Shortcut.Del;

            MenuItem searchMenuItem = new MenuItem("&Search with Google...");
            searchMenuItem.Click += new EventHandler(Search);
            searchMenuItem.Shortcut = Shortcut.CtrlE;

            MenuItem findMenuItem = new MenuItem("&Find...");
            findMenuItem.Click += new EventHandler(Find);
            findMenuItem.Shortcut = Shortcut.CtrlF;

            MenuItem replaceMenuItem = new MenuItem("&Replace...");
            replaceMenuItem.Click += new EventHandler(Replace);
            replaceMenuItem.Shortcut = Shortcut.CtrlH;

            MenuItem selectAllMenuItem = new MenuItem("&Select All");
            selectAllMenuItem.Click += new EventHandler(SelectAll);
            selectAllMenuItem.Shortcut = Shortcut.CtrlA;

            MenuItem timeAndDateMenuItem = new MenuItem("&Time/Date");
            timeAndDateMenuItem.Click += new EventHandler(TimeDate);
            timeAndDateMenuItem.Shortcut = Shortcut.F5;

            Edit.MenuItems.Add(undoMenuItem);
            Edit.MenuItems.Add(redoMenuItem);
            Edit.MenuItems.Add("-");

            Edit.MenuItems.Add(cutMenuItem);
            Edit.MenuItems.Add(copyMenuItem);
            Edit.MenuItems.Add(pasteMenuItem);
            Edit.MenuItems.Add(deleteMenuItem);
            Edit.MenuItems.Add("-");

            Edit.MenuItems.Add(searchMenuItem);
            Edit.MenuItems.Add(findMenuItem);
            Edit.MenuItems.Add(replaceMenuItem);
            Edit.MenuItems.Add("-");

            Edit.MenuItems.Add(selectAllMenuItem);
            Edit.MenuItems.Add(timeAndDateMenuItem);

            MenuItem Format = mainMenu.MenuItems.Add("&Format");

            wordWrapMenuItem = new MenuItem("&Word Wrap");
            wordWrapMenuItem.Click += new EventHandler(WordWrap);
            wordWrapMenuItem.Checked = true;

            formattingMenuItem = new MenuItem("&Formatting?");
            formattingMenuItem.Click += new EventHandler(Formatting);
            formattingMenuItem.Shortcut = Shortcut.F3;

            boldMenuItem = new MenuItem("&Bold");
            boldMenuItem.Click += new EventHandler(Bold);
            boldMenuItem.Shortcut = Shortcut.CtrlB;

            italicMenuItem = new MenuItem("&Italic");
            italicMenuItem.Click += new EventHandler(Italic);
            italicMenuItem.Shortcut = Shortcut.CtrlI;

            underlineMenuItem = new MenuItem("&Underline");
            underlineMenuItem.Click += new EventHandler(Underline);
            underlineMenuItem.Shortcut = Shortcut.CtrlU;

            Format.MenuItems.Add(wordWrapMenuItem);
            Format.MenuItems.Add("-");
            Format.MenuItems.Add(formattingMenuItem);
            Format.MenuItems.Add("-");
            Format.MenuItems.Add(boldMenuItem);
            Format.MenuItems.Add(italicMenuItem);
            Format.MenuItems.Add(underlineMenuItem);

            MenuItem View = mainMenu.MenuItems.Add("&View");

            MenuItem About = mainMenu.MenuItems.Add("&Help");

            MenuItem aboutMenuItem = new MenuItem("&About Notepad");
            aboutMenuItem.Click += new EventHandler(AboutBoxThing);

            MenuItem sendFeedbackMenuItem = new MenuItem("&Send Feedback");
            sendFeedbackMenuItem.Click += new EventHandler(SendFeeback);

            About.MenuItems.Add(sendFeedbackMenuItem);
            About.MenuItems.Add("-");
            About.MenuItems.Add(aboutMenuItem);

            Menu = mainMenu;
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

            boldMenuItem.Checked = !boldMenuItem.Checked;
        }

        private void Italic(object sender, EventArgs e)
        {
            formattingThings[1] = !formattingThings[1];
            FormattingChange();

            italicMenuItem.Checked = !italicMenuItem.Checked;
        }

        private void Underline(object sender, EventArgs e)
        {
            formattingThings[2] = !formattingThings[2];
            FormattingChange();

            underlineMenuItem.Checked = !underlineMenuItem.Checked;
        }

        bool[] noFormatting = { false, false, false };
        bool[] bFormatting = { true, false, false };
        bool[] iFormatting = { false, true, false };
        bool[] uFormatting = { false, false, true };
        bool[] bIFormatting = { true, true, false };
        bool[] iUFormatting = { false, true, true };
        bool[] bUFormatting = { true, false, true };

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
            wordWrapMenuItem.Checked = !wordWrapMenuItem.Checked;
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
                richTextBox1.Size = new Size(ActiveForm.Size.Width - 16, ActiveForm.Size.Height - 70);
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
}