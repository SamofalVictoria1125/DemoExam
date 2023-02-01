using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoExam.AttributeT;

namespace DemoExam.Models
{
    
        [Table("отгрузки")]
        public class Shipment : Base
        {

            [Column("ShipmentDate")]
            public DateTime ShipmentDate { get; set; }

            [Column("IDManager")]
            public int IDManager { get; set; }
        }
    
}
