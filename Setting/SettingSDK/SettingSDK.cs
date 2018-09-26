using System;
using System.Net.Http;
using System.Threading.Tasks;
using MicroMonolith.Common;

namespace MicroMonolith.Setting
{
    public class SettingSDK : ISettingService
    {

        private readonly ISDKConfigurationService sdkConfigurationService;
        private readonly HttpClient client;

        public SettingSDK(ISDKConfigurationService sdkConfigurationService)
        {
            this.sdkConfigurationService = sdkConfigurationService;
            client = new HttpClient();
        }

        async Task<bool> ISettingService.GetSettingBool(string settingName, bool? fallback)
        {
            var baseUri = sdkConfigurationService.GetBaseUri(MMService.Setting);
            var request = String.Format("{0}/api/setting/{1}{2}", baseUri, settingName, fallback.HasValue ? "?fallback=" + fallback : "");
            var response = await client.GetAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            bool result;
            if(bool.TryParse(content, out result))
            {
                return result;
            } else
            {
                throw new Exception(content);
            }
        }

        async Task ISettingService.SetSettingBool(string settingName, bool settingValue)
        {
            var baseUri = sdkConfigurationService.GetBaseUri(MMService.Setting);
            var request = String.Format("{0}/{1}", baseUri, settingName);
            var content = new StringContent(settingValue.ToString());
            var response = await client.PutAsync(request, content);

            response.EnsureSuccessStatusCode();
        }
    }
}
