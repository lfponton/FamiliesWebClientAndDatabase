using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class WebChildrenService : IChildrenService
    {
        private IList<Child> children;
        private IFamiliesService familiesService;

        public WebChildrenService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetChildren();
        }
        
        public IList<Child> GetChildren()
        {
            children = new List<Child>();
            foreach (var family in familiesService.GetFamilies())
            {
                foreach (var child in family.Children)
                {
                    children.Add(child);
                }
            }
            return children;
        }

        public void AddChild(int? familyId, Child child)
        {
            int max = children.Max(child => child.Id);
            child.Id = (++max);
            children.Add(child);
            Family family = familiesService.GetFamilyById(familyId);
            family.Children.Add(child);
            familiesService.UpdateFamily(family);
        }

        public void RemoveChild(Child child)
        {
            Child toRemove = children.First(c => c.Id == child.Id);
            children.Remove(toRemove);
            familiesService.UpdateFamily(FindFamily(child));
        }

        public void UpdateChild(Child child)
        {
            Child toUpdate = children.First(c => c.Id == child.Id);
            toUpdate.Interests = child.Interests;
            toUpdate.Pets = child.Pets;
            toUpdate.Age = child.Age;
            toUpdate.Height = child.Height;
            toUpdate.Sex = child.Sex;
            toUpdate.Weight = child.Weight;
            toUpdate.EyeColor = child.EyeColor;
            toUpdate.HairColor = child.HairColor;
            toUpdate.FirstName = child.FirstName;
            toUpdate.LastName = child.LastName;
            familiesService.UpdateFamily(FindFamily(child));
        }

        public Child GetChild(int id)
        {
            return children.FirstOrDefault(c => c.Id == id);
        }

        private Family FindFamily(Child child)
        {
            Family familyToUpdate = null;
            foreach (var family in familiesService.GetFamilies())
            {
                if (family.Children.Any(c => c.Id == child.Id))
                {
                    familyToUpdate = family;
                    break;
                }
            }
            return familyToUpdate;
        }
    }
}