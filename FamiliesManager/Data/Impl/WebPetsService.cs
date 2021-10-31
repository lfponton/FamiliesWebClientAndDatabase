using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamiliesManager.Data
{
    public class WebPetsService : IPetsService
    {
        private IList<Pet> pets;
        private IFamiliesService familiesService;

        public WebPetsService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetFamilyPets();
        }
        
        public IList<Pet> GetFamilyPets()
        {
            pets = new List<Pet>();
            foreach (var family in familiesService.GetFamilies())
            {
                foreach (var pet in family.Pets)
                {
                    pets.Add(pet);
                }
            }
            return pets;
        }

        public void AddPet(int? familyId, Pet pet)
        {
            int max = pets.Max(p => p.Id);
            pet.Id = (++max);
            pets.Add(pet);
            Family family = familiesService.GetFamilyById(familyId);
            family.Pets.Add(pet);
            familiesService.UpdateFamily(family);
        }

        public void RemovePet(Pet pet)
        {
            Pet toRemove = pets.First(p => p.Id == pet.Id);
            pets.Remove(toRemove);
            familiesService.UpdateFamily(FindFamily(pet));
        }

        public void UpdatePet(Pet pet)
        {
            Pet toUpdate = pets.First(p => p.Id == pet.Id);
            toUpdate.Species = pet.Species;
            toUpdate.Name = pet.Name;
            toUpdate.Age = pet.Age;
            familiesService.UpdateFamily(FindFamily(pet));
        }

        public Pet GetPet(int id)
        {
            return pets.FirstOrDefault(a => a.Id == id);
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