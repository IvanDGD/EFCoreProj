using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEFCoreIntro.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Financing { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name}, опис: {Description}. Фінансування {Financing}";
        }
    }
}
