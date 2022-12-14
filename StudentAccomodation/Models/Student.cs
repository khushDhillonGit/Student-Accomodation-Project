using System.ComponentModel.DataAnnotations;

namespace StudentAccomodation.Models
{
    public class Student
    {
        [Required]
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }

        [Display(Name = "House Name")]
        public int HouseId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }

        [Required]
        [Range(minimum:1000000,maximum:999999999)]
        public int studentNumber { get; set; }

        public DateTime StartDate { get; set; }

        public string UserId { get; set; }

        //one student can only be in one house
        public House? House { get; set; }
    }
}
