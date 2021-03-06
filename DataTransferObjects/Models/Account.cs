﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }

        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        [Required]
        public virtual Role Role { get; set; }

        [Display(Name = "Confirmed: ")]
        public bool IsConfirmed { get; set; } = true;

        [ForeignKey("Group")]
        public int? GroupID { get; set; }

        public Group Group { get; set; }

        public virtual ICollection<Queue> Queues { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
