using System;
using System.Collections.Generic;
using System.Text;

namespace MicroMonolith.Common
{
    public interface ISDKConfigurationService
    {
        string GetBaseUri(MMService service);
    }

    public enum MMService
    {
        Personnel = 1,
        Setting = 2
    }
}
