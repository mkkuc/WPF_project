using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
