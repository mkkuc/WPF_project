using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class UpdateIssueOnListCommand
    {
        private static RoutedUICommand update;
        static UpdateIssueOnListCommand()
        {
            update = new RoutedUICommand("Update issue on list", "UpdateIssueOnList", typeof(UpdateIssueOnListCommand));
        }
        public static RoutedUICommand Update
        {
            get { return update; }
        }

    }
}
