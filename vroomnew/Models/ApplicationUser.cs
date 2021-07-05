using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name="Office Phone Number")]
        public string phoneNO2 { get; set; }
        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}
