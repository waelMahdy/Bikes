using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vroomnew.Extensions;

namespace vroomnew.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public Make Make { get; set; }
        [RegularExpression("^[1-9]*$",ErrorMessage ="Select Make")]
        public int MakeID { get; set; }
        public Model Model { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage = "Select Model")]

        public int ModelID { get; set; }
        [Required]
        [YearRangTillDat(2000,ErrorMessage ="Invalid year")]
        public int Year { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Provide mileage")]
        public int Mileage { get; set; }

        public string Features { get; set; }
        [Display(Name ="Seller name")]
        [Required(ErrorMessage ="Provide a seller name")]
        public string SellerName { get; set; }

        [Display(Name = "Seller Email")]
        [EmailAddress]
        public string SellerEmail { get; set; }

        [Display(Name = "Seller phone")]
        [Required]
        public string SellerPhone { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        [RegularExpression("^[1-9A-Z]*$", ErrorMessage = "Select currency")]
        public string Currency { get; set; }
        public string ImagePath { get; set; }

    }
}
