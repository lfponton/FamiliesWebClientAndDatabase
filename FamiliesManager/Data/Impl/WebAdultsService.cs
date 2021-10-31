using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class AdultsService : IAdultsService
    {
        private IList<Adult> adults;
        private IFamiliesService familiesService;

        public AdultsService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetAdults();
        }
        
        public IList<Adult> GetAdults()
        {
            adults = new List<Adult>();
            foreach (var family in familiesService.GetFamilies())
            {
                foreach (var adult in family.Adults)
                {
                    adults.Add(adult);
                }
            }
            return adults;
        }

        public void AddAdult(int? familyId, Adult adult)
        {
            int max = adults.Max(adult => adult.Id);
            adult.Id = (++max);
            adults.Add(adult);
            Family family = familiesService.GetFamilyById(familyId);
            family.Adults.Add(adult);
            familiesService.UpdateFamily(family);
        }

        public void RemoveAdult(Adult adult)
        {
            Adult toRemove = adults.First(a => a.Id == adult.Id);
            adults.Remove(toRemove);
            familiesService.UpdateFamily(FindFamily(adult));
        }

        public void UpdateAdult(Adult adult)
        {
            Adult toUpdate = adults.First(a => a.Id == adult.Id);
            toUpdate.JobTitle = adult.JobTitle;
            toUpdate.Age = adult.Age;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            toUpdate.Weight = adult.Weight;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            familiesService.UpdateFamily(FindFamily(adult));
        }

        public Adult GetAdult(int id)
        {
            return adults.FirstOrDefault(a => a.Id == id);
        }

        private Family FindFamily(Adult adult)
        {
            Family familyToUpdate = null;
            foreach (var family in familiesService.GetFamilies())
            {
                if (family.Adults.Any(a => a.Id == adult.Id))
                {
                    familyToUpdate = family;
                    break;
                }
            }
            return familyToUpdate;
        }
    }
}