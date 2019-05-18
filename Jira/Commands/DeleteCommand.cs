using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class DeleteCommand
    {
        private static RoutedUICommand delete;
        static DeleteCommand()
        {
            delete = new RoutedUICommand("Delete person", "Delete", typeof(DeleteCommand));
        }
        public static RoutedUICommand Delete
        {
            get { return delete; }
        }
    }
}
