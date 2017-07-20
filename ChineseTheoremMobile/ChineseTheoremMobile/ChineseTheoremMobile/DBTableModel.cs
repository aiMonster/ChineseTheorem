using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    [Table("Expressions")]
    public class DBTableModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string name { get; set; }
        public string condition { get; set; }
        public string expression { get; set; }
        public bool status { get; set; }
        public DateTime date { get; set; }
    }
}
