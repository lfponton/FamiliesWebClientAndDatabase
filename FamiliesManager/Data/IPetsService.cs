using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data
{
    public interface IPetsService
    {
        IList<Pet> Pets { get; }
        Task<IList<Pet>> GetFamilyPetsAsync(int? familyId);
        Task AddPetAsync(int? familyId, Pet pet);
        Task RemovePetAsync(int familyId, int? id);
        Task UpdatePetAsync(int? familyId, Pet pet);
        Task<Pet> GetPetAsync(int familyId, int? id);

    }
}