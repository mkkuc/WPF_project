using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IGroupRepository
    {
        Group Create(string name, int groupOwnerID, ICollection<Account> accounts, ICollection<Queue> queues, ICollection<Issue> issues);

        void Add(Group group);
        void Edit(Group group);
        void Delete(int? id);
        Group Get(int? id);
        List<Group> GetAll();
        List<Account> GetUsersFromGroup(int? id);
        List<Issue> GetIssues(int? id);
        List<Queue> GetQueues(int? id);
        List<Account> GetAccounts(int? id);
    }
}
