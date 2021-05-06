using SmartHotel.Clients.Core.Extensions;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SmartHotel.Clients.Core.Helpers;
using System.Diagnostics;

namespace SmartHotel.Clients.Core.Services.MisParcelas
{
    public class ParcelasService : IParcelasService
    {
        readonly IRequestService requestService;
        DOSAOLIVARStaticValues DOSAFuctions;

        public ParcelasService(IRequestService requestService)
        {
            this.requestService = requestService;
            DOSAFuctions = new DOSAOLIVARStaticValues();
        }

        public Task<IEnumerable<ParcelasModel>> GetParcelasAsync()
        {
            var builder = new UriBuilder(AppSettings.ParcelasEndpoint);
            builder.AppendToPath("username/" + AppSettings.sessionUsername);

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<ParcelasModel>>(uri);
        }

        // Borra Parcela por ID
        public async Task<GenericServiceAnswer> DeleteParcelasAsync(string _id)
        {
            HttpClient httpClient = new HttpClient();
            Console.WriteLine("aqui estamos para borrar");
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var builder = new UriBuilder(AppSettings.ParcelasEndpoint);
                builder.AppendToPath(_id);

                var uri = builder.ToString();
                Console.WriteLine(uri);
                var response = await httpClient.DeleteAsync(uri);
                var responseData = await response.Content.ReadAsStringAsync();
                string x = responseData;
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(x)).ConfigureAwait(false);
                return jsonObject;
            }
        }


        public Task<IEnumerable<BoquillasModel>> GetAllBoquillasAsync()
        {
            var builder = new UriBuilder(AppSettings.BoquillasEndpoint);
            builder.AppendToPath("all/filtro");

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<BoquillasModel>>(uri);
        }

        public async Task<BoquillasModel> GetOneBoquillaAsync(string _index)
        {
            var builder = new UriBuilder(AppSettings.BoquillasEndpoint);
            builder.AppendToPath($"getOneByID/{_index}");

            var uri = builder.ToString();
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var response = await httpClient.GetAsync(uri);
                var responseData = await response.Content.ReadAsStringAsync();
                string x = responseData.ToString();
                x = x.Replace("[", "");
                x = x.Replace("]", "");
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<BoquillasModel>(x)).ConfigureAwait(false);
                return jsonObject;
            }
        }

        public Task<IEnumerable<EquipoModel>> GetAllEquiposAsync()
        {
            var builder = new UriBuilder(AppSettings.EquiposEndpoint);
            builder.AppendToPath("username/" + AppSettings.sessionUsername);

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<EquipoModel>>(uri);
        }

        public Task<IEnumerable<TratamientosModel>> GetAllTratamientosAsync()
        {
            var builder = new UriBuilder(AppSettings.TratamientosEndpoint);
            builder.AppendToPath("username/" + AppSettings.sessionUsername);

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<TratamientosModel>>(uri);
        }


        // Recoge los datos de una localidad por CP
        public async Task <LocalidadModel> GetManualLocalidadByCPAsync(string _cp)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var response = await httpClient.GetAsync("https://dosaolivar.macross.is/panel/services/municipios/cp/" + _cp);
                var responseData = await response.Content.ReadAsStringAsync();
                string x = responseData;
                x  = x.Replace("[", "");
                x = x.Replace("]", "");
                x = x.Replace("&OACUTE;", "Ó");
                x = x.Replace("&Oacute;", "Ó");
                x = x.Replace("&Eacute;", "E");
                x = x.Replace("&EACUTE;", "E");
                x = x.Replace("&Iacute;", "I");
                x = x.Replace("&IACUTE;", "Í");
                x = x.Replace("&IACUTE;", "Í");
                x = x.Replace("&Ntilde;", "Ñ");
                x = x.Replace("&NTILDE;", "Ñ");
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<LocalidadModel>(x)).ConfigureAwait(false);
                jsonObject.localidad = DOSAFuctions.getLocalidad(jsonObject.localidad);
                return jsonObject;
            }
        }

        // Recoge los datos de una localidad por CP
        public Task<LocalidadModel> GetLocalidadByCPAsync(string _cp)
        {
            var builder = new UriBuilder(AppSettings.LocalidadEndpoint);
            builder.AppendToPath("cp/" + _cp);

            var uri = builder.ToString();
            return requestService.GetAsync<LocalidadModel>(uri);
        }

        public async Task<GenericServiceAnswer> PostNewParcelaAsync(ParcelasModel data, string apiUrl)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                using (httpClient)
                {
                    // Setting http method and creating a new http request message
                    var serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
                    //var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "application/json"));
                    var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "text/plain"));
                    var responseData = await response.Content.ReadAsStringAsync();
                    var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData.ToString())).ConfigureAwait(false);
                    return jsonObject;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<GenericServiceAnswer> PostNewEquipoAsync(EquipoModel data, string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
                var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "application/json"));
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData.ToString())).ConfigureAwait(false);
                return jsonObject;
            }
        }

        public async Task<GenericServiceAnswer> PostNewTratamientoAsync(TratamientosModel data, string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
                var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "application/json"));
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData)).ConfigureAwait(false);
                return jsonObject;
            }
        }

        public Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            var builder = new UriBuilder(AppSettings.ProductosEndpoint);
            builder.AppendToPath("getAll");

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<Producto>>(uri);
        }
    }
}