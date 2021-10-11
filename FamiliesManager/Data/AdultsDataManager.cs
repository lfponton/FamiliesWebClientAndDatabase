using System.Collections.Generic;
using System.Linq;
using FileData;
using Models;

namespace FamiliesManager.Data
{
    public class AdultsDataManager : IAdultsDataManager
    {
        private IFileContext fileContext;
        private IList<Adult> adults;

        public AdultsDataManager(IFileContext fileContext)
        {
            this.fileContext = fileContext;
            adults = fileContext.Adults;
        }
        
        public IList<Adult> GetAdults()
        {
            return adults;
        }

        public void AddAdult(Adult adult)
        {
            int max = adults.Max(adult => adult.Id);
            adult.Id = (++max);
            adults.Add(adult);
            fileContext.Adults = adults;
            fileContext.SaveChanges();
        }

        public void RemoveAdult(Adult adult)
        {
            Adult toRemove = adults.First(a => a.Id == adult.Id);
            adults.Remove(toRemove);
            fileContext.SaveChanges();
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
            fileContext.Adults = adults;
            fileContext.SaveChanges();
        }

        public Adult GetAdult(int id)
        {
            return adults.FirstOrDefault(a => a.Id == id);
        }
    }
}