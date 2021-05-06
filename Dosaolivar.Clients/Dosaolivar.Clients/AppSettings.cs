using Xamarin.Essentials;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Utils;
using System.Collections.Generic;

namespace SmartHotel.Clients.Core
{
	public static class AppSettings
    {
        //IF YOU DEPLOY YOUR OWN ENDPOINT REPLACE THE VALUEW BELOW

        //App Center
        const string defaultAppCenterAndroid = "b3b1403c-3f9d-4c77-805e-9c002de6ddf7";
        const string defaultAppCenteriOS = "7a2a290b-07b0-47dc-9dcd-15461e894e6d";
        const string defaultAppCenterUWP = "140a8550-c309-4bc1-a05d-e5a0f7e4df1d";


        // VERSION
        static string version = "0.18";

        public static string HostURL;
        static string apiURL;
        static string defaultAuthURL;
        static string defaultSignUpURL;
        static string defaultParcelasURL;
        static string defaultEquiposURL;
        static string defaultBoquillasURL;
        static string defaultProductosURL;
        static string defaultTratamientosURL;
        static string defaultLocalidadURL;


        static IEnumerable<TratamientosModel> static_tratamientos;

        // STATIC VARIABLES
        public static string sessionUsername;
        public static string sessionNick;
        public static EquipoModel sessionEquipo;
        public static TratamientosModel sessionTratamiento;


        // BORRAR
        static string defaultBookingEndpoint;
        static string defaultHotelsEndpoint;
        static string defaultSuggestionsEndpoint;
        static string defaultNotificationsEndpoint;
        static string defaultSettingsFileUrl;

        // Endpoints
        const string defaultImagesBaseUri = "https://sh360imgdev.blob.core.windows.net";
        const string defaultRoomDevicesEndpoint = "";

        // Maps
        const string defaultBingMapsApiKey = "AkSuJ-YtW4VDvIzErxK3ke2ILQD1muWwS2KN2QvhqHobx4YBEIYqkEVBLyx1LYby";
        public const string DefaultFallbackMapsLocation = "40.762246,-73.986943";

        // Bots
        const string defaultSkypeBotId = "87e0cdb5-8e79-4592-9dc8-11697ffe79cc";



        // Booking 
        const bool defaultHasBooking = false;

		// Room Devices
	    const string defaultRoomId = "";

        // Fakes
        const bool defaultUseFakes = false;



        static AppSettings()
        {
            HostURL = "https://dosaolivar.macross.is/panel";
            apiURL = "https://dosaolivar.macross.is/panel/services";
            defaultAuthURL = apiURL + "/auth/auth";
            defaultSignUpURL = apiURL + "/signupapp";
            defaultParcelasURL = apiURL + "/parcelasapp";
            defaultEquiposURL = apiURL + "/equiposapp";
            defaultTratamientosURL = apiURL + "/tratamientosapp";
            defaultBoquillasURL = apiURL + "/boquillasapp";
            defaultProductosURL = apiURL + "/productos";
            defaultLocalidadURL = apiURL + "/municipiosapp";
            static_tratamientos = new List<TratamientosModel>();
            sessionNick = "";


            // BORRAR 
            defaultBookingEndpoint = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/bookings";
            defaultHotelsEndpoint = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/hotels-api";
            defaultSuggestionsEndpoint = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/suggestions-api";
            defaultNotificationsEndpoint = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/notifications-api";
            defaultSettingsFileUrl = "http://sh360production.2c3abf6edd44497688b2.westus.aksapp.io/configuration-api/cfg/aks";
		}



        // VERSION

        public static IEnumerable<TratamientosModel> TratamientosStatic
        {
            get => static_tratamientos;
            set
            {
                static_tratamientos = value;
            }
        }

        public static string VersionAPP
        {
            get => Preferences.Get(nameof(VersionAPP), version);
            set => Preferences.Set(nameof(VersionAPP), value);
        }

        // API Endpoints

        public static string AuthURL
        {
            get => Preferences.Get(nameof(AuthURL), defaultAuthURL);
            set => Preferences.Set(nameof(AuthURL), value);
        }
               
        public static string SignUpURL
        {
            get => Preferences.Get(nameof(SignUpURL), defaultSignUpURL);
            set => Preferences.Set(nameof(SignUpURL), value);
        }


