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
    public class SecurityService
    {
        private HttpClient client;
        private Models.Config.Settings settings;
        public SecurityService(HttpClient client, IConfiguration configuration)
        {
            try
            {
                this.client = client;
                settings = configuration.GetRequiredSection(nameof(Models.Config.Settings)).Get<Models.Config.Settings>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en SecurityService.cs >> SecurityService: " + ex.Message);
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var url = $"{settings.UrlBase}/api/usuario/login";
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

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
                Debug.WriteLine("Excepción en SecurityService.cs >> Login: " + ex.Message);
                return false;
            }
        }
    }
}
