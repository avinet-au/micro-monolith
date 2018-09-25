using MicroMonolith.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroMonolith.Setting
{
    public class SettingSDK : ISettingService
    {

        private readonly string baseUri;
        private readonly HttpClient client;

        public SettingSDK(string baseUri)
        {
            this.baseUri = baseUri;
            this.client = new HttpClient();
        }

        async Task<bool> ISettingService.GetSettingBool(string settingName, bool? fallback)
        {
            var request = String.Format("{0}/{1}{2}", baseUri, settingName, fallback.HasValue ? "?fallback=" + fallback : "");
            var response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return bool.Parse(content);
        }

        async Task ISettingService.SetSettingBool(string settingName, bool settingValue)
        {
            var request = String.Format("{0}/{1}");
            var content = new StringContent(settingValue.ToString());
            var response = await client.PutAsync(request, content);

            response.EnsureSuccessStatusCode();
        }
    }
}
