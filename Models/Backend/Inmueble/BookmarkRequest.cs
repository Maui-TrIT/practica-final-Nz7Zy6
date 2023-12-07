using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models.Backend.Inmueble
{
    public class BookmarkRequest
    {
        public string UsuarioId { get; set; }
        public int InmuebleId { get; set; }
    }
}
