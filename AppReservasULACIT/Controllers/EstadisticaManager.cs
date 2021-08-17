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
    public class EstadisticaManager
    {
        string Url = "http://localhost:49220/api/estadistica/";

        //INICIALIZAR EL HTTPCLIENT (REQUEST)
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization",token);
            client.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return client;
        }

        public async Task<IEnumerable<Estadistica>> ObtenerEstadisticas(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Estadistica>>(resultado);
        }

        public async Task<Estadistica> ObtenerEstadistica(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url,codigo));

            return JsonConvert.DeserializeObject<Estadistica>(resultado);
        }

        public async Task<Estadistica> Ingresar(Estadistica estadistica, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(estadistica), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Estadistica>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Estadistica> Actualizar(Estadistica estadistica, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(estadistica), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Estadistica>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}