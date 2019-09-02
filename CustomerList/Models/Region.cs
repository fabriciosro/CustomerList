using System;
using System.Collections.Generic;

namespace CustomerList.Models
{
    public class Region
    {
        public Region()
        {
            this.City = new HashSet<City>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<City> City { get; set; }
    }
}
