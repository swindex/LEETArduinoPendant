using System;
using System.Diagnostics;


namespace LEETArduinoPendant
{
    public class Settings
    {
        public bool Imperial { get; set; } = true;

        public Plugininterface.Entry UC { get; set; }

        public event Action SettingsChanged;

        public Settings(Plugininterface.Entry uc)
        {
            UC = uc;
            try
            {
#if !DEBUG
                string v = "";

                v = UC.Readkey("LEETArduinoPendant", "imperial", "0");
                if (v == "0")
                {
                    Imperial = false;
                }
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void Save()
        {
            try
            {
#if !DEBUG
                UC.Writekey("LEETArduinoPendant", "imperial", Imperial ? "1" : "0");
                SettingsChanged?.Invoke();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
