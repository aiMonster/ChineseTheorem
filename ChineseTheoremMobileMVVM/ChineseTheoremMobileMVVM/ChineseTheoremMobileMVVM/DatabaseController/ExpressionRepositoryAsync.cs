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
    //controller for out dataBase
    public class ExpressionRepositoryAsync
    {
        SQLiteAsyncConnection database;
        public ExpressionRepositoryAsync(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<ExpressionModel>();
        }
        
        public async Task <List<ExpressionModel>> GetItemsAsync()
        {
            return await database.Table<ExpressionModel>().ToListAsync();

        }

        public async Task<int> SaveItem(ExpressionModel item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(ExpressionModel item)
        {
            return await database.DeleteAsync(item);
        }

    }
}
