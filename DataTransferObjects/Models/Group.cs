using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "GroupOwnerID")]
        public int GroupOwnerID { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Queue> Queue { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
