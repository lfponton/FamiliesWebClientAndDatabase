using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data
{
    public interface IFamiliesService
    {
        IList<Family> Families { get; }
        Task<IList<Family>> GetFamiliesAsync();
        Task AddFamilyAsync(Family family);
        Task RemoveFamilyAsync(int? id);
        Task UpdateFamilyAsync(Family family);
        Task<Family> GetFamilyByIdAsync(int? familyId);
    }
}