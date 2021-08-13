using AppReservasULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppReservasULACIT.Controllers
{
    public class VueloManager
    {
        string Url = "http://localhost:49220/api/vuelo/";

        //INICIALIZAR EL HTTPCLIENT (REQUEST)
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return client;
        }

        public async Task<IEnumerable<Vuelo>> ObtenerVuelos(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Vuelo>>(resultado);
        }

        public async Task<Vuelo> ObtenerVuelo(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Vuelo>(resultado);
        }

        public async Task<Vuelo> Ingresar(Vuelo vuelo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Vuelo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Vuelo> Actualizar(Vuelo vuelo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Vuelo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}