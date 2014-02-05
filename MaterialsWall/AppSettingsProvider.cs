using System.Configuration;

namespace Granta.MaterialsWall
{
    public interface IAppSettingsProvider
    {
        string GetSetting(string name);
    }

    public sealed class AppSettingsProvider : IAppSettingsProvider
    {
        public string GetSetting(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : ConfigurationManager.AppSettings[name];
        }
    }
}
