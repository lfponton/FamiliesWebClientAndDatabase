using System.Collections.Generic;
using Models;

namespace FamiliesManager.Data
{
    public interface IChildrenService
    {
        IList<Child> Children { get; }
        IList<Child> GetChildren();
        void AddChild(int? familyId, Child child);
        void RemoveChild(Child child);
        void UpdateChild(Child child);
        Child GetChild(int? id);
    }
}