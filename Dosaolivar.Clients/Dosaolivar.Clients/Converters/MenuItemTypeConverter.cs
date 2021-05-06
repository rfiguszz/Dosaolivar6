using SmartHotel.Clients.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Converters
{
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            var platform = Device.RuntimePlatform == Device.UWP;

            switch (menuItemType)
            {
                case MenuItemType.Home:
                    return platform ? "Assets/ic_home.png" : "ic_home.png";
                case MenuItemType.MisParcelas:
                    return platform ? "Assets/ic_bed.png" : "ic_elevator.png";
                case MenuItemType.MisTratamientos:
                    return platform ? "Assets/ic_key.png" : "ic_key.png";
                case MenuItemType.MisEquipos:
                    return platform ? "Assets/ic_bot.png" : "ic_bot.png";
                case MenuItemType.Logout:
                    return platform ? "Assets/ic_logout.png" : "ic_logout.png";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}