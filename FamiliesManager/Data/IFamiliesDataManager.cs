using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IFamiliesDataManager
    {
        IList<Family> GetFamilies();
        void AddFamily(Family family);
        void RemoveFamily(Family family);
        void UpdateFamily(Family family);
        Family getFamily(int id);
    }
}