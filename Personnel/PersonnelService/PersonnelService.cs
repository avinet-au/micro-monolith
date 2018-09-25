using MicroMonolith.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMonolith.Personnel
{
    public class PersonnelService : IPersonnelService
    {
        private const string LAST_NAME_FIRST = "PersonnelLastNameFirst";

        private readonly List<PersonnelEntity> data = new List<PersonnelEntity>() {
            new PersonnelEntity() { FirstName = "Daniel", LastName = "Emery" },
            new PersonnelEntity() { FirstName = "Martin", LastName = "Adams" },
            new PersonnelEntity() { FirstName = "Mark", LastName = "Healy" },
            new PersonnelEntity() { FirstName = "Matthew", LastName = "Botting" },
        };

        private readonly ISettingService settingService;

        public PersonnelService(ISettingService settingService)
        {
            this.settingService = settingService;
        }

        async Task<IEnumerable<Personnel>> IPersonnelService.GetPersonnel(PersonnelRequest request)
        {
            var lastNameFirst = await settingService.GetSettingBool(LAST_NAME_FIRST, true);
            var query = data.AsQueryable();

            if(request.FirstNameFilter != null)
            {
                query = query.Where(w => w.FirstName.Contains(request.FirstNameFilter));
            }

            if (request.LastNameFilter != null)
            {
                query = query.Where(w => w.LastName.Contains(request.LastNameFilter));
            }

            IEnumerable<Personnel> result = query.Select(s => new Personnel() {
                DisplayText = lastNameFirst ? s.LastName + ", " + s.FirstName : s.FirstName + " " + s.LastName,
                FirstName = s.FirstName,
                LastName = s.LastName
            }).ToList();

            return result;
        }

        private class PersonnelEntity
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
