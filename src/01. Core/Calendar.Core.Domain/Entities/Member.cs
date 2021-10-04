using Calendar.Core.Domain.Commons;
using System;
using System.Collections.Generic;

namespace Calendar.Core.Domain.Entities
{
    public class Member : Entity<Guid>
    {
        public Member()
        {
            Id = new Guid();
        }
        public string Name { get; set; }
        public List<EventItem> EventItems { get; set; }
        public List<EventItem> EventOrganizers { get; set; }

        public static Member AddNewItem(MemberItemCommand.AddNewItem data)
        {
            return new Member
            {
                Name = data.Name
            };
        }
    }
}
