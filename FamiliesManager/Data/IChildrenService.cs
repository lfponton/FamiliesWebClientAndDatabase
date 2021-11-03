using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data
{
    public interface IChildrenService
    {
        IList<Child> Children { get; }
        Task<IList<Child>> GetChildrenAsync(int? familyId);
        Task AddChildAsync(int familyId, Child child);
        Task RemoveChildAsync(int familyId, int? id);
        Task UpdateChildAsync(int? familyId, Child child);
        Task<Child> GetChildAsync(int familyId, int? id);
    }
}