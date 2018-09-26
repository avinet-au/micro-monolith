using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MicroMonolith.Common;
using Newtonsoft.Json;

namespace MicroMonolith.Personnel
{
    public class PersonnelSDK : IPersonnelService
    {
        private readonly ISDKConfigurationService sdkConfigurationService;
        private readonly HttpClient client;

        public PersonnelSDK(ISDKConfigurationService sdkConfigurationService)
        {
            this.sdkConfigurationService = sdkConfigurationService;
            client = new HttpClient();
        }

        async Task<IEnumerable<Personnel>> IPersonnelService.GetPersonnel(PersonnelRequest personnelRequest)
        {
            var baseUri = sdkConfigurationService.GetBaseUri(MMService.Personnel);
            var request = String.Format("{0}/api/personnel", baseUri);
            var response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Personnel>>(content);
        }
    }
}
