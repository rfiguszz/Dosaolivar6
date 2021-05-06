﻿namespace SmartHotel.Clients.Core.Services.IoT
{
    public static class FakeRoomAmbientLight
    {
        private static readonly SensorValue FakeDesiredValue = new SensorValue(65, SensorType.DimmerSwitch);

        public static RoomAmbientLight Create()
        {
            return new RoomAmbientLight(FakeDesiredValue);
        }
    }
}