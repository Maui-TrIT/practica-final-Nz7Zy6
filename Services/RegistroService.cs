using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Backend.Login;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Services
{
    public class RegistroService
    {
        private HttpClient _httpClient;
        private Models.Config.Settings _settings;

        public RegistroService(HttpClient client, IConfiguration configuration)
        {
            try
            {
                _httpClient = client;
                _settings = configuration.GetRequiredSection(nameof(Models.Config.Settings)).Get<Models.Config.Settings>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en RegistroService.cs >> RegistroService: " + ex.Message);
            }
        }

        public async Task<bool> Registro(Models.Backend.Registro.RegistrarRequest registrarRequest)
        {
            try
            {
                var url = $"{_settings.UrlBase}/api/usuario/registrar";

                var json = JsonConvert.SerializeObject(registrarRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode) return false;

                var jsonResultado = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<UsuarioResponse>(jsonResultado);

                Preferences.Set("accesstoken", resultado.Token);
                Preferences.Set("userid", resultado.Id);
                Preferences.Set("email", resultado.Email);
                Preferences.Set("nombre", $"{resultado.Nombre}  {resultado.Apellido}");
                Preferences.Set("telefono", resultado.Telefono);
                Preferences.Set("username", resultado.UserName);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en RegistroService.cs >> Registro: " + ex.Message);
                return false;
            }
        }

    }
}
