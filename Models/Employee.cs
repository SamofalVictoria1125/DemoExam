using DemoExam.AttributeT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam.Models
{
    [Table("сотрудники")]
    public class Employee : Base
    {

        [Column("IDContact")]
        public int IDContact { get; set; }

    }
}
