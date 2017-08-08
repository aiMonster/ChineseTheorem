using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobileMVVM.Models;
using Newtonsoft.Json;

namespace ChineseTheoremMobileMVVM.Services
{
    public class PromoCodeService
    {
        const string Url = "http://chinesetheoremwebapi.azurewebsites.net/api/promocode/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<string> GetCode(int amount)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/transferpromocode/" + amount + "/deimei");
            return JsonConvert.DeserializeObject<string>(result);
        }

        public async Task<int> TransferCode(string promoCode)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/activatepromocode/" + promoCode + "/deimei");
            return JsonConvert.DeserializeObject<int>(result);
        }
    }
}
