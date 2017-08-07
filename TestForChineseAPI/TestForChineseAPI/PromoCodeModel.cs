using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForChineseAPI
{
    public class PromoCodeModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string IMEI { get; set; }
        public string PromoCode { get; set; }
        public int AmountAttempts { get; set; }
        public bool isUsed { get; set; }

        public override bool Equals(object obj)
        {
            PromoCodeModel promoCode = obj as PromoCodeModel;
            return this.Id == promoCode.Id;
        }
    }
}
