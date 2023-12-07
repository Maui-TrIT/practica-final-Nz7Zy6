using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Converters
{
    public class PrecioParaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal precio;
            try
            {
                precio = (decimal)value;
                if (precio >= 0 && precio <= 100) return Colors.Green;
                else return Colors.Red;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en PrecioParaColorConverter.cs >> Convert: " + ex.Message);
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
