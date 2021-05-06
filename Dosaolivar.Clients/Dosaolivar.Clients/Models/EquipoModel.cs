namespace SmartHotel.Clients.Core.Models
{
    public class EquipoModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string nombre { get; set; } // PASA A SER MARCA
        public string fabricante { get; set; } // PASA A SER MODELO
        public string nbastidor { get; set; }
        public string capacidad { get; set; }

        // NUEVOS CAMPOS
        public string nroma { get; set; }
        public string nropo { get; set; }


        public string nboquillas { get; set; }

        public string boquilla1id { get; set; }
        public string boquilla1url { get; set; }
        public string boquilla1cantidad { get; set; }
        public string boquilla1des { get; set; }

        public string boquilla2id { get; set; }
        public string boquilla2url { get; set; }
        public string boquilla2cantidad { get; set; }
        public string boquilla2des { get; set; }


        public string boquilla3id { get; set; }
        public string boquilla3url { get; set; }
        public string boquilla3cantidad { get; set; }
        public string boquilla3des { get; set; }

        public string boquilla4id { get; set; }
        public string boquilla4url { get; set; }
        public string boquilla4cantidad { get; set; }
        public string boquilla4des { get; set; }


        public string getNombreCompleto() => $"{nombre}, {fabricante}";

        public override string ToString() => $"{nombre}, {fabricante}";
    }
}
