using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class Producto
    {
        public string nombre_plaga { get; set; }
        public string nombre_formulado { get; set; }
        public string nombre_comercial { get; set; }
        public string url_pdf { get; set; }
        public string empresa { get; set; }
        public string domicilio { get; set; }

        public override string ToString() => $"{nombre_comercial}";
    }
}
