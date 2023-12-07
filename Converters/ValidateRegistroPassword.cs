using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Converters
{
    public class ValidateRegistroPassword : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string password;
            bool bTieneCaracteresEspeciales = false;
            try
            {
                if (value == null) { return true; }

                password = (string)value;
                for (int i = 0; i <password.Length; i++)
                {
                    if (!char.IsLetterOrDigit(password[i]) && !char.IsWhiteSpace(password[i]))
                    {
                        bTieneCaracteresEspeciales = true;
                        break;
                    }
                }
                if (password.Length < 12 || !password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !bTieneCaracteresEspeciales)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ValidateRegistroPassword.cs >> Convert: " + ex.Message);
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
