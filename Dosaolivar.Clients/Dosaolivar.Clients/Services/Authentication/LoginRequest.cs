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
namespace SmartHotel.Clients.Core.Services.Authentication
{
    public class LoginRequest
    {
        private const string url = "https://jsonplaceholder.typicode.com/posts";
        readonly JsonSerializerSettings serializerSettings;
        private HttpClient httpClient = new HttpClient();

        public LoginRequest()
        {
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public static async Task<User> LoginAPI(string userName, string userPassword, string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl))
                {
                    var content = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("password", userPassword)
                    });

                    // TODO : Optional setup for PUT and POST methods, should be removed for GET method.
                    httpRequestMessage.Content = content;
                    // Sending request message and setting configure await as false to run code in background
                    // Sending Request
                    using (var httpResponse = await httpClient.SendAsync(httpRequestMessage))
                    {
                        Console.WriteLine("Recogiendo info");
                        // Reading result
                        string readHttpResponse = await httpResponse.Content.ReadAsStringAsync();
                        var jObject = await Task.Run(() => JObject.Parse(readHttpResponse)).ConfigureAwait(false);
                        // Deserializing json object and setting configure await  as false to run code in background by running code in Task
                        var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<Models.User>(jObject.ToString())).ConfigureAwait(false);
                        return jsonObject;
                        // TODO : Use same steps within basic authentication to be able to work with Json data
                    }
                }

            }
        }
        // Returning final list


        public static async Task<Models.User> LoginAPIModel(Models.User data, string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
                var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "application/json"));
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<Models.User>(responseData.ToString())).ConfigureAwait(false);
                return jsonObject;
            }

        }




        public static async Task<GenericServiceAnswer> RegisterUserAPIModel(Models.User data, string apiUrl)
        {

            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
                var response = await httpClient.PostAsync(apiUrl, new StringContent(serialized, Encoding.UTF8, "application/json"));
                var responseData = await response.Content.ReadAsStringAsync();
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<Models.GenericServiceAnswer>(responseData.ToString())).ConfigureAwait(false);
                return jsonObject;
            }

        }

        public static async Task<GenericServiceAnswer> GetReportAsync(string apiUrl)
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                // Setting http method and creating a new http request message
                var response = await httpClient.GetAsync(apiUrl);
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<GenericServiceAnswer>(responseData)).ConfigureAwait(false);
                return jsonObject;
            }
        }


    }
}


/* using (httpClient)
  {
      // Setting default request authorization
     // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(convertStringtoByteArray(userName, userPassword)));
 
      // Setting http method and creating a new http request message
      using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl))
      {
          // TODO :  Enter your own api key and key value 
          //httpRequestMessage.Headers.Add("YourApiKey", "YourApiKeyValue");
 
          // Sending request message and setting configure await as false to run code in background
          using (var httpResponse = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false))
          {
              // Reading result and setting configure await as false to run code in background
              string returnValue = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
 
              // Converting string result to json object and setting configure await  as false to run code in background
              var jObject = await Task.Run(() => JObject.Parse(returnValue)).ConfigureAwait(false);
 
              // Deserializing json object and setting configure await  as false to run code in background by running code in Task
              var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<LoginAnswer.Example>(jObject.ToString())).ConfigureAwait(false);
 
              // Filling list with each object within result
              foreach (var user in jsonObject.SessionResponse.User)
              {
                  Console.WriteLine("hola");
 
              }
          }
      }
  }*/
