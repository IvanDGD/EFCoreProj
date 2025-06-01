using FirstEFCoreIntro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FirstEFCoreIntro
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            AppDbContext db = new AppDbContext();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== Главное меню ===");
                Console.WriteLine("1. Группы");
                Console.WriteLine("2. Студенты");
                Console.WriteLine("3. Преподаватели");
                Console.WriteLine("4. Кафедры");
                Console.WriteLine("5. Предметы");
                Console.WriteLine("6. Паспорта");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GroupMenu(db);
                        break;
                    case "2":
                        StudentMenu(db);
                        break;
                    case "3":
                        TeacherMenu(db);
                        break;
                    case "4":
                        DepartmentMenu(db);
                        break;
                    case "5":
                        SubjectMenu(db);
                        break;
                    case "6":
                        PassportMenu(db);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }


        static void GroupMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Группы --");
                Console.WriteLine("1. Добавить группу");
                Console.WriteLine("2. Просмотреть группы");
                Console.WriteLine("3. Обновить группу");
                Console.WriteLine("4. Удалить группу");
                Console.WriteLine("0. Назад");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGroup(db);
                        break;
                    case "2":
                        ShowGroups(db);
                        break;
                    case "3":
                        UpdateGroup(db);
                        break;
                    case "4":
                        DeleteGroup(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddGroup(AppDbContext db)
        {
            Group group = new Group();
            Console.Write("Введите название группы (до 10 символов): ");
            group.Name = Console.ReadLine();
            db.Groups.Add(group);
            db.SaveChanges();
        }
        static void ShowGroups(AppDbContext db)
        {
            var groups = db.Groups.Include(g => g.Students).ToList();
            foreach (var group in groups)
            {
                Console.WriteLine(group); 
            }
        }
        static void UpdateGroup(AppDbContext db)
        {
            Console.WriteLine("Enter id: ");
            int Id = int.Parse(Console.ReadLine());
            var group = db.Groups.FirstOrDefault(g => g.Id == Id);
            if (group == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            Console.Write("Введите новое название (до 10 символов): ");
            string newName = Console.ReadLine();
            group.Name = newName;
            db.SaveChanges();    
            
        }
        static void DeleteGroup(AppDbContext db)
        {
            Console.Write("Введите ID группы для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Некорректный ID.");
                return;
            }

            var group = db.Groups.Include(g => g.Students).FirstOrDefault(g => g.Id == id);

            if (group.Students.Any())
            {
                Console.WriteLine("Нельзя удалить группу, в которой есть студенты.");
                return;
            }

            db.Groups.Remove(group); 
            db.SaveChanges();
        }


        static void StudentMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Студенты --");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Просмотреть студентов");
                Console.WriteLine("3. Обновить студента");
                Console.WriteLine("4. Удалить студента");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent(db);
                        break;
                    case "2":
                        ShowStudents(db);
                        break;
                    case "3":
                        UpdateStudent(db);
                        break;
                    case "4":
                        DeleteStudent(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddStudent(AppDbContext db)
        {
            Student student = new Student();
            Console.Write("Имя: ");
            student.Name = Console.ReadLine();

            Console.Write("Возраст: ");
            student.Age = int.Parse(Console.ReadLine());

            Console.Write("Email: ");
            student.Email = Console.ReadLine();

            Console.Write("Стипендия: ");
            student.Scholarship = decimal.Parse(Console.ReadLine());

            Console.Write("Формат обучения (0 - Очный, 1 - Заочный): ");
            student.StudyFormat = (StudyFormat)int.Parse(Console.ReadLine());

            Console.Write("ID группы: ");
            student.GroupId = int.Parse(Console.ReadLine());

            db.Students.Add(student);
            db.SaveChanges();
        }
        static void ShowStudents(AppDbContext db)
        {
            foreach (var student in db.Students.Include(s => s.Group))
            {
                Console.WriteLine(student);
            }
        }
        static void UpdateStudent(AppDbContext db)
        {
            Console.Write("ID студента: ");
            int id = int.Parse(Console.ReadLine());

            var student = db.Students.Include(s => s.Passport).First(s => s.Id == id);

            Console.Write("Новое имя: ");
            student.Name = Console.ReadLine();

            Console.Write("Новый возраст: ");
            student.Age = int.Parse(Console.ReadLine());

            Console.Write("Новый email: ");
            student.Email = Console.ReadLine();

            Console.Write("Новая стипендия: ");
            student.Scholarship = decimal.Parse(Console.ReadLine());

            Console.Write("Новый формат обучения (0 - Очный, 1 - Заочный): ");
            student.StudyFormat = (StudyFormat)int.Parse(Console.ReadLine());

            Console.Write("Новый ID группы: ");
            student.GroupId = int.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        static void DeleteStudent(AppDbContext db)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            var student = db.Students.First(s => s.Id == id);
            db.Students.Remove(student);
            db.SaveChanges();
        }


        static void TeacherMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Преподаватели --");
                Console.WriteLine("1. Добавить преподавателя");
                Console.WriteLine("2. Просмотреть преподавателей");
                Console.WriteLine("3. Обновить преподавателя");
                Console.WriteLine("4. Удалить преподавателя");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTeacher(db);
                        break;
                    case "2":
                        ShowTeachers(db);
                        break;
                    case "3":
                        UpdateTeacher(db);
                        break;
                    case "4":
                        DeleteTeacher(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddTeacher(AppDbContext db)
        {
            Teacher teacher = new Teacher();
            Console.Write("ФИО: ");
            teacher.FullName = Console.ReadLine();

            Console.Write("Возраст: ");
            teacher.Age = int.Parse(Console.ReadLine());

            Console.Write("Зарплата: ");
            teacher.Salary = float.Parse(Console.ReadLine());


            db.Teachers.Add(teacher);
            db.SaveChanges();
        }
        static void ShowTeachers(AppDbContext db)
        {
            List<Teacher> teachers = db.Teachers.ToList();

            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher);
            }
        }
        static void UpdateTeacher(AppDbContext db)
        {
            Console.Write("ID преподавателя для изменения: ");
            int id = int.Parse(Console.ReadLine());

            Teacher teacher = db.Teachers.FirstOrDefault(t => t.Id == id);

            if (teacher == null) return;

            Console.Write("Новое ФИО: ");
            teacher.FullName = Console.ReadLine();

            Console.Write("Новый возраст: ");
            teacher.Age = int.Parse(Console.ReadLine());

            Console.Write("Новая зарплата: ");
            teacher.Salary = float.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        static void DeleteTeacher(AppDbContext db)
        {
            Console.Write("ID преподавателя для удаления: ");
            int id = int.Parse(Console.ReadLine());

            Teacher teacher = db.Teachers.FirstOrDefault(t => t.Id == id);

            if (teacher == null) return;

            db.Teachers.Remove(teacher);
            db.SaveChanges();
        }



        static void DepartmentMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Кафедры --");
                Console.WriteLine("1. Добавить кафедру");
                Console.WriteLine("2. Просмотреть кафедры");
                Console.WriteLine("3. Обновить кафедру");
                Console.WriteLine("4. Удалить кафедру");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDepartment(db);
                        break;
                    case "2":
                        ShowDepartments(db);
                        break;
                    case "3":
                        UpdateDepartment(db);
                        break;
                    case "4":
                        DeleteDepartment(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddDepartment(AppDbContext db)
        {
            Department department = new Department();

            Console.Write("Название: ");
            department.Name = Console.ReadLine();

            Console.Write("Описание: ");
            department.Description = Console.ReadLine();

            Console.Write("Финансирование: ");
            department.Financing = int.Parse(Console.ReadLine());

            db.Departments.Add(department);
            db.SaveChanges();
        }
        static void ShowDepartments(AppDbContext db)
        {
            List<Department> departments = db.Departments.ToList();

            foreach (Department department in departments)
            {
                Console.WriteLine(department);
            }
        }
        static void UpdateDepartment(AppDbContext db)
        {
            Console.Write("ID кафедры для изменения: ");
            int id = int.Parse(Console.ReadLine());

            Department department = db.Departments.FirstOrDefault(d => d.Id == id);

            if (department == null) return;

            Console.Write("Новое название: ");
            department.Name = Console.ReadLine();

            Console.Write("Новое описание: ");
            department.Description = Console.ReadLine();

            Console.Write("Новое финансирование: ");
            department.Financing = int.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        static void DeleteDepartment(AppDbContext db)
        {
            Console.Write("ID кафедры для удаления: ");
            int id = int.Parse(Console.ReadLine());

            Department department = db.Departments.FirstOrDefault(d => d.Id == id);

            if (department == null) return;

            db.Departments.Remove(department);
            db.SaveChanges();
        }



        static void SubjectMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Предметы --");
                Console.WriteLine("1. Добавить предмет");
                Console.WriteLine("2. Просмотреть предметы");
                Console.WriteLine("3. Обновить предмет");
                Console.WriteLine("4. Удалить предмет");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSubject(db);
                        break;
                    case "2":
                        ShowSubjects(db);
                        break;
                    case "3":
                        UpdateSubject(db);
                        break;
                    case "4":
                        DeleteSubject(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddSubject(AppDbContext db)
        {
            Subject subject = new Subject();

            Console.Write("Название: ");
            subject.Name = Console.ReadLine();

            Console.Write("Описание: ");
            subject.Description = Console.ReadLine();

            Console.Write("Количество часов: ");
            subject.Time = float.Parse(Console.ReadLine());

            Console.WriteLine("Выберите ID кафедры:");
            foreach (var department in db.Departments)
            {
                Console.WriteLine($"{department.Id}. {department.Name}");
            }

            Console.Write("ID кафедры: ");
            subject.DepartmentId = int.Parse(Console.ReadLine());

            db.Subjects.Add(subject);
            db.SaveChanges();
        }
        static void ShowSubjects(AppDbContext db)
        {
            List<Subject> subjects = db.Subjects.ToList();
            foreach (Subject subject in subjects)
            {
                Console.WriteLine(subject);
            }
        }
        static void UpdateSubject(AppDbContext db)
        {
            Console.Write("ID предмета для изменения: ");
            int id = int.Parse(Console.ReadLine());

            Subject subject = db.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null) return;

            Console.Write("Новое название: ");
            subject.Name = Console.ReadLine();

            Console.Write("Новое описание: ");
            subject.Description = Console.ReadLine();

            Console.Write("Новое количество часов: ");
            subject.Time = float.Parse(Console.ReadLine());

            Console.WriteLine("Выберите новую кафедру:");
            foreach (var department in db.Departments)
            {
                Console.WriteLine($"{department.Id}. {department.Name}");
            }

            Console.Write("ID кафедры: ");
            subject.DepartmentId = int.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        static void DeleteSubject(AppDbContext db)
        {
            Console.Write("ID предмета для удаления: ");
            int id = int.Parse(Console.ReadLine());

            Subject subject = db.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null) return;

            db.Subjects.Remove(subject);
            db.SaveChanges();
        }


        static void PassportMenu(AppDbContext db)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n-- Паспорта --");
                Console.WriteLine("1. Добавить паспорт");
                Console.WriteLine("2. Просмотреть паспорта");
                Console.WriteLine("3. Обновить паспорт");
                Console.WriteLine("4. Удалить паспорт");
                Console.WriteLine("0. Назад");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPassport(db);
                        break;
                    case "2":
                        ShowPassports(db);
                        break;
                    case "3":
                        UpdatePassport(db);
                        break;
                    case "4":
                        DeletePassport(db);
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void AddPassport(AppDbContext db)
        {
            Passport passport = new Passport();

            Console.Write("Номер паспорта: ");
            passport.Number = Console.ReadLine();

            Console.WriteLine("Выберите ID студента:");
            foreach (var student in db.Students)
            {
                Console.WriteLine($"{student.Id}. {student.Name}");
            }

            Console.Write("ID студента: ");
            passport.StudentId = int.Parse(Console.ReadLine());

            db.Passports.Add(passport);
            db.SaveChanges();
        }
        static void ShowPassports(AppDbContext db)
        {
            List<Passport> passports = db.Passports.Include(p => p.Student).ToList();
            foreach (var passport in passports)
            {
                Console.WriteLine($"ID: {passport.Id}, Номер: {passport.Number}, Студент: {passport.Student.Name}");
            }
        }
        static void UpdatePassport(AppDbContext db)
        {
            Console.Write("ID паспорта для изменения: ");
            int id = int.Parse(Console.ReadLine());

            Passport passport = db.Passports.FirstOrDefault(p => p.Id == id);
            if (passport == null) return;

            Console.Write("Новый номер паспорта: ");
            passport.Number = Console.ReadLine();

            Console.WriteLine("Выберите нового студента:");
            foreach (var student in db.Students)
            {
                Console.WriteLine($"{student.Id}. {student.Name}");
            }

            Console.Write("ID студента: ");
            passport.StudentId = int.Parse(Console.ReadLine());

            db.SaveChanges();
        }
        static void DeletePassport(AppDbContext db)
        {
            Console.Write("ID паспорта для удаления: ");
            int id = int.Parse(Console.ReadLine());

            Passport passport = db.Passports.FirstOrDefault(p => p.Id == id);
            if (passport == null) return;

            db.Passports.Remove(passport);
            db.SaveChanges();
        }
    }
}
