using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAccountRepository
    {
        Account Create(string login, string password, string email, string name, string surname, int roleID, Role role, bool isConfirmed, ICollection<Queue> queues, ICollection<Issue> issues);

        void Add(Account account);
        void Edit(Account account);
        void Delete(int? id);
        List<Issue> GetIssues(int? id);
        List<Queue> GetQueues(int? id);

    }
}
