using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IIssueRepository
    {
        Issue Create(string title, string description, int priorityID, Priority priority);

        void Add(Issue issue);
        void Edit(Issue issue);
        void Delete(int? id);
        Issue Get(int? id);
    }
}
