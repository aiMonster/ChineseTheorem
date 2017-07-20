using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public static class DBSaver
    {
        public static bool Save(ToDBModel model)
        {
            DBTableModel mm = new DBTableModel();
            mm.condition = model.condition;
            mm.date = model.date;
            mm.expression = model.expression;
            mm.name = model.name;
            mm.status = model.status; 
            App.Database.SaveItem(mm);
            return true;
        }

    }
}
