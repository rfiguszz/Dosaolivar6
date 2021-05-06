using MvvmHelpers;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SmartHotel.Clients.Core.Controls;
using System.Collections.ObjectModel;
using SmartHotel.Clients.Core.Services.MisParcelas;
using SmartHotel.Clients.Core.Models;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class CalendarioTratamientosViewModel : ViewModelBase
    {
        ObservableRangeCollection<DateTime> dates;
        readonly IParcelasService parcelasService;
        DateTime from;
        DateTime until;
        bool isNextEnabled;
        // DOSAOLIVAR


        // ----
        string search;
        IEnumerable<string> suggestions;
        string suggestion;
        public string Search
        {
            get => search;
            set
            {
                search = value;
               // FilterAsync(search);
                OnPropertyChanged();
            }
        }

        public IEnumerable<string> Suggestions
        {
            get => suggestions;
            set => SetProperty(ref suggestions, value);
        }

        public string Suggestion
        {
            get => suggestion;
            set
            {
                suggestion = value;

                IsNextEnabled = string.IsNullOrEmpty(suggestion) ? false : true;

             //   dismissKeyboardService.DismissKeyboard();

                OnPropertyChanged();
            }
        }

        public string getaccioncalendario;
        public string GetAccionCalendario
        {
            get => getaccioncalendario;
            set 
            {
                getaccioncalendario = value;

                OnPropertyChanged();
            }
        }

        IEnumerable<TratamientosModel> tratamientos;
        private ObservableCollection<SpecialDate> attendances;
        public ObservableCollection<SpecialDate> Attendances
        {
            get
            {
                return attendances;
            }
            set => SetProperty(ref attendances, value);
        }



        public CalendarioTratamientosViewModel()
        {
            Console.WriteLine("CalendarioTratamientosViewModel(");
            tratamientos = AppSettings.TratamientosStatic;
            suggestions = new List<string>();
            getaccioncalendario = "Seleccione una fecha";
           // Suggestions = new List<string>(tratamientos.Select(c => c.ToString()));
            //this.parcelasService = parcelasService;
            var today = DateTime.Today;

            dates = new ObservableRangeCollection<DateTime>
            {
                today
            };  

            attendances = new ObservableCollection<SpecialDate>();
            SpecialDate evento;
            foreach (var o in tratamientos)
            {
                DateTime date = DateTime.Parse(o.fecha);
                evento = new SpecialDate(date)
                {
                    BackgroundColor = Color.Green,
                    Selectable = true

                };
                Attendances.Add(evento);
                Console.WriteLine(o.fecha);
            }

           /* attendances.Add(evento1);
            Attendances.Add(evento1);
            Attendances.Add(evento2);*/
            SelectedDate(today);
        }

        public override async Task InitializeAsync(object navigationData)
        {
           /* tratamientos = await parcelasService.GetAllTratamientosAsync();
            if (navigationData != null)
            {
               // City = navigationData as Models.City;
            }
            SpecialDate evento1 = new SpecialDate(new DateTime(2019, 11, 3))
            {
                BackgroundColor = Color.Green
            };
            attendances.Add(evento1);
            Attendances.Add(evento1);

            attendances = new ObservableCollection<SpecialDate>();
            SpecialDate evento2 = new SpecialDate(new DateTime(2019, 11, 5))
            {
                BackgroundColor = Color.Green
            };
            attendances.Add(evento2);
            Attendances.Add(evento2);
            OnPropertyChanged("Attendances");
            OnPropertyChanged("attendances");
            OnPropertyChanged("SpecialDates"); */
            //return base.InitializeAsync(navigationData);
        }


        

        public ObservableRangeCollection<DateTime> Dates
        {
            get => dates;
            set => SetProperty(ref dates, value);
        }

        public DateTime From
        {
            get => from;
            set => SetProperty(ref from, value);
        }

        public DateTime Until
        {
            get => until;
            set => SetProperty(ref until, value);
        }

        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ICommand SelectedDateCommand => new Command((d) => SelectedDate(d));

        public ICommand NextCommand => new AsyncCommand(NextAsync);

       
       

        void SelectedDate(object date)
        {
            Console.WriteLine("hola " + date.ToString());
            if (date == null)
                return;
            Suggestions = new List<string>();
            foreach (var v in tratamientos)
            {

                Console.WriteLine(v.getFecha());
                if (date.ToString() == v.getFecha().Replace("-","/"))
                {
                    GetAccionCalendario = "Seleccione Tratamiento";
                    Suggestions = new List<string>(new string[] { $"{v.nombre}/{v.parcela_nombre}/{v.equipo_nombre}"  });
                }
            }


            /*
            if (Dates.Any())
            {
                From = Dates.OrderBy(d => d.Day).FirstOrDefault();
                Until = Dates.OrderBy(d => d.Day).LastOrDefault();
                IsNextEnabled = Dates.Any() ? true : false;
            }*/
        }

        async Task NextAsync()
        {
            Console.WriteLine("tra botoncito");
            var tratamientosel = tratamientos.FirstOrDefault(c => c.getNombreTratamientoCalendario().Equals(Suggestion));
            if(tratamientosel != null)
            {
                await NavigationService.NavigateToAsync<DetalleTratamientoViewModel>(tratamientosel);
            }
            // just return Task, but have to provide an argument because there is no overload
        }

    }
}