﻿using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    public class SelectedRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return RoleConverter.RoleToString(((Role)value).ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //string roleName = (string)value;
            return RoleConverter.StringToRole((string)value);
        }
    }
}
