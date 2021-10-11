using System.Collections.Generic;
using System.Linq;
using FileData;
using Models;

namespace FamiliesManager.Data
{
    public class FamiliesDataManager : IFamiliesDataManager
    {
        private IFileContext fileContext;
        private IList<Family> families;

        public FamiliesDataManager(IFileContext fileContext)
        {
            this.fileContext = fileContext;
            families = fileContext.Families;
        }

        public IList<Family> GetFamilies()
        {
            return families;
        }

        public void AddFamily(Family family)
        {
            families.Add(family);
            fileContext.Families = families;
            fileContext.SaveChanges();
        }

        public void RemoveFamily(Family family)
        {
            Family toRemove = families.First(f => f.Id == family.Id);
            families.Remove(toRemove);
            fileContext.SaveChanges();
        }

        public void UpdateFamily(Family family)
        {
            Family toUpdate = families.First(f => f.Id == family.Id);
            toUpdate.StreetName = family.StreetName;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            fileContext.Families = families;
            fileContext.SaveChanges();
        }

        public Family getFamily(int id)
        {
            return families.FirstOrDefault(f => f.Id == id);
        }
    }
}