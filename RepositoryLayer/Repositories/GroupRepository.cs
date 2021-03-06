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
    public class GroupRepository : IGroupRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void Add(string name, int groupOwnerID, ICollection<Account> accounts, ICollection<Queue> queues, ICollection<Issue> issues)
        { 
            
            Group group = new Group
            {
                Name = name,
                GroupOwnerID = groupOwnerID,
                Queues = queues,
                Issues = issues,
            };

            var accountsToAdd = new List<Account>();
            foreach(var account in accounts)
            {
                accountsToAdd.Add(db.Accounts.Find(account.AccountID));
            }
            group.Accounts = accountsToAdd;
            db.Groups.Add(group);
            db.SaveChanges();

        }

        public Group Create(string name, int groupOwnerID, ICollection<Account> accounts, ICollection<Queue> queues, ICollection<Issue> issues)
        {
            return new Group
            {
                Name = name,
                GroupOwnerID = groupOwnerID,
                Accounts = accounts,
                Queues = queues,
                Issues = issues,
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
                Group group = db.Groups.Find(id);
                db.Groups.Remove(group);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void Edit(Group group)
        {
            try
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Group Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Groups.FirstOrDefault(g => g.GroupID == id);
            }
            catch(Exception e)
            {
                throw new Exception();
            }
        }

        public List<Account> GetAccounts(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Groups.Find(id).Accounts.ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public Group GetByUser(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Groups.FirstOrDefault(g => g.GroupID == account.GroupID);
            }
            catch (Exception e)
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
                Group group = db.Groups.Find(id);
                return group.Issues.ToList();
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
                return db.Groups.Find(id).Queues.ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Account> GetUnacceptedMembers(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Accounts.Where(a => a.Queues.Any(q => q.GroupID == id)).ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Account> GetUsersFromGroup(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Groups.Find(id).Accounts.Where(a => a.AccountID != db.Groups.Find(id).GroupOwnerID).ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void DeleteUserFromGroup(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                var user = db.Accounts.Find(userId);
                
                user.GroupID = null;
                db.Entry(user).State = EntityState.Modified;
                db.Issues.RemoveRange(db.Issues.Where(i => i.AssigneeID == userId));

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void AcceptUserToRequest(int? userId, int? groupId)
        {
            if (userId == null || groupId == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                var user = db.Accounts.Find(userId);

                user.GroupID = groupId;
                db.Entry(user).State = EntityState.Modified;
                db.Queues.RemoveRange(db.Queues.Where(i => i.AccountID == userId));

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
