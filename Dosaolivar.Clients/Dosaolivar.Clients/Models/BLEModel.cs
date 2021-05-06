using System.ComponentModel;
namespace SmartHotel.Clients.Core.Models
{
    public class BLEModel : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string rssi { get; set; }

        public string getNombre() => $"{name}";
        public override string ToString() => $"{name}";

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
