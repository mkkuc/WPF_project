using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class DeleteIssueFromListCommand
    {
        private static RoutedUICommand delete;
        static DeleteIssueFromListCommand()
        {
            delete = new RoutedUICommand("Delete issue from list", "DeleteFromIssueList", typeof(DeleteIssueFromListCommand));
        }
        public static RoutedUICommand Delete
        {
            get { return delete; }
        }
    }
}
