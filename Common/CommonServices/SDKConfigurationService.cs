using System;
using System.Collections.Generic;
using System.Text;

namespace MicroMonolith.Common
{
    public class SDKConfigurationService : ISDKConfigurationService
    {
        private readonly Dictionary<MMService, string> baseUris;

        public SDKConfigurationService(Dictionary<MMService, string> baseUris)
        {
            this.baseUris = baseUris;
        }

        string ISDKConfigurationService.GetBaseUri(MMService service)
        {
            return baseUris[service];
        }
    }
}
