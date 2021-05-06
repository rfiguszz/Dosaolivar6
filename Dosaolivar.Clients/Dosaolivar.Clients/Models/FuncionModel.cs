using System.ComponentModel;

namespace SmartHotel.Clients.Core.Models
{
    public class FuncionModel : INotifyPropertyChanged
    {
        public string des { get; set; }

        public string value { get; set; }


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
