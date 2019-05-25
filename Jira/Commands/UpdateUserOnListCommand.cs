using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class UpdateUserOnListCommand
    {
        private static RoutedUICommand update;
        static UpdateUserOnListCommand()
        {
            update = new RoutedUICommand("Update user on list", "UpdateUserOnList", typeof(UpdateUserOnListCommand));
        }
        public static RoutedUICommand Update
        {
            get { return update; }
        }
    }
}
