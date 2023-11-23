using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Esemenynaptar_BackEnd
{
    public class EventService
    {
        HttpClient client = new HttpClient();
        //string url = "https://retoolapi.dev/dFqFgC/data";
        //string url = "https://retoolapi.dev/kNEA4T/events";
        //string url = "https://retoolapi.dev/ByFje8/events";
        string url = "https://retoolapi.dev/spRLIm/events";

        public List<Event> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<Event>>(json);
        }

        public Event Add(Event eseneny)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(eseneny), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Event>(responseContent);
        }

        public bool Delete(Event esemeny)
        {
            int id = esemeny.ID;
            HttpResponseMessage responseMessage = client.DeleteAsync($"{url}/{id}").Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public Event Update(int id, Event person)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Event>(responseContent);
        }
    }
}
