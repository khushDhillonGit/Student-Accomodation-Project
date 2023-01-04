using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StudentAccomodation.Models
{
    public class House
    {
        
        [Required]
        [Display(Name = "House Id")]
        public int HouseId { get; set; }

        [Required]
        [Display(Name = "Name of House")]
        public string HouseName { get; set; }

        public string? Image { get; set; } 

        [Required]
        [Display(Name = "Owner's Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string OwnerName { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Owner's Contact")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        public string OwnerPhone { get; set; }

        [Required]
        [Display(Name = "Occupancy Available")]
        [Range(minimum: 0, maximum: 10)]
        public int Occupancy { get; set; }

        [Required]
        [Range(minimum: 400, maximum: 700)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Monthly Rent")]
        public int MonthRent { get; set; }

        [Required]
        [Display(Name = "House Number")]
        public int HouseNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "User Id")]
        public string? UserId { get; set; }

        //one house can contain many 
        //public List<Student>? Students { get; set; }
        public List<Student>? Students { get; set; }
    }
}