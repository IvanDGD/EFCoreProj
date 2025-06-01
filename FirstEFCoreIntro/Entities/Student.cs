using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEFCoreIntro.Entities
{
    public enum StudyFormat
    {
        FullTime, PartTime, Online, Hybrid
    }
    [PrimaryKey("Id")]
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }

        public string Email { get; set; } = null!;
        public decimal? Scholarship { get; set; }

        public StudyFormat StudyFormat { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Passport Passport { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id}. {Name} {Age} років. Стипендія {Scholarship}. Группа {GroupId}. Студент";
        }
    }
}
