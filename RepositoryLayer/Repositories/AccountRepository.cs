﻿using DatabaseLayer.Context;
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

        public Role GetRole(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Roles.Find(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Account GetAccount(string login, string password)
        {
            Account account = db.Accounts.FirstOrDefault(t => t.Login == login && t.Password == password);
            if (account == null)
                return null;
            return db.Accounts.Find(account.AccountID);
        }

        public Role GetRole(string role)
        {
            try
            {
                return db.Roles.FirstOrDefault(r => r.Name == role);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsLoginCorrect(string login)
        {
            Account account = db.Accounts.FirstOrDefault(t => t.Login == login);
            if (account == null)
                    return true;
            return false;
        }

        public bool IsEmailCorrect(string email)
        {
            Account account = db.Accounts.FirstOrDefault(t => t.Email == email);
            if (account == null)
                if (IsValidEmail(email))
                    return true;
            return false;
        }

        public bool IsEmailCorrect(int id, string email)
        {
            Account account = db.Accounts.FirstOrDefault(t => t.Email == email && t.AccountID != id);
            if (account == null)
                if (IsValidEmail(email))
                    return true;
            return false;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var check = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Account Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Accounts.FirstOrDefault(a => a.AccountID == id);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public List<Account> GetAll()
        {
            return db.Accounts.ToList();
        }

        public List<Account> GetConfirmed()
        {
            return db.Accounts.Where(a => a.IsConfirmed == true).ToList();
        }

        public List<Account> GetNotConfirmed()
        {
            return db.Accounts.Where(a => a.IsConfirmed == false).ToList();
        }

        public List<Account> GetNotAssigned()
        {
            return db.Accounts.Where(a => a.GroupID == null && !a.Role.Name.Equals("Admin") && a.IsConfirmed).ToList();
        }
    }
}
