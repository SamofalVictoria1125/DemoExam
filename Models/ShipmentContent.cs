using DemoExam.AttributeT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExam.Models
{

    [Table("состав отгрузок")]
    public class ShipmentContent : Base
    {


        [Column("IDShipment")]
        public int IDShipment { get; set; }

        [Column("IDProduct")]
        public int IDProduct { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Volume")]
        public double Volume { get; set; }

        [Column("Weight")]
        public double Weight { get; set; }

        [Column("Sum")]
        public double Sum { get; set; }
    }
}
