using System;

namespace Dynamo.Business
{
    public class Store
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }

        public Store(string name)
        {
            StoreId = Guid.NewGuid();
            Name = name;
        }
    }
}