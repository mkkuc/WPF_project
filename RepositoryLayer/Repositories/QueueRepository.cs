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
    public class QueueRepository : IQueueRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void Add(Queue queue)
        {
            try
            {
                db.Queues.Add(queue);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public Queue Create(int groupID, Group group, int accountID, Account account)
        {
            return new Queue
            {
                GroupID = groupID,
                Group = group,
                AccountID = accountID,
                Account = account
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
                Queue queue = db.Queues.Find(id);
                db.Queues.Remove(queue);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void Edit(Queue queue)
        {
            try
            {
                db.Entry(queue).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
