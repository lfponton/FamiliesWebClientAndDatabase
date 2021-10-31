using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IAdultsService
    {
        IList<Adult> Adults { get; }
        IList<Adult> GetAdults();
        void AddAdult(int? familyId, Adult adult);
        void RemoveAdult(Adult adult);
        void UpdateAdult(Adult adult);
        Adult GetAdult(int id);
    }
}