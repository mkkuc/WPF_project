using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class UserDetailsVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CurrentRoleName { get; set; }
    }
}
