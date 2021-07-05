using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(225)]
        public string  Name { get; set; }
    }
}
