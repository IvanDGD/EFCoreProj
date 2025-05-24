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
            //#region Створення студента
            //Student st = new Student();

            //Console.WriteLine("Введіть ім'я студента: ");
            //st.Name = Console.ReadLine();

            //Console.WriteLine("Введіть вік студента: ");
            //st.Age = int.Parse(Console.ReadLine());

            //db.Students.Add(st);
            //db.SaveChanges();
            //#endregion
            //#region Створення викладача
            //Teacher th = new Teacher();

            //Console.WriteLine("Введіть повне ім'я викладача: ");
            //th.FullName = Console.ReadLine();

            //Console.WriteLine("Введіть вік викладача: ");
            //th.Age = int.Parse(Console.ReadLine());

            //Console.WriteLine("Введіть заробітну плату викладача в UAN: ");
            //th.Salary = int.Parse(Console.ReadLine());

            //db.Teachers.Add(th);
            //db.SaveChanges();
            //#endregion
            foreach (var s in db.Students)
            {
                Console.WriteLine(s.ToString());
            }

            foreach (var t in db.Teachers)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
