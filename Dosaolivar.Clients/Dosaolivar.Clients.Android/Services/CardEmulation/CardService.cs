﻿using System;
using System.Linq;
using System.Text;
using Android.OS;
using Android.Nfc.CardEmulators;
using SmartHotel.Clients.Core;
using Xamarin.Forms;
using Android.App;

namespace SmartHotel.Clients.Droid.Services.CardEmulation
{
    [Service(
        Exported = true, 
        Enabled = true, 
        Name = ServiceName,
        Permission = "android.permission.BIND_NFC_SERVICE"),
        IntentFilter(new[] { "android.nfc.cardemulation.action.HOST_APDU_SERVICE" }, 
        Categories = new[] { "android.intent.category.DEFAULT" }),
        MetaData("android.nfc.cardemulation.host_apdu_service",
        Resource = "@xml/apduservice")]
    public class CardService : HostApduService
    {
        public const string ServiceName = "smartHotel.clients.droid.services.cardEmulation.cardService";

        // "OK" status word sent in response to SELECT AID command (0x9000)
        static readonly byte[] sELECT_OK_SW = HexStringToByteArray("9000");
        
        // "UNKNOWN" status word sent in response to invalid APDU command (0x0000)
        static readonly byte[] uNKNOWN_CMD_SW = HexStringToByteArray("0000");

        static string messageValue;

        public CardService() => MessagingCenter.Subscribe<string>(this, MessengerKeys.SendNFCToken, StartNFCService);

        public override byte[] ProcessCommandApdu(byte[] commandApdu, Bundle extras)
        {
            if (!string.IsNullOrEmpty(messageValue))
            {
                return ConcatArrays(Encoding.UTF8.GetBytes(messageValue), sELECT_OK_SW);
            }
            else
            {
                return uNKNOWN_CMD_SW;
            }
        }

        public override void OnDeactivated(DeactivationReason reason)
        {
        }

        static byte[] HexStringToByteArray(string hex) => Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();

        static byte[] ConcatArrays(byte[] first, byte[] rest)
        {
            var result = new byte[first.Length + rest.Length];
            first.CopyTo(result, 0);
            rest.CopyTo(result, first.Length);
            return result;
        }

        void StartNFCService(string message) => messageValue = message;
    }
}