using Microsoft.Extensions.Configuration;
using ShopApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Services
{
    public class CompraService
    {
        private HttpClient client;
        private Models.Config.Settings settings;

        public CompraService(HttpClient client, IConfiguration configuration)
        {
            try
            {
                this.client = client;
                settings = configuration.GetRequiredSection(nameof(Models.Config.Settings)).Get<Models.Config.Settings>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en CompraService.cs >> CompraService: " + ex.Message);
            }
        }

        public async Task<bool> EnviarData(IEnumerable<Compra> compras)
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/compra";
                var body = new
                {
                    data = compras
                };

                var resultado = await client.PostAsJsonAsync(uri, body);
                return resultado.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en CompraService.cs >> EnviarData: " + ex.Message);
            }

            return false;
        }
    }
}
