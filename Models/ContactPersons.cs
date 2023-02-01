using DemoExam.AttributeT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam.Models
{

    [Table("контактные лица")]
    public class ContactPerson : Base
    {


        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Patronymic")]
        public string Patronymic { get; set; }

        [Column("Sex")]
        public string Sex { get; set; }
    }
}
