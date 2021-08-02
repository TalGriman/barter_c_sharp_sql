using barter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace barter.Data
{
    public class CategoryData
    {
        private readonly DbConnection db = new DbConnection();

        public List<Category> SelectAllCategories()
        {
            string sql = "SelectAllCategories";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");
        
            DataTable dt = db.ReadAndClose(cmd);
            List<Category> categoryList = db.ConvertDataTable<Category>(dt);

            return categoryList;
        }
    }
}