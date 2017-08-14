using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminChineseMoblie.Models
{
    public class PromoCodeModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string IMEI { get; set; }
        public string PromoCode { get; set; }
        public int AmountAttempts { get; set; }
        public bool isUsed { get; set; }
    }
}
