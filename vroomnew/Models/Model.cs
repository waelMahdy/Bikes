using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [StringLength(225)]
        public string Name { get; set; }
        public Make Make { get; set; }

        [ForeignKey("Make")]
        public int makeId { get; set; }

    }
}
