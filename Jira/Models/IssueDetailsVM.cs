using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class IssueDetailsVM
    {
        public int IssueID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public string PriorityName { get; set; }

        public string StatusName { get; set; }

        public string Assignee { get; set; }
    }
}
