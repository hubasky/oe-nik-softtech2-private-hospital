using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Common
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public string File { get; set; }

        public Attachment()
        {
            File = "";
        }

        public Attachment(string path)
        {
            File = path;
        }
    }
}
