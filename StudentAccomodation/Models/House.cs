using System.ComponentModel.DataAnnotations;

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

        //one house can contain many 
        //public List<Student>? Students { get; set; }
        public List<Student>? Students { get; set; }
    }
}