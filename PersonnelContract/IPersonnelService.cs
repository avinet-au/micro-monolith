using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroMonolith.Personnel
{
    public interface IPersonnelService
    {
        Task<IEnumerable<Personnel>> GetPersonnel(PersonnelRequest request);
    }
}
