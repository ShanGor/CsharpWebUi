using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebUi
{
    public sealed class ConfigHelper : ConfigurationSection
    {
        public static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}
