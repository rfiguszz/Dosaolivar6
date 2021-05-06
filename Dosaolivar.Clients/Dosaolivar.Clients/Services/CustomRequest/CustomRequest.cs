using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Xamarin.Essentials;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Extensions;

namespace SmartHotel.Clients.Core.Services.CustomRequest
{
    public class CustomRequest
    {
        private const string url = "https://jsonplaceholder.typicode.com/posts";
        readonly JsonSerializerSettings serializerSettings;
        private HttpClient httpClient = new HttpClient();

        public CustomRequest()
        {
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            serializerSettings.Converters.Add(new StringEnumConverter());
        }

      
        // BORRAR PARCELA
        public static async Task<GenericServiceAnswer> DeleteParcelaAPIModel(string _id)
        {
            Console.WriteLine("ELIMINANDO PARCELA");
            // Setting http method and creating a new http request message
            var builder = new UriBuilder(AppSettings.ParcelasEndpoint);
            builder.AppendToPath(_id);

            var uri = builder.ToString();
            Console.WriteLine(uri);
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                Console.WriteLine("HACIENDO PETICION");
                var response = await httpClient.DeleteAsync(uri);
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData)).ConfigureAwait(false);
                return jsonObject;
            }

        }

      
        // BORRAR EQUIPO
        public static async Task<GenericServiceAnswer> DeleteEquipoAPIModel(string _id)
        {
            Console.WriteLine("ELIMINANDO EQUIPO");
            // Setting http method and creating a new http request message
            var builder = new UriBuilder(AppSettings.EquiposEndpoint);
            builder.AppendToPath(_id);

            var uri = builder.ToString();
            Console.WriteLine(uri);
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                Console.WriteLine("HACIENDO PETICION");
                var response = await httpClient.DeleteAsync(uri);
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData)).ConfigureAwait(false);
                return jsonObject;
            }

        }

        // BORRAR TRATAMIENTO
        public static async Task<GenericServiceAnswer> DeleteTratamientoAPIModel(string _id)
        {
            Console.WriteLine("ELIMINANDO TRATAMIENTO");
            var builder = new UriBuilder(AppSettings.TratamientosEndpoint);
            builder.AppendToPath(_id);
            var uri = builder.ToString();
            Console.WriteLine(uri);
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                Console.WriteLine("HACIENDO PETICION");
                var response = await httpClient.DeleteAsync(uri);
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData)).ConfigureAwait(false);
                return jsonObject;
            }

        }
    }
}

