using FirstEFCoreIntro;
using FirstEFCoreIntro.Entities;
using System.Text.RegularExpressions;

namespace EFCoreTest
{
    public class Tests
    {
        [Test]
        public void AddTeacher_AddsSuccessfully()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var department = new Department
                {
                    Name = "Mathematics",
                    Description = "Algebra and Geometry",
                    Financing = 100000
                };
                db.Departments.Add(department);
                db.SaveChanges();

                var subject = new Subject
                {
                    Name = "Math",
                    Description = "Algebra",
                    Time = 45,
                    DepartmentId = department.Id
                };
                db.Subjects.Add(subject);
                db.SaveChanges();

                Teacher teacher = new Teacher
                {
                    FullName = "Ivan Petrov",
                    Age = 25,
                    Salary = 5
                };
                db.Teachers.Add(teacher);
                db.SaveChanges();

                var addedTeacher = db.Teachers.FirstOrDefault();
                Assert.IsNotNull(addedTeacher);
                Assert.AreEqual("Ivan Petrov", addedTeacher.FullName);
            }
        }

    }
}