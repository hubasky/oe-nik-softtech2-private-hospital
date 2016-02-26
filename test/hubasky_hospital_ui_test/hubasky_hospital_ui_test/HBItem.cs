using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hubasky_hospital_ui_test
{
    public class HBItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Quantity { get; set; }
        public string Dimension { get; set; }

        public HBItem(string id, string name, string dept, string qnt, string dim)
        {
            Id = id;
            Name = name;
            Dept = dept;
            Quantity = qnt;
            Dimension = dim;
        }
    }
}
