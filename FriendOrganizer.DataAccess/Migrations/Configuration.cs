namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(f => f.FirstName,
                new Model.Friend { FirstName = "John", LastName = "Smith" },
                new Model.Friend { FirstName = "Jane", LastName = "Smith" },
                new Model.Friend { FirstName = "Joe", LastName = "Hall" },
                new Model.Friend { FirstName = "Bill", LastName = "Jones" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new Model.ProgrammingLanguage { Name = "C#" },
                new Model.ProgrammingLanguage { Name = "TypeScript" },
                new Model.ProgrammingLanguage { Name = "F#" },
                new Model.ProgrammingLanguage { Name = "Swift" },
                new Model.ProgrammingLanguage { Name = "Python" }
                );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new Model.FriendPhoneNumber { Number = "+1 123456789", FriendId = context.Friends.First().Id });

            context.Meetings.AddOrUpdate(m => m.Title,
                new Model.Meeting
                {
                    Title = "Watching football",
                    DateFrom = new DateTime(2019, 12, 1),
                    DateTo = new DateTime(2019, 12, 1),
                    Friends = new List<Friend>()
                    {
                        context.Friends.Single(f => f.FirstName == "John" && f.LastName == "Smith"),
                        context.Friends.Single(f => f.FirstName == "Joe" && f.LastName == "Hall")
                    }
                });
        }
    }
}
