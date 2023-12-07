using ShopApp.Services;
using System.Diagnostics;

namespace ShopApp
{
    public class DatabaseRutaService : IDatabaseRutaService
    {
        public string Get(string nombreArchivo)
        {
            try
            {
                var rutaDirectorio = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(rutaDirectorio, nombreArchivo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en DatabaseRutaService.cs >> Get: " + ex.Message);
                return "";
            }
        }
    }
}
