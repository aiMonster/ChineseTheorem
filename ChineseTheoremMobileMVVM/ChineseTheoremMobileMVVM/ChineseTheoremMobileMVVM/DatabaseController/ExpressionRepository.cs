using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChineseTheoremMobileMVVM.Models;

namespace ChineseTheoremMobileMVVM.DatabaseController
{
    public class ExpressionRepository
    {

        SQLiteConnection database;
        public ExpressionRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<ExpressionModel>();
        }

        public IEnumerable<ExpressionModel> GetItems()
        {
            return (from i in database.Table<ExpressionModel>() select i).ToList();

        }

        public int SaveItem(ExpressionModel item)
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

        public int DeleteItem(int id)
        {
            return database.Delete<ExpressionModel>(id);
        }

    }
}
