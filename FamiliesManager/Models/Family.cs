using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models {
    public class Family
    {

        public int Id { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public IList<Adult> Adults { get; set; }
        public IList<Child> Children { get; set; }
        public IList<Pet> Pets { get; set; }

        public Family()
        {
            Adults = new List<Adult>();
            Children = new List<Child>();
            Pets = new List<Pet>();
        }
    }
}