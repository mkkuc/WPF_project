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
    public class IssueRepository : IIssueRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void Add(Issue issue)
        {
            try
            {
                db.Issues.Add(issue);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public Issue Create(string title, string description, int priorityID, Priority priority)
        {
            return new Issue
            {
                Title = title,
                Description = description,
                PriorityID = priorityID,
                Priority = priority
            };
        }

        public List<Issue> GetGroupIssues(int groupId)
        {
            return db.Issues.Where(i => i.GroupID == groupId).ToList();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Null argument");
            }
            try
            {
                Issue issue = db.Issues.Find(id);
                db.Issues.Remove(issue);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void Edit(Issue issue)
        {
            try
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Status> GetAllStatuses()
        {
            return db.Statuses.ToList();
        }
    }
}
