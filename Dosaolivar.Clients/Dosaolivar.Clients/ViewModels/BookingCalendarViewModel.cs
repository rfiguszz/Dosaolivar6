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

namespace SmartHotel.Clients.Core.ViewModels
{
    public class BookingCalendarViewModel : ViewModelBase
    {
        Models.City city;
        ObservableRangeCollection<DateTime> dates;
        DateTime from;
        DateTime until;
        bool isNextEnabled;

        public BookingCalendarViewModel()
        {
            Console.WriteLine("hola");
            var today = DateTime.Today;

            dates = new ObservableRangeCollection<DateTime>
            {
                today
            };
            attendances = new ObservableCollection<SpecialDate>();
            //Attendances = new ObservableCollection<SpecialDate>();
            SpecialDate evento1 = new SpecialDate(new DateTime(2019, 11, 1))
            {
                BackgroundColor = Color.Red
            };
            attendances.Add(evento1);
            Attendances.Add(evento1);
            Console.WriteLine("hola2");
            SelectedDate(today);
        }

        public Models.City City
        {
            get => city;
            set => SetProperty(ref city, value);
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

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                City = navigationData as Models.City;
            }

            return base.InitializeAsync(navigationData);
        }

        private ObservableCollection<SpecialDate> attendances;

        public ObservableCollection<SpecialDate> Attendances
        {
            get
            {
                return attendances;
            }
            set => SetProperty(ref attendances, value);
        }

        void SelectedDate(object date)
        {
            if (date == null)
                return;

            if (Dates.Any())
            {
                From = Dates.OrderBy(d => d.Day).FirstOrDefault();
                Until = Dates.OrderBy(d => d.Day).LastOrDefault();
                IsNextEnabled = Dates.Any() ? true : false;
            }
        }

        Task NextAsync()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "city", City },
                { "from", From },
                { "until", Until },
            };

            return NavigationService.NavigateToAsync<BookingHotelsViewModel>(navigationParameter);
        }
    }
}