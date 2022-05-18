using System;
using System.Collections.Generic;

namespace Dynamo.Business
{
    public class Region 
    {
        public Guid RegionId { get; set; }
        public string Name { get; set;}
        public List<Store> Stores { get; set; }

        public Region(string name) 
        {
            RegionId = Guid.NewGuid();
            Name = name;
            Stores = new List<Store>();
        }
    }
}
