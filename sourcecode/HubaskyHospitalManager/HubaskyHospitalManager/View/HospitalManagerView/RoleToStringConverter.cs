using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    public class RoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] source = value as string[];
            string[] target = new string[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                switch (source[i])
                {
                    case "Doctor":
                        target[i] = "orvos";
                        break;
                    case "Nurse":
                        target[i] = "ápoló";
                        break;
                    case "Laboratorian":
                        target[i] = "labortechnikus";
                        break;
                    case "DataRecorder":
                        target[i] = "adatrögzítő";
                        break;
                    case "Administrator":
                        target[i] = "adminisztrátor";
                        break;
                }
            }
            return target;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
