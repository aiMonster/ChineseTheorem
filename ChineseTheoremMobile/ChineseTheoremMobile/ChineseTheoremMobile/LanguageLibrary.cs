using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public static class LanguageLibrary
    {

        public static LanguageModel English()
        {
            LanguageModel model = new LanguageModel();

            model.manual = "Manual";
            model.decide = "Decide";
            model.history = "History";
            model.promoCode = "Promo code";

            return model;
        }

        public static LanguageModel Ukrainian()
        {
            LanguageModel model = new LanguageModel();

            model.manual = "Посібник";
            model.decide = "Калькулятор";
            model.history = "Історія";
            model.promoCode = "Промо код";

            return model;
        }
    }
}
