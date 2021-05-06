using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartHotel.Clients.Core.Models
{
    public class Provincias
    {

        public string JobsId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class RootModel : INotifyPropertyChanged
    {

        List<Provincias> jobList;
        public List<Provincias> JobList
        {
            get { return jobList; }
            set
            {
                if (jobList != value)
                {
                    jobList = value;
                    OnPropertyChanged();
                }
            }
        }

        Provincias selectedProvincia;
        public Provincias ProvinciaSeleccionada
        {
            get { return selectedProvincia; }
            set
            {
                if (selectedProvincia != value)
                {
                    selectedProvincia = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}