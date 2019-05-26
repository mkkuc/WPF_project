using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class GroupDetailsVM
    {
        public string Name { get; set; }
        public string NameAndSurnameOwner { get; set; }
        public List<Account> AccountList { get; set; } = new List<Account>();
        public List<string> NameAndSurnameList { get; set; } = new List<string>();

    }
}
