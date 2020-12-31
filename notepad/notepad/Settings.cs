using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        bool formattingDefault = false;

        string[] settingsRaw;

        string settingsPath;
        string settingsFolderPath;

        string defaultSettings = "false";

        private void Settings_Load(object sender, EventArgs e)
        {
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BetterNotepadING\\settings.txt";
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

            formattingDefault = bool.Parse(settingsRaw[0]);
            formattingEnabledByDefault.Checked = formattingDefault;
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
            FileStream fParameter = new FileStream(settingsPath, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);

            m_WriterParameter.Write(formattingEnabledByDefault.Checked);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
