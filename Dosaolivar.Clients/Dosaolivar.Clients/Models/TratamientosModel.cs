using System.ComponentModel;
namespace SmartHotel.Clients.Core.Models
{
    public class TratamientosModel : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string username { get; set; }
        public string nombre { get; set; }
        public string velocidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string fecha_fin { get; set; }
        public string hora_fin { get; set; }
        public string presion { get; set; }
        public string parcela_id { get; set; }
        public string equipo_id { get; set; }
        public string parcela_nombre { get; set; }
        public string equipo_nombre { get; set; }
        public string const_qb { get; set; }
        public string const_k { get; set; }
        public string nboquillas { get; set; }
        public string volumen_caldo { get; set; }

        public string producto1id { get; set; }
        public string producto1des { get; set; }
        public string producto1cantidad { get; set; }
        public string producto2id { get; set; }
        public string producto2cantidad { get; set; }
        public string producto2des { get; set; }
        public string producto3id { get; set; }
        public string producto3cantidad { get; set; }
        public string producto3des { get; set; }

        public int producto_buscado { get; set; }

        // ACTIVOS
        public string materia1id { get; set; }
        public string materia1des { get; set; }
        public string materia2id { get; set; }
        public string materia2des { get; set; }
        public string materia3id { get; set; }
        public string materia3des { get; set; }


        // FUNCION
        public string funcion1id { get; set; }
        public string funcion1des { get; set; }
        public string funcion2id { get; set; }
        public string funcion2des { get; set; }
        public string funcion3id { get; set; }
        public string funcion3des { get; set; }

        public string getNombreTratamiento() => $"{nombre}, {fecha}";

        public string getFecha() => $"{fecha} 12:00:00 AM";

        public string getNombreTratamientoCalendario() => $"{nombre}/{parcela_nombre}/{equipo_nombre}";

        public override string ToString() => $"{nombre}, {fecha}";


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
