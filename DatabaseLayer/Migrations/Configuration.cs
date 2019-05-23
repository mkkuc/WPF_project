namespace DatabaseLayer.Migrations
{
    using DataTransferObjects.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseLayer.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseLayer.Context.DatabaseContext context)
        {
            Status[] statuses = new Status[]
            {
                new Status() { StatusID = 1, Name = "New"},
                new Status() { StatusID = 2, Name = "In progress"},
                new Status() { StatusID = 3, Name = "Done"}
            };

            foreach(Status status in statuses)
            {
                context.Statuses.AddOrUpdate(a => new { a.StatusID, a.Name }, status);
            }

            Priority[] priorities = new Priority[]
            {
                new Priority() { PriorityID = 1, Name = "Low" },
                new Priority() { PriorityID = 2, Name = "Medium" },
                new Priority() { PriorityID = 3, Name = "High" },
            };

            foreach (Priority priority in priorities)
            {
                context.Priority.AddOrUpdate(a => new { a.PriorityID, a.Name }, priority);
            }

            Role[] roles = new Role[]
            {
                new Role() { RoleID = 1, Name = "Admin"},
                new Role() { RoleID = 2, Name = "GroupOwner"},
                new Role() { RoleID = 3, Name = "User"},
            };

            foreach (Role role in roles)
            {
                context.Roles.AddOrUpdate(a => new { a.RoleID, a.Name }, role);
            }

            context.SaveChanges();

            Account[] accounts = new Account[]
            {
                new Account() { AccountID = 1, Login = "admin", Password = "admin", Email = "admin@admin.pl", Name = "Andrzej", Surname = "Grabowski", RoleID = 1, IsConfirmed = true},

                new Account() { AccountID = 2, Login = "owner", Password = "owner", Email = "owner@owner.pl", Name = "W³aœciciel", Surname = "Grupy", RoleID = 2, IsConfirmed = true},

                new Account() { AccountID = 3, Login = "usernot", Password = "usernot", Email = "usernot@usernot.pl", Name = "Niepotwierdzony", Surname = "U¿ytkownik", RoleID = 3, IsConfirmed = false },

                new Account() { AccountID = 4, Login = "user", Password = "user", Email = "user@user.pl", Name = "Zwyk³y", Surname = "U¿ytkownik", RoleID = 3, IsConfirmed = true},             
            };

            foreach (Account account in accounts)
            {
                context.Accounts.AddOrUpdate(a => new { a.AccountID, a.Login, a.Password, a.Email, a.Name, a.Surname, a.RoleID, a.IsConfirmed }, account);
            }

            context.SaveChanges();

            var list = new List<Account>();
            list.Add(accounts[3]);
            list.Add(accounts[1]);
            ICollection<Account> collection = list;

            Group group = new Group()
            {
                GroupID = 1,
                Name = "Pierwsza grupa fajna",
                GroupOwnerID = 2,
                Accounts = collection
            };

            var listOfGroups = new Collection<Group>();
            listOfGroups.Add(group);
            ICollection<Group> groups = listOfGroups;
            context.Groups.AddRange(groups);
            

        }
    }
}
