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
        public int Quantity { get; set; }

        public ItemUsage()
        {
        }

        public ItemUsage(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }


        public object Clone()
        {
            Object clone = new ItemUsage(ItemId, Quantity);

            return clone;            
        }
    }
}
