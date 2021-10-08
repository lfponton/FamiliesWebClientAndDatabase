using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IAdultsDataManager
    {
        IList<Adult> GetAdults();
        void AddAdult(Adult adult);
        void RemoveAdult(Adult adult);
        void UpdateAdult(Adult adult);
        Adult GetAdult(int id);
    }
}