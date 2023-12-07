using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models.Backend.Inmueble
{
    public class CategoryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string NombreCategory { get; set; }

        [JsonProperty("imageUrl")]
        public string ImagenUrl { get; set; }

    }
}
