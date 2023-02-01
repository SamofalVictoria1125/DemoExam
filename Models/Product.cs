using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoExam.AttributeT;

namespace DemoExam.Models
{
    [Table("товары")]
    public class Product : Base
    {

        
        [Column("ProductName")]
        public string ProductName { get; set; }
    
        [Column("Description")]
        public string Description { get; set; }
      
        [Column("Category")]
        public string Category { get; set; }
    }

}

