using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IQueueRepository
    {
        Queue Create(int groupID, Group group, int accountID, Account account);

        void Add(int AccountID, int GroupID);
        void Edit(Queue queue);
        void Delete(int? id);
    }
}
