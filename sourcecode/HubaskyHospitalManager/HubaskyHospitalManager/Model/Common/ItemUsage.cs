using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Common
{
    class ItemUsage
    {
        [key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public ItemUsage()
        {

        }

        public ItemUsage(int id, int itemId, int quantity)
        {
            Id = id;
            ItemId = itemId;
            Quantity = quantity;
        }

    }
}
