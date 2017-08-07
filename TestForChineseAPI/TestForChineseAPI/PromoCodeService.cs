using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestForChineseAPI
{
    class PromoCodeService
    {
        const string Url = "http://localhost:52580/api/promocode/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        //public async Task<IEnumerable<PromoCodeModel>> Get()
        //{
        //    HttpClient client = GetClient();
        //    string result = await client.GetStringAsync(Url);
        //    return JsonConvert.DeserializeObject<IEnumerable<PromoCodeModel>>(result);
        //}

        public async Task<PromoCodeModel> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + 1);
            return JsonConvert.DeserializeObject<PromoCodeModel>(result);
        }
    }
}
