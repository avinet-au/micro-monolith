using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroMonolith.Personnel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonnelService personnelService;

        public IndexModel(IPersonnelService personnelService) {
            this.personnelService = personnelService;
        }

        public IEnumerable<Personnel> Personnel { get; private set; }

        public async Task OnGetAsync()
        {
            Personnel = await personnelService.GetPersonnel(new PersonnelRequest());
        }
    }
}
