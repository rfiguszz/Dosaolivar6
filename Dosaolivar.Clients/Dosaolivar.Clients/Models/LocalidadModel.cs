using System;
using System.Collections.Generic;

namespace SmartHotel.Clients.Core.Models
{
    public class LocalidadModel
    {
        public string id { get; set; }

        public string localidad { get; set; }

        public string provincia { get; set; }

        public string cp { get; set; }

        public string getNombreCompleto() => $"{localidad}, {provincia} ";

    }
}