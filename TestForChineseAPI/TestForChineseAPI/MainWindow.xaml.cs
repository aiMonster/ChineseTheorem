using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestForChineseAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool initialized = false;
        PromoCodeService promoCodeService = new PromoCodeService();
        //static HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (initialized == true) return;
            //IEnumerable<PromoCodeModel> codes = await promoCodeService.Get();
            //PromoCodeModel  codes = await promoCodeService.Get();
        }

        private async void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(await promoCodeService.GetCode(Convert.ToInt32(tbTransfer.Text)));
        }

        //static async Task<string> TransferPromoCode(int amount)
        //{
        //    client.BaseAddress = new Uri("http://localhost:52580/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    string result = "";

        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync("api/promocode/transferpromocode/911/deimei");
        //        if(response.IsSuccessStatusCode)
        //        {
        //            result = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("something wrong");
        //    }

        //    return result;
        //}
    }
}
