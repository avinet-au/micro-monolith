using System.Threading.Tasks;

namespace MicroMonolith.Settings
{
    public interface ISettingService
    {
        /// <summary>
        /// Load a boolean setting
        /// </summary>
        /// <param name="settingName">The name of the setting to load.</param>
        /// <param name="fallback">
        /// The value to use as a default if setting has not been set, 
        /// if not provided an empty setting will result in an exception.</param>
        /// <returns>The saved setting, or the default if not present.</returns>
        Task<bool> GetSettingBool(string settingName, bool? fallback = null);

        /// <summary>
        /// Set a boolean setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to save.</param>
        /// <param name="settingValue">New value for the setting.</param>
        Task SetSettingBool(string settingName, bool settingValue);
    }
}
