using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChineseTheoremMobile
{
    public class ExpressionRepository
    {
        SQLiteConnection database;
        public ExpressionRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<DBTableModel>();
        }

        public IEnumerable<DBTableModel> GetItems()
        {
            return (from i in database.Table<DBTableModel>() select i).ToList();

        }

        public int SaveItem(DBTableModel item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }


    }
}
