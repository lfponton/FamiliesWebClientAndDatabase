using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IPetsService
    {
        IList<Pet> Pets { get; }
        IList<Pet> GetFamilyPets();
        void AddPet(int? familyId, Pet pet);
        void RemovePet(Pet pet);
        void UpdatePet(Pet pet);
        Pet GetPet(int id);

    }
}