using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class WebAdultsService : IAdultsService
    {
        public IList<Adult> Adults { get; private set; }
        private readonly IFamiliesService familiesService;

        public WebAdultsService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetAdults();
        }
        
        public IList<Adult> GetAdults()
        {
            Adults = new List<Adult>();
            foreach (var family in familiesService.GetFamilies())
            {
                foreach (var adult in family.Adults)
                {
                    Adults.Add(adult);
                }
            }
            return Adults;
        }

        public void AddAdult(int? familyId, Adult adult)
        {
            int max = Adults.Max(adult => adult.Id);
            adult.Id = (++max);
            Adults.Add(adult);
            Family family = familiesService.GetFamilyById(familyId);
            family.Adults.Add(adult);
            familiesService.UpdateFamily(family);
        }

        public void RemoveAdult(Adult adult)
        {
            Adult toRemove = Adults.First(a => a.Id == adult.Id);
            Adults.Remove(toRemove);
            familiesService.UpdateFamily(FindFamily(adult));
        }

        public void UpdateAdult(Adult adult)
        {
            Adult toUpdate = Adults.First(a => a.Id == adult.Id);
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

        public Adult GetAdult(int? id)
        {
            return Adults.FirstOrDefault(a => a.Id == id);
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