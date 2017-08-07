using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChineseTheoremWebAPI.Models
{
    public class PromoCodeModel
    {        
        
        public int Id { get; set; }        
        public string Date { get; set; }
        public string IMEI { get; set; }
        public string PromoCode { get; set; }
        public int AmountAttempts { get; set; }
        public bool IsUsed { get; set; }

    }
}