using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartHotel.Clients.Core.Models
{
    public class BoquillasModel
    {
        [JsonProperty("boquillas_id")]
        public string id { get; set; }

        public string index { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public string codref { get; set; }

        public string submodelo { get; set; }

        public string color { get; set; }

        public string caudal { get; set; }

        public string pmax { get; set; }

        public string pmin { get; set; }

        public string pmaxrecomendada { get; set; }

        public string pminrecomendada { get; set; }

        public string angulochorro { get; set; }

        public string codfoto { get; set; }

        public string urlfoto { get; set; }

        public string getNombreCompleto() => $"{marca}, {modelo} {color}";

        public string URLImagen() => $"https://app.dosaolivar.es/boquillas/{codfoto}.jpg";

        public override string ToString() => $"{marca}, {modelo} {color}";

        public string getNombreBoquilla() 
        {
            string custom_nombre = "";
            string custom_modelo = modelo.Replace("ninguno"," ");
            string custom_color = color.Replace("ninguno", " ");
            return custom_nombre;
        }
    }
}