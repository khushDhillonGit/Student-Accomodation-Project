using Microsoft.AspNetCore.Mvc;
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

            students.Add(new Student
            {
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
                StudentId = 22,
                HouseId = 2,
                FirstName = "Rama",
                LastName = "Daniel",
                StudentEmail = "ramadaniel@gmail.com",
                studentNumber = 1000002
            });
            foreach (var student in students)
            {
                _context.Students.Add(student);
            };
            _context.SaveChanges();
            //controller = new StudentsController(_context);
        }



        //DELETE: GET
        [TestMethod]
        public void DeleteRecievesNull()
        {

            var result = (ViewResult)controller.Delete(null).Result;
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteRecievesInvalidId()
        {
            var result = (ViewResult)controller.Delete(-10).Result;
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteNoStudentFound()
        {
            var result = (ViewResult)controller.Delete(19).Result;
            Assert.IsNull(result.Model);
        }


        [TestMethod]
        public void DeleteNoStudentFoundViewReturned()
        {
            var result = (ViewResult)controller.Delete(19).Result;
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteCorrectViewReturned()
        {
            var result = (ViewResult)controller.Delete(20).Result;
            Assert.AreEqual("Delete", result.ViewName);
        }


        //EDIT: POST
        [TestMethod]
        public void EditIdDoNotMatch()
        {
            var results = (ViewResult)controller.Edit(21, students.Find(s => s.StudentId == 20)).Result;
            Assert.AreEqual("404", results.ViewName);
        }

        [TestMethod]
        public void EditStudentDoesNotExist()
        {
            var results = (ViewResult)controller.Edit(19, new Student { StudentId = 19 }).Result;
            Assert.AreEqual("404", results.ViewName);
        }

        [TestMethod]
        public void EditRedirectToIndexAction()
        {
            var results = (RedirectToActionResult)controller.Edit(20, students.Find(s => s.StudentId == 20)).Result;
            Assert.AreEqual("Index", results.ActionName);
        }

        [TestMethod]
        public void EditModelStateInvalidStoresViewData()
        {
            controller.ModelState.AddModelError("Model state is Invalid", "Required");
            var results = (ViewResult)controller.Edit(20, students.Find(s => s.StudentId == 20)).Result;
            Assert.IsNotNull(results.ViewData);
        }


        [TestMethod]
        public void EditModelStateInvalidReturnsCorrectView()
        {
            controller.ModelState.AddModelError("Model state is Invalid", "Required");
            var results = (ViewResult)controller.Edit(20, students.Find(s => s.StudentId == 20)).Result;
            Assert.AreEqual("Edit", results.ViewName);
        }



    }
}