using System.ComponentModel.DataAnnotations;

namespace StudentAccomodation.Models
{
    public class House
    {
        [Required]
        public int HouseId { get; set; }

        [Required]
        public string HouseName { get; set; }

        [Required]
        [Range(minimum: 400, maximum: 700)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public int MonthRent { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public string street { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string postalCode { get; set; }

        //one house can contain many 
        //public List<Student>? Students { get; set; }
        public List<Student>? Students { get; set; }
    }
}
