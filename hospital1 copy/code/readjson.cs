using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using betahospital;

namespace betahospital
{
    public class ReadJson
    {
        public List<RandomPeople> ReadPersonsFromJson(string url)
        {
            return ReadFromUrl(new Uri(url)).Result;
        }

        private async Task<List<RandomPeople>> ReadFromUrl(Uri url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    return JsonConvert.DeserializeObject<List<RandomPeople>>(json);
                }
                catch (HttpRequestException)
                {
                    //Error.
                    return null;
                }
            }
        }
    }
}
