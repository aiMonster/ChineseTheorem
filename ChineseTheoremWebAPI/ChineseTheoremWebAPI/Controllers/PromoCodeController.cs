using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChineseTheoremWebAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ChineseTheoremWebAPI.Controllers
{
    public class PromoCodeController : ApiController
    {

        static HttpClient client = new HttpClient();
        SqlConnection sqlConnection;


        [HttpGet]
        [Route("api/promocode/getinfopromocode/{promocode}")]
        public PromoCodeModel GetInfoPromoCode(string promoCode)
        {
            PromoCodeModel tmpMod = new PromoCodeModel();
            tmpMod.PromoCode = promoCode;
            string connectionString = "Data Source=chinesetheoremserver.database.windows.net;Initial Catalog=ChineseTheoremDataBase;Integrated Security=False;User ID=ChineseTheoremAdmin;Password=Admin2017;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [PromoCodesTable] WHERE PromoCode='" + promoCode + "'", sqlConnection);
            try
            {
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    tmpMod.IsUsed = Convert.ToBoolean(sqlReader["IsUsed"]);
                    tmpMod.AmountAttempts = Convert.ToInt32(sqlReader["AmountAttempts"]);
                    tmpMod.Date = Convert.ToString(sqlReader["Date"]);
                    tmpMod.IMEI = Convert.ToString(sqlReader["IMEI"]);
                    tmpMod.Id = Convert.ToInt32(sqlReader["Id"]);
                }
            }
            catch
            {
                tmpMod.AmountAttempts = 0;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

            return tmpMod;
        }




        [HttpGet]
        public int ActivatePromoCode(string promoCode, string IMEI)
        {
            PromoCodeModel tmpMod = new PromoCodeModel();
            string connectionString = "Data Source=chinesetheoremserver.database.windows.net;Initial Catalog=ChineseTheoremDataBase;Integrated Security=False;User ID=ChineseTheoremAdmin;Password=Admin2017;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;
            
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [PromoCodesTable] WHERE PromoCode='" + promoCode + "'", sqlConnection);
            try
            {                
                sqlReader = sqlCommand.ExecuteReader();
                
                while (sqlReader.Read())
                {
                    tmpMod.IsUsed = Convert.ToBoolean(sqlReader["IsUsed"]); 
                    if(tmpMod.IsUsed)
                    {
                        tmpMod.AmountAttempts = 0;
                    }                 
                    else
                    {
                        tmpMod.AmountAttempts = Convert.ToInt32(sqlReader["AmountAttempts"]);
                    }
                                           
                }
            }
            catch
            {
                tmpMod.AmountAttempts = 0;
                tmpMod.IsUsed = true;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

            try
            {
                if(!tmpMod.IsUsed)
                {
                    SqlCommand command = new SqlCommand("UPDATE [PromoCodesTable] SET Date=@date, IMEI=@imei, IsUsed=@isUsed WHERE PromoCode=@promo", sqlConnection);
                    string date = Convert.ToString(DateTime.Now);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@imei", IMEI);
                    command.Parameters.AddWithValue("@isUsed", 1);
                    command.Parameters.AddWithValue("@promo", promoCode);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                tmpMod.AmountAttempts = 0;
            }
            sqlConnection.Close();
            return tmpMod.AmountAttempts;
        }

        [HttpGet]
        [Route ("api/promocode/transferpromocode/{amountattempts:int}/{imei}")]
        public string TransferPromoCode(int amountAttempts, string IMEI)
        {
            PromoCodeModel tmpMod = new PromoCodeModel();
            
            string connectionString = "Data Source=chinesetheoremserver.database.windows.net;Initial Catalog=ChineseTheoremDataBase;Integrated Security=False;User ID=ChineseTheoremAdmin;Password=Admin2017;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;

            string generatedPromo = "";
            while(true)
            {
                generatedPromo = GeneratePromoCode();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [PromoCodesTable] WHERE PromoCode='" + generatedPromo + "'", sqlConnection);
                try
                {
                    sqlReader = sqlCommand.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        generatedPromo = "";
                    }
                }
                catch
                {
                    generatedPromo = "";
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }

                if (generatedPromo != "")
                    break;
            }
            tmpMod.PromoCode = generatedPromo;
            tmpMod.Date = Convert.ToString(DateTime.Now);
            tmpMod.IMEI = IMEI;
            tmpMod.AmountAttempts = amountAttempts;
            tmpMod.IsUsed = false;

            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [PromoCodesTable] (Date, IMEI, PromoCode, AmountAttempts, IsUsed) VALUES (@date, @imei, @promocode, @amountattempts, @isused)", sqlConnection);
                command.Parameters.Add("@date", SqlDbType.NChar).Value = tmpMod.Date;
                command.Parameters.Add("@imei",SqlDbType.NChar).Value = tmpMod.IMEI;
                command.Parameters.Add("@isused", SqlDbType.Bit).Value = 0;
                command.Parameters.Add("@promocode", SqlDbType.NChar).Value = tmpMod.PromoCode;
                command.Parameters.Add("@amountattempts", SqlDbType.Int).Value = tmpMod.AmountAttempts;
                command.ExecuteNonQuery();

            }
            catch
            {
                tmpMod.PromoCode = "";
            }

            sqlConnection.Close();
            return tmpMod.PromoCode;
        }

        private string GeneratePromoCode()
        {
            string result = "";
            char[] stuff = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm'};
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {                
                result += stuff[rnd.Next(0, stuff.Count())];
            }
            return result;
        }

    }
}
