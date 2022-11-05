using System.ComponentModel.DataAnnotations;

namespace StudentAccomodation.Models
{
    public class Student
    {
        [Required]
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }

        [Display(Name = "House Name")]
        public int HouseID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }

        //one student can only be in one house
        public House? House { get; set; }
    }
}
