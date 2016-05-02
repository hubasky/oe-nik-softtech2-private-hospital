using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HubaskyHospitalManager.View
{

    public class StringToBoolConverter : IValueConverter
    {   
        //pl. ha listában ki van jelölve valami, csak akkor enabled egy textbox

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            string keyword = value.ToString();
            //if (keyword.Equals(parameter.ToString(), StringComparison.CurrentCultureIgnoreCase))
            //    return true;
            if (!(keyword==null))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StringToVisibilityConverter : IValueConverter
    {
        //pl. ha listában ki van jelölve valami, csak akkor látható egy textbox

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            string keyword = value.ToString();

            if (!(keyword == null))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    //class phoneNrConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();

    //        //string taj = (string)value;

    //        //bool ervenyes = taj.Length == 9;
    //        //if (ervenyes)
    //        //{
    //        //    for (int i = 0; i < 9 && ervenyes; i++)
    //        //    {
    //        //        if (!char.IsDigit(taj[i]))
    //        //            ervenyes = false;
    //        //    }
    //        //}

    //        //if (ervenyes)
    //        //{
    //        //    return taj + " (érvényes)";
    //        //}
    //        //else
    //        //{
    //        //    return taj + " (nem érvényes)";
    //        //}



    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //class BoolToVis : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {

    //        if ((bool)value)
    //        {
    //            return Visibility.Visible;
    //        }
    //        return Visibility.Collapsed;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        if ((Visibility)value == Visibility.Visible)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}



    //public class enumBooleanConverter : IValueConverter
    //{

    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return value.Equals(parameter);
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return value.Equals(true) ? parameter : Binding.DoNothing;
    //    }
    //    //forrás: http://stackoverflow.com/questions/397556/how-to-bind-radiobuttons-to-an-enum

    //}

    //public class MarginConverter : IValueConverter
    //{

    //    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return new Thickness(System.Convert.ToDouble(value), 0, 0, 0);
    //    }

    //    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return null;
    //    }
    //}
}

