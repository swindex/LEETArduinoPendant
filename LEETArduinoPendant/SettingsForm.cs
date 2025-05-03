using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LEETArduinoPendant
{
    public partial class SettingsForm : Form
    {
        private Plugininterface.Entry UC;
        private UCCNCplugin PluginMain;
        private Settings Settings;

        public SettingsForm(UCCNCplugin callerPluginMain)
        {
            UC = callerPluginMain.UC;
            PluginMain = callerPluginMain;
            Settings = PluginMain.Settings;

            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            radio_in.Checked = Settings.Imperial;
            radio_mm.Checked = !Settings.Imperial;
        }
                
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://zero-divide.net",
                UseShellExecute = true
            });
        }


        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Imperial = radio_in.Checked;
            Settings.Save();
        }
    }
}