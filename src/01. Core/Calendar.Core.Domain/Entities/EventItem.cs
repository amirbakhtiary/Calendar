using Calendar.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Core.Domain.Entities
{
    public class EventItem : Entity<Guid>
    {
        public string Name { get; private set; }
        public DateTime EventTime { get; private set; }
        public Location Location { get; private set; }
        public Member EventOrganizer { get; private set; }
        public List<Member> Members { get; private set; }

        public static EventItem AddEventItem(EventItemCommand.AddNewEventItem data)
        {
            return new EventItem
            {
                Id = data.Id,
                EventOrganizer = Member.AddNewItem(new MemberItemCommand.AddNewItem
                {
                    Name = data.EventOrganizer
                }),
                Name = data.Name,
                EventTime = data.EventTime,
                Location = new Location { Name = data.Location },
                Members = AddItemsToList(data.Members)
            };
        }

        public void UpdateEventItem(EventItemCommand.UpdateEventItem data)
        {
            ClearMembers();

            EventOrganizer = Member.AddNewItem(new MemberItemCommand.AddNewItem
            {
                Name = data.EventOrganizer
            });
            Name = data.Name;
            EventTime = data.EventTime;
            Location = new Location { Name = data.Location };
            Members = AddItemsToList(data.Members);
        }

        private void ClearMembers()
        {
            if (Members.Any()) Members.Clear();
        }

        private static List<Member> AddItemsToList(List<string> items) =>
            items.Select(o => Member.AddNewItem(new MemberItemCommand.AddNewItem { Name = o })).ToList();
    }
}
