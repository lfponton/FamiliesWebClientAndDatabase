using System.Collections.Generic;
using System.Linq;
using FileData;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class WebFamiliesService : IFamiliesService
    {
        private IFileContext fileContext;
        public IList<Family> Families { get; }

        public WebFamiliesService(IFileContext fileContext)
        {
            this.fileContext = fileContext;
            Families = fileContext.Families;
        }

        public IList<Family> GetFamilies()
        {
            return Families;
        }

        public void AddFamily(Family family)
        {
            int max = Families.Max(f => f.Id);
            family.Id = (++max);
            Families.Add(family);
            fileContext.Families = Families;
            fileContext.SaveChanges();
        }

        public void RemoveFamily(Family family)
        {
            Family toRemove = Families.First(f => f.Id == family.Id);
            Families.Remove(toRemove);
            fileContext.SaveChanges();
        }

        public void UpdateFamily(Family family)
        {
            Family toUpdate = Families.First(f => f.Id == family.Id);
            toUpdate.StreetName = family.StreetName;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            fileContext.Families = Families;
            fileContext.SaveChanges();
        }

        public Family GetFamilyById(int? familyId)
        {
            return Families.FirstOrDefault(f => f.Id == familyId);
        }
    }
}