using AdminChineseMoblie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AdminChineseMoblie.Services
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

        //takes amount of points we want to transfer into promoCode
        public async Task<string> TransferCode(int amount)
        {
            HttpClient client = GetClient();
            string imei = "adminMobile";
            string result = await client.GetStringAsync(Url + "/transferpromocode/" + amount + "/" + imei);
            return JsonConvert.DeserializeObject<string>(result);
        }

        //takes promoCode we want get info about
        public async Task<PromoCodeModel> GetInfoAboutCode(string promoCode)
        {
            HttpClient client = GetClient();            
            string result = await client.GetStringAsync(Url + "/getinfopromocode/" + promoCode);
            return JsonConvert.DeserializeObject<PromoCodeModel>(result);
        }
    }
}
