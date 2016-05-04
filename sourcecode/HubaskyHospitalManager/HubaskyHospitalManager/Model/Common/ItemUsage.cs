using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Common
{
    public class ItemUsage : ICloneable 
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public InventoryUnit Unit { get; set; }

        public ItemUsage()
        {
        }

        public ItemUsage(int id, int itemid, string name, int quantity, InventoryUnit unit)
        {
            Id = id;
            ItemId = itemid;
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }

        public override string ToString()
        {
            return String.Format("{2} {3} {1}", Id, Name, Quantity, Unit);
        }


        public object Clone()
        {
            Object clone = new ItemUsage(Id, ItemId, Name, Quantity, Unit);

            return clone;
        }
    }
}
