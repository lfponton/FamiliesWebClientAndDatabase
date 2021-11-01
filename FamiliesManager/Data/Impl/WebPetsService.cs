using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class WebPetsService : IPetsService
    {
        public IList<Pet> Pets { get; private set; }
        private readonly IFamiliesService familiesService;

        public WebPetsService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetFamilyPets();
        }
        
        public IList<Pet> GetFamilyPets()
        {
            Pets = new List<Pet>();
            foreach (var family in familiesService.GetFamilies())
            {
                foreach (var pet in family.Pets)
                {
                    Pets.Add(pet);
                }
            }
            return Pets;
        }

        public void AddPet(int? familyId, Pet pet)
        {
            int max = Pets.Max(p => p.Id);
            pet.Id = (++max);
            Pets.Add(pet);
            Family family = familiesService.GetFamilyById(familyId);
            family.Pets.Add(pet);
            familiesService.UpdateFamily(family);
        }

        public void RemovePet(Pet pet)
        {
            Pet toRemove = Pets.First(p => p.Id == pet.Id);
            Pets.Remove(toRemove);
            familiesService.UpdateFamily(FindFamily(pet));
        }

        public void UpdatePet(Pet pet)
        {
            Pet toUpdate = Pets.First(p => p.Id == pet.Id);
            toUpdate.Species = pet.Species;
            toUpdate.Name = pet.Name;
            toUpdate.Age = pet.Age;
            familiesService.UpdateFamily(FindFamily(pet));
        }

        public Pet GetPet(int? id)
        {
            return Pets.FirstOrDefault(a => a.Id == id);
        }

        private Family FindFamily(Pet pet)
        {
            Family familyToUpdate = null;
            foreach (var family in familiesService.GetFamilies())
            {
                if (family.Adults.Any(p => p.Id == pet.Id))
                {
                    familyToUpdate = family;
                    break;
                }
            }
            return familyToUpdate;
        }
    }
}