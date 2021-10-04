using Calendar.Core.Domain.Commons;
using System;

namespace Calendar.Core.Domain.Entities
{
    public class Location : Entity<Guid>
    {
        public Location()
        {
            Id = new Guid();
        }
        public string Name { get; set; }
    }
}
