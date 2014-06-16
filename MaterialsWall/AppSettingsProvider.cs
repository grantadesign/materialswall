using System.Configuration;

namespace Granta.MaterialsWall
{
    public interface IAppSettingsProvider
    {
        string GetSetting(string name);
        int GetIntegerSetting(string name, int defaultValue);
    }

    public sealed class AppSettingsProvider : IAppSettingsProvider
    {
        public string GetSetting(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : ConfigurationManager.AppSettings[name];
        }

        public int GetIntegerSetting(string name, int defaltValue)
        {
            string stringValue = GetSetting(name);

            if (stringValue == null)
            {
                return defaltValue;
            }

            int value;
            return int.TryParse(stringValue, out value) ? value : defaltValue;
        }
    }
}