        public static string ParcelasEndpoint
        {
            get => Preferences.Get(nameof(ParcelasEndpoint), defaultParcelasURL);
            set => Preferences.Set(nameof(ParcelasEndpoint), value);
        }
        public static string EquiposEndpoint
        {
            get => Preferences.Get(nameof(EquiposEndpoint), defaultEquiposURL);
            set => Preferences.Set(nameof(EquiposEndpoint), value);
        }
        public static string TratamientosEndpoint
        {
            get => Preferences.Get(nameof(TratamientosEndpoint), defaultTratamientosURL);
            set => Preferences.Set(nameof(TratamientosEndpoint), value);
        }
        public static string BoquillasEndpoint
        {
            get => Preferences.Get(nameof(BoquillasEndpoint), defaultBoquillasURL);
            set => Preferences.Set(nameof(BoquillasEndpoint), value);
        }
        public static string ProductosEndpoint
        {
            get => Preferences.Get(nameof(ProductosEndpoint), defaultProductosURL);
            set => Preferences.Set(nameof(ProductosEndpoint), value);
        }

        public static string LocalidadEndpoint
        {
            get => Preferences.Get(nameof(LocalidadEndpoint), defaultLocalidadURL);
            set => Preferences.Set(nameof(LocalidadEndpoint), value);
        }

        /*
         * 
         * 
         *         
         * BORRAR
         * 
         * 
         */




        // API Endpoints
        public static string BookingEndpoint
        {
            get => Preferences.Get(nameof(BookingEndpoint), defaultBookingEndpoint);
            set => Preferences.Set(nameof(BookingEndpoint), value);
        }

        public static string HotelsEndpoint
        {
            get => Preferences.Get(nameof(HotelsEndpoint), defaultHotelsEndpoint);
            set => Preferences.Set(nameof(HotelsEndpoint), value);
        }

        public static string SuggestionsEndpoint
        {
            get => Preferences.Get(nameof(SuggestionsEndpoint), defaultSuggestionsEndpoint);
            set => Preferences.Set(nameof(SuggestionsEndpoint), value);
        }

        public static string NotificationsEndpoint
        {
            get => Preferences.Get(nameof(NotificationsEndpoint), defaultNotificationsEndpoint);
            set => Preferences.Set(nameof(NotificationsEndpoint), value);
        }

        public static string ImagesBaseUri
        {
            get => Preferences.Get(nameof(ImagesBaseUri), defaultImagesBaseUri);
            set => Preferences.Set(nameof(ImagesBaseUri), value);
        }

        public static string RoomDevicesEndpoint
        {
            get => Preferences.Get(nameof(RoomDevicesEndpoint), defaultRoomDevicesEndpoint);
            set => Preferences.Set(nameof(RoomDevicesEndpoint), value);
        }

        public static string SkypeBotId
        {
            get => Preferences.Get(nameof(SkypeBotId), defaultSkypeBotId);
            set => Preferences.Set(nameof(SkypeBotId), value);
        }

        // Other settings

        public static string BingMapsApiKey
        {
            get => Preferences.Get(nameof(BingMapsApiKey), defaultBingMapsApiKey);
            set => Preferences.Set(nameof(BingMapsApiKey), value);
        }

        public static string SettingsFileUrl
        {
            get => Preferences.Get(nameof(SettingsFileUrl), defaultSettingsFileUrl);
            set => Preferences.Set(nameof(SettingsFileUrl), value);
        }

        public static string FallbackMapsLocation
        {
            get => Preferences.Get(nameof(FallbackMapsLocation), DefaultFallbackMapsLocation);
            set => Preferences.Set(nameof(FallbackMapsLocation), value);
        }

        public static User User
        {
            get => PreferencesHelpers.Get(nameof(User), default(User));
            set => PreferencesHelpers.Set(nameof(User), value);
        }

        public static string AppCenterAnalyticsAndroid
        {
            get => Preferences.Get(nameof(AppCenterAnalyticsAndroid), defaultAppCenterAndroid);
            set => Preferences.Set(nameof(AppCenterAnalyticsAndroid), value);
        }

        public static string AppCenterAnalyticsIos
        {
            get => Preferences.Get(nameof(AppCenterAnalyticsIos), defaultAppCenteriOS);
            set => Preferences.Set(nameof(AppCenterAnalyticsIos), value);
        }

        public static string AppCenterAnalyticsWindows
        {
            get => Preferences.Get(nameof(AppCenterAnalyticsWindows), defaultAppCenterUWP);
            set => Preferences.Set(nameof(AppCenterAnalyticsWindows), value);
        }

        public static bool UseFakes
        {
            get => Preferences.Get(nameof(UseFakes), defaultUseFakes);
            set => Preferences.Set(nameof(UseFakes), value);
        }

        public static bool HasBooking
        {
            get => Preferences.Get(nameof(HasBooking), defaultHasBooking);
            set => Preferences.Set(nameof(HasBooking), value);
        }

        public static void RemoveUserData() => Preferences.Remove(nameof(User));
		
		public static string RoomId
	    {
		    get => Preferences.Get(nameof(RoomId), defaultRoomId);
		    set => Preferences.Set(nameof(RoomId), value);
	    }
    }
}
