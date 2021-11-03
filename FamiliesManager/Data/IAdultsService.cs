using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data
{
    public interface IAdultsService
    {
        IList<Adult> Adults { get; }
        Task<IList<Adult>> GetAdultsAsync(int? familyId);
        Task AddAdultAsync(int familyId, Adult adult);
        Task RemoveAdultAsync(int familyId, int? id);
        Task UpdateAdultAsync(int? familyId, Adult adult);
        Task<Adult> GetAdultAsync(int familyId, int? id);
    }
}