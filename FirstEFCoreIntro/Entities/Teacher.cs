using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEFCoreIntro.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public override string ToString()
        {
            return $"{Id}. {FullName} {Age} років. Заробітна плата становить {Salary} UAN. Викладач";
        }
    }
}
