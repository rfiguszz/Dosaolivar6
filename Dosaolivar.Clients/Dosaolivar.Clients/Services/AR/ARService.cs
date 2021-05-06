using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Services.AR
{
    public class ARService
    {
        static ARService arService;

        public static ARService Current
        {
            get
            {
                if (arService == null)
                {
                    arService = new ARService();
                }

                return arService;
            }
        }

        public void OnCapturarMedidas(MedidasAR medidas)
        {
            medidas.CopaH = Math.Round(medidas.CopaH,2);
            medidas.CopaD1 = Math.Round(medidas.CopaD1, 2);
            medidas.CopaD2 = Math.Round(medidas.CopaD2, 2);

            MessagingCenter.Send<ARService, MedidasAR>(this, MessageKeys.OnMedidas, medidas);
        }
    }
}
