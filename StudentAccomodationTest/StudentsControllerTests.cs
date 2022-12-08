using Microsoft.EntityFrameworkCore;
using StudentAccomodation.Controllers;
using StudentAccomodation.Data;
using StudentAccomodation.Models;

namespace StudentAccomodationTest
{
    [TestClass]
    public class StudentsControllerTests
    {

        private ApplicationDbContext _context;
        StudentsController controller; 

        List<Student> students = new List<Student>();

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var house = new House
            {
                HouseId = 2,
                HouseName = "Hell",
                OwnerName = "Demon",
                OwnerPhone = "+1 999 999 9999",
                Occupancy = 10,
                MonthRent = 500,
                HouseNumber = 0,
                Street = "Blood Street",
                City = "Hell City",
                PostalCode = "DEA TH1"
            };
            _context.Add(house);

            students.Add(new Student { 
                StudentId = 20,
                HouseId = 2,
                FirstName = "John",
                LastName = "Doe",
                StudentEmail = "johndoe@gmail.com",
                studentNumber = 1000000
            });

            students.Add(new Student
            {
                StudentId = 21,
                HouseId = 2,
                FirstName = "Daniel",
                LastName = "Rama",
                StudentEmail = "danielrama@gmail.com",
                studentNumber = 1000001
            });

            students.Add(new Student
            {
                StudentId = 20,
                HouseId = 2,
                FirstName = "Rama",
                LastName = "Daniel",
                StudentEmail = "ramadaniel@gmail.com",
                studentNumber = 1000002
            });
            foreach (var student in students) {
                _context.Add(student);
            };
            _context.SaveChanges();
            controller = new StudentsController(_context);
        }

        //EDIT: POST

        //DELETE: GET


    }
}