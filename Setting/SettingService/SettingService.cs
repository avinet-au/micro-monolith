using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroMonolith.Settings
{
    public class SettingService : ISettingService
    {
        private Dictionary<string, bool> settings;

        public SettingService() {
            settings = new Dictionary<string, bool>();
        }

        Task<bool> ISettingService.GetSettingBool(string settingName, bool? fallback)
        {
            if (settings.ContainsKey(settingName))
            {
                return Task.FromResult(settings[settingName]);
            }
            else if (fallback.HasValue)
            {
                return Task.FromResult(fallback.Value);
            }
            else
            {
                throw new ArgumentException(String.Format("No setting found with name {0} and no fallback provided", settingName));
            }
        }

        Task ISettingService.SetSettingBool(string settingName, bool settingValue)
        {
            settings[settingName] = settingValue;
            return Task.CompletedTask;
        }
    }
}
