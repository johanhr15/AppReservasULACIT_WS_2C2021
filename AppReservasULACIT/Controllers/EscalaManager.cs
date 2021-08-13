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
    public class EscalaManager
    {
        string Url = "http://localhost:49220/api/escala/";

        //INICIALIZAR EL HTTPCLIENT (REQUEST)
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return client;
        }

        public async Task<IEnumerable<Escala>> ObtenerEscalas(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Escala>>(resultado);
        }

        public async Task<Escala> ObtenerEscala(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Escala>(resultado);
        }

        public async Task<Escala> Ingresar(Escala escala, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(escala), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Escala>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Escala> Actualizar(Escala escala, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(escala), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Escala>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}