using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroMonolith.Personnel
{
    [Route("api/[controller]")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            this.personnelService = personnelService;
        }

        [HttpGet]
        public async Task<IEnumerable<Personnel>> Get(PersonnelRequest personnelRequest)
        {
            var result = await personnelService.GetPersonnel(personnelRequest);
            return result;
        }
    }
}
