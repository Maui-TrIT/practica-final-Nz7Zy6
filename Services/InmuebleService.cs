using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Backend.Inmueble;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Services
{
    public class InmuebleService
    {

        private readonly HttpClient client;
        private Models.Config.Settings settings;

        public InmuebleService(HttpClient client, IConfiguration configuration)
        {
            try
            {
                this.client = client;
                this.settings = configuration.GetRequiredSection(nameof(Models.Config.Settings)).Get<Models.Config.Settings>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> InmuebleService: " + ex.Message);
            }
        }

        public async Task<List<CategoryResponse>> GetCategories()
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/category";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<CategoryResponse>>(resultado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetCategories: " + ex.Message);
            }

            return new List<CategoryResponse>();    
        }

        public async Task<List<InmuebleResponse>> GetInmueblesByCategory(int categoryId)
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/inmueble/category/{categoryId}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<InmuebleResponse>>(resultado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetInmueblesByCategory: " + ex.Message);
            }
            return new List<InmuebleResponse>();
        }

        public async Task<List<InmuebleResponse>> GetInmueblesFavoritos()
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/inmueble/trending";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<InmuebleResponse>>(resultado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetInmueblesFavoritos: " + ex.Message);
            }

            return new List<InmuebleResponse>();
        }

        public async Task<InmuebleResponse> GetInmubleById(int inmuebleId)
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/inmueble/{inmuebleId}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<InmuebleResponse>(resultado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetInmubleById: " + ex.Message);
            }

            return new InmuebleResponse();
        }

        public async Task<bool> SaveBookmark(BookmarkRequest bookmark)
        {
            try
            {
                var url = $"{settings.UrlBase}/api/bookmark";
                var json = JsonConvert.SerializeObject(bookmark);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));


                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode) return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> SaveBookmark: " + ex.Message);
                return false;
            }

            return true;
        }

        public async Task<List<InmuebleResponse>> GetBookmarks()
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/inmueble/bookmark";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<InmuebleResponse>>(resultado);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetBookmarks: " + ex.Message);
            }

            return new List<InmuebleResponse>();
        }


        public async Task<List<InmuebleResponse>> GetBusquedaInmuebles(string inmuebleValue)
        {
            try
            {
                var uri = $"{settings.UrlBase}/api/inmueble/search/{inmuebleValue}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

                var resultado = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<InmuebleResponse>>(resultado);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleService.cs >> GetBusquedaInmuebles: " + ex.Message);
            }

            return new List<InmuebleResponse>();
        }

    }
}
