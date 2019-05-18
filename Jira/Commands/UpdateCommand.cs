using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jira.Commands
{
    public class UpdateCommand
    {
        private static RoutedUICommand update;
        static UpdateCommand()
        {
            update = new RoutedUICommand("Update person", "Update", typeof(UpdateCommand));
        }
        public static RoutedUICommand Update
        {
            get { return update; }
        }
    }
}
