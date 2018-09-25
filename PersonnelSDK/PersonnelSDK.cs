using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicroMonolith.Personnel
{
    public class PersonnelSDK : IPersonnelService
    {
        private readonly string baseUri;
        private readonly HttpClient client;

        public PersonnelSDK(string baseUri)
        {
            this.baseUri = baseUri;
            this.client = new HttpClient();
        }

        async Task<IEnumerable<Personnel>> IPersonnelService.GetPersonnel(PersonnelRequest personnelRequest)
        {
            var request = String.Format("{0}", baseUri);
            var response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Personnel>>(content);
        }
    }
}
