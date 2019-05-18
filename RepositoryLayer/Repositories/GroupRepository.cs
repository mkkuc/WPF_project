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

        public void Add(Group group)
        {
            try
            {
                db.Groups.Add(group);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
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

        public List<Issue> GetIssues(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                return db.Groups.Find(id).Issues.ToList();
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
    }
}