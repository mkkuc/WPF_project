using DatabaseLayer.Context;
using DataTransferObjects.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void Add(Account account)
        {
            try
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public Account Create(string login, string password, string email, string name, string surname, int roleID, Role role, bool isConfirmed, ICollection<Queue> queues, ICollection<Issue> issues)
        {
            return new Account
            {
                Login = login,
                Password = password,
                Email = email,
                Name = name,
                Surname = surname,
                RoleID = roleID,
                Role = role,
                IsConfirmed = isConfirmed,
                Queues = queues,
                Issues = issues
            };
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                Account account = db.Accounts.Find(id);
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void Edit(Account account)
        {
            try
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Issue> GetIssues(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Accounts.Find(id).Issues.ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Queue> GetQueues(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Accounts.Find(id).Queues.ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
