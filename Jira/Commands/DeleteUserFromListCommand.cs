using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class DeleteUserFromListCommand
    {
        private static RoutedUICommand delete;
        static DeleteUserFromListCommand()
        {
            delete = new RoutedUICommand("Delete user from list", "DeleteFromUserList", typeof(DeleteUserFromListCommand));
        }
        public static RoutedUICommand Delete
        {
            get { return delete; }
        }
    }
}
