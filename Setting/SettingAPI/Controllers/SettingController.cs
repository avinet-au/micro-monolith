using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroMonolith.Setting;

namespace SettingAPI.Controllers
{
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly ISettingService settingService;

        public SettingController(ISettingService settingService)
        {
            this.settingService = settingService;
        }

        [HttpGet("{settingName}")]
        public async Task<bool> Get(string settingName, bool? fallback = null)
        {
            var result = await settingService.GetSettingBool(settingName, fallback);
            return result;
        }

        [HttpPut("{settingName}")]
        public async Task Put(string settingName, [FromBody]bool value)
        {
            await settingService.SetSettingBool(settingName, value);
        }

    }
}
