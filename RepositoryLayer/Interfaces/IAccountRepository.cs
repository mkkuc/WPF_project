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
        Account Get(int? id);
        List<Issue> GetIssues(int? id);
        List<Queue> GetQueues(int? id);
        Role GetRole(int? id);
        Role GetRole(string role);
        Account GetAccount(string login, string password);
        bool IsLoginCorrect(string login);
        bool IsEmailCorrect(string email);
        bool IsEmailCorrect(int id, string email);


    }
}
