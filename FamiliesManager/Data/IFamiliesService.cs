using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IFamiliesService
    {
        IList<Family> Families { get; }
        IList<Family> GetFamilies();
        void AddFamily(Family family);
        void RemoveFamily(Family family);
        void UpdateFamily(Family family);
        Family GetFamilyById(int? familyId);
    }
}