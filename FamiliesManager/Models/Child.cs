using System.Collections.Generic;

namespace Models {
public class Child : Person {
    
    public IList<Interest> Interests { get; set; }
    public IList<Pet> Pets { get; set; }
}
}