using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Issue")]
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("Priority")]
        public int PriorityID { get; set; }

        public virtual Priority Priority { get; set; }

        [ForeignKey("Status")]
        public int StatusID { get; set; }

        public virtual Status Status { get; set; }
    }
}
