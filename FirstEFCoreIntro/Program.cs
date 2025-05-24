using FirstEFCoreIntro.Entities;

namespace FirstEFCoreIntro
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            AppDbContext db = new AppDbContext();
            int choose;
            do
            {
                Console.WriteLine("1. Створити студента");
                Console.WriteLine("2. Створити викладача");
                Console.WriteLine("3. Вивести студента");
                Console.WriteLine("4. Вивести викладача");
                Console.WriteLine("5. Вийти");

                choose = int.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        Student st = new Student();

                        Console.WriteLine("Введіть ім'я студента: ");
                        st.Name = Console.ReadLine();

                        Console.WriteLine("Введіть вік студента: ");
                        st.Age = int.Parse(Console.ReadLine());

                        db.Students.Add(st);
                        db.SaveChanges();
                        break;

                    case 2:
                        Teacher th = new Teacher();

                        Console.WriteLine("Введіть повне ім'я викладача: ");
                        th.FullName = Console.ReadLine();

                        Console.WriteLine("Введіть вік викладача: ");
                        th.Age = int.Parse(Console.ReadLine());

                        Console.WriteLine("Введіть заробітну плату викладача в UAN: ");
                        th.Salary = int.Parse(Console.ReadLine());

                        db.Teachers.Add(th);
                        db.SaveChanges();
                        break;

                    case 3:
                        foreach (var s in db.Students)
                        {
                            Console.WriteLine(s.ToString());
                        }
                        break;

                    case 4:
                        foreach (var t in db.Teachers)
                        {
                            Console.WriteLine(t.ToString());
                        }
                        break;

                    case 5:
                        Console.WriteLine("Вихід з програми.");
                        break;

                    default:
                        break;
                }

            } while (choose != 5);

        }
    }
    }
}
