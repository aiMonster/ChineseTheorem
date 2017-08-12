using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobileMVVM.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

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

        public async Task<string> TransferCode(int amount)
        {
            HttpClient client = GetClient();
            string imei = DependencyService.Get<IImeiGetter>().GetImei();            
            string result = await client.GetStringAsync(Url + "/transferpromocode/" + amount + "/" + imei);
            return JsonConvert.DeserializeObject<string>(result);
        }

        public async Task<int> ActivateCode(string promoCode)
        {
            HttpClient client = GetClient();
            string imei = DependencyService.Get<IImeiGetter>().GetImei();
            string result = await client.GetStringAsync(Url + "/activatepromocode/" + promoCode + "/" + imei);
            return JsonConvert.DeserializeObject<int>(result);
        }
    }
}
