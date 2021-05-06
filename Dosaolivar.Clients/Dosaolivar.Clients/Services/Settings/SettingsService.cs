﻿using SmartHotel.Clients.Core.Models;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.Services.Settings
{
    public class SettingsService : BaseSettingsLoader<RemoteSettings>, ISettingsService<RemoteSettings>
    {
        public string RemoteFileUrl { get => AppSettings.SettingsFileUrl; set => AppSettings.SettingsFileUrl = value; }

        public Task<RemoteSettings> LoadSettingsAsync()
        {
            var settings = new RemoteSettings();
            settings.Urls.Bookings = AppSettings.BookingEndpoint;
            settings.Urls.Hotels = AppSettings.HotelsEndpoint;
            settings.Urls.Suggestions = AppSettings.SuggestionsEndpoint;
            settings.Urls.Notifications = AppSettings.NotificationsEndpoint;
            settings.Urls.ImagesBaseUri = AppSettings.ImagesBaseUri;
            settings.Urls.RoomDevicesEndpoint = AppSettings.RoomDevicesEndpoint;
            settings.Tokens.Bingmaps = AppSettings.BingMapsApiKey;
         
            settings.Analytics.Android = AppSettings.AppCenterAnalyticsAndroid;
            settings.Analytics.Ios = AppSettings.AppCenterAnalyticsIos;
            settings.Analytics.Uwp = AppSettings.AppCenterAnalyticsWindows;
            settings.Others.FallbackMapsLocation = AppSettings.FallbackMapsLocation;
            settings.Bot.SkypeId = AppSettings.SkypeBotId;
	        settings.RoomDevices.RoomId = AppSettings.RoomId;

            return Task.FromResult(settings);
        }

        public Task PersistRemoteSettingsAsync(RemoteSettings remote)
        {
            AppSettings.BookingEndpoint = remote.Urls.Bookings;
            AppSettings.HotelsEndpoint = remote.Urls.Hotels;
            AppSettings.SuggestionsEndpoint = remote.Urls.Suggestions;
            AppSettings.NotificationsEndpoint = remote.Urls.Notifications;
            AppSettings.ImagesBaseUri = remote.Urls.ImagesBaseUri;
            AppSettings.RoomDevicesEndpoint = remote.Urls.RoomDevicesEndpoint;
            AppSettings.BingMapsApiKey = remote.Tokens.Bingmaps;
           
            AppSettings.AppCenterAnalyticsAndroid = remote.Analytics.Android;
            AppSettings.AppCenterAnalyticsIos = remote.Analytics.Ios;
            AppSettings.AppCenterAnalyticsWindows = remote.Analytics.Uwp;
            AppSettings.FallbackMapsLocation = remote.Others.FallbackMapsLocation;
            AppSettings.SkypeBotId = remote.Bot.SkypeId;
	        AppSettings.RoomId = remote.RoomDevices.RoomId;

            return Task.FromResult(false);
        }
    }
}
