using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEFCoreIntro.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Time { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Teacher> Teachers { get; set; }
        
        public override string ToString()
        {
            return $"{Id}. {Name}, опис: {Description}. Кількість часу(у годинах) {Time}";
        }
    }
}
