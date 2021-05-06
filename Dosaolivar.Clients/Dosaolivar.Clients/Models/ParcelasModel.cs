using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartHotel.Clients.Core.Models
{
    public class ParcelasModel : INotifyPropertyChanged
    {
        public string id { get; set; }

        public string nombre { get; set; }

        public string area { get; set; }

        public string area_metros { get; set; }

        public string area_decimales { get; set; }

        public string username { get; set; }

        public string cp { get; set; }

        public string localidad { get; set; }

        public string provincia { get; set; }

        public string agregado { get; set; }

        public string zona { get; set; }

        public string poligono { get; set; }

        public string parcela { get; set; }

        public string recinto { get; set; }

        public string a_metros { get; set; }

        public string a_decimales { get; set; }

        public string s_metros { get; set; }

        public string s_decimales { get; set; }


        public string densidad_hojas_id { get; set; }

        public string sistema_cultivo_id { get; set; }

        public string marco_cultivo_id { get; set; }

        public string copa_h { get; set; }

        public string copa_d1 { get; set; }

        public string copa_d2 { get; set; }

        public string copa_vca { get; set; }

        public string altura_media_copa { get; set; }

        public string diametro_media_copa { get; set; }

        public string getNombreArea() => $"{nombre}, {localidad}";

        public string getAnchuraCalle() => $"{a_metros},{a_decimales}";
        public string getSeparacionArboles() => $"{s_metros},{s_decimales}";

        public override string ToString() => $"{nombre}, {localidad}";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}