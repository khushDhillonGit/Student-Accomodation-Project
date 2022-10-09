using System.ComponentModel.DataAnnotations;

namespace StudentAccomodation.Models
{
    public class Student
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string StudentEmail { get; set; }

        //one student can only be in one house
        public House? House { get; set; }
    }
}
