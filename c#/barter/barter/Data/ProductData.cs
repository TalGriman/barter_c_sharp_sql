using barter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace barter.Data
{
    public class ProductData
    {
        private readonly DbConnection db = new DbConnection();

        public int AddUserProduct(Product product)
        {
            string sql = "AddUserProduct";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userID = new SqlParameter("@UserID", SqlDbType.Int);
            userID.Value = product.UserID;
            cmd.Parameters.Add(userID);

            SqlParameter title = new SqlParameter("@Title", SqlDbType.Text);
            title.Value = product.Title;
            cmd.Parameters.Add(title);

            SqlParameter description = new SqlParameter("@Description", SqlDbType.Text);
            description.Value = product.Description;
            cmd.Parameters.Add(description);

            SqlParameter address = new SqlParameter("@Address", SqlDbType.Text);
            address.Value = product.Address;
            cmd.Parameters.Add(address);

            SqlParameter category = new SqlParameter("@Category", SqlDbType.Int);
            category.Value = product.Category;
            cmd.Parameters.Add(category);

            SqlParameter latitude = new SqlParameter("@Latitude", SqlDbType.Decimal);
            latitude.Value = product.Latitude;
            cmd.Parameters.Add(latitude);

            SqlParameter longitude = new SqlParameter("@Longitude", SqlDbType.Decimal);
            longitude.Value = product.Longitude;
            cmd.Parameters.Add(longitude);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = product.Img;
            cmd.Parameters.Add(img);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

            //run the command
            db.ExecuteAndClose(cmd);
            return Convert.ToInt32(cmd.Parameters["@ID"].Value);
        }


        public int UpdateImage(Product product)
        {
            string sql = "UpdateProductImage";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter productId = new SqlParameter("@ID", SqlDbType.Int);
            productId.Value = product.ID;
            cmd.Parameters.Add(productId);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = product.Img;
            cmd.Parameters.Add(img);

            //run the command
            return db.ExecuteAndClose(cmd);
        }

        public int AddExchanges(Exchange exchanges)
        {
            int[] categoriesArr = exchanges.Categories;
            int counter = 0;
            string sql = "AddExchanges";
            for (int i = 0; i < categoriesArr.Length; i++)
            {
                SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

                //create params
                SqlParameter productID = new SqlParameter("@ProductID", SqlDbType.Int);
                productID.Value = exchanges.ProductID;
                cmd.Parameters.Add(productID);

                SqlParameter categoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                categoryID.Value = categoriesArr[i];
                cmd.Parameters.Add(categoryID);

                counter += db.ExecuteAndClose(cmd);
            }

            return counter;
        }

        public int ClearExchanges(Exchange exchanges)
        {
            string sql = "ClearExchanges";

            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter productID = new SqlParameter("@ProductID", SqlDbType.Int);
            productID.Value = exchanges.ProductID;
            cmd.Parameters.Add(productID);

            return db.ExecuteAndClose(cmd);
        }

        public List<ProductFullDetails> AllProductsByCategory(UserSelectedCategory usc)
        {
            string sql = "AllProductsByCategory";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter categoryID = new SqlParameter("@categoryID", SqlDbType.Int);
            categoryID.Value = usc.CategoryID;
            cmd.Parameters.Add(categoryID);

            SqlParameter currentUserId = new SqlParameter("@currentUserId", SqlDbType.Int);
            currentUserId.Value = usc.CurrentUserID;
            cmd.Parameters.Add(currentUserId);

            DataTable dt = db.ReadAndClose(cmd);
            List<ProductFullDetails> uscList = db.ConvertDataTable<ProductFullDetails>(dt);
            if (uscList.Count == 0)
            {
                return null;
            }
            return uscList;
        }

        public int HandleFavorite(Favorite favorite)
        {
            string sql = "HandleFavorite";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userID = new SqlParameter("@UserID", SqlDbType.Int);
            userID.Value = favorite.UserID;
            cmd.Parameters.Add(userID);

            SqlParameter productID = new SqlParameter("@ProductID", SqlDbType.Int);
            productID.Value = favorite.ProductID;
            cmd.Parameters.Add(productID);

            //run the command
            return db.ExecuteAndClose(cmd);
        }


        public List<ProductFullDetails> AllProductsByUser(UserSelectedCategory userProducts)
        {
            string sql = "AllProductsByUser";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            SqlParameter currentUserId = new SqlParameter("@currentUserId", SqlDbType.Int);
            currentUserId.Value = userProducts.CurrentUserID;
            cmd.Parameters.Add(currentUserId);

            DataTable dt = db.ReadAndClose(cmd);
            List<ProductFullDetails> userProductsList = db.ConvertDataTable<ProductFullDetails>(dt);
            if (userProductsList.Count == 0)
            {
                return null;
            }
            return userProductsList;
        }


        public List<ProductFullDetails> UserFavoriteProducts(UserSelectedCategory userProducts)
        {
            string sql = "UserFavoriteProducts";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            SqlParameter currentUserId = new SqlParameter("@currentUserId", SqlDbType.Int);
            currentUserId.Value = userProducts.CurrentUserID;
            cmd.Parameters.Add(currentUserId);

            DataTable dt = db.ReadAndClose(cmd);
            List<ProductFullDetails> userProductsList = db.ConvertDataTable<ProductFullDetails>(dt);
            if (userProductsList.Count == 0)
            {
                return null;
            }
            return userProductsList;
        }


        public int EditProduct(Product product)
        {
            string sql = "EditProduct";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter productID = new SqlParameter("@ID", SqlDbType.Int);
            productID.Value = product.ID;
            cmd.Parameters.Add(productID);

            SqlParameter title = new SqlParameter("@Title", SqlDbType.Text);
            title.Value = product.Title;
            cmd.Parameters.Add(title);

            SqlParameter description = new SqlParameter("@Description", SqlDbType.Text);
            description.Value = product.Description;
            cmd.Parameters.Add(description);

            SqlParameter address = new SqlParameter("@Address", SqlDbType.Text);
            address.Value = product.Address;
            cmd.Parameters.Add(address);

            SqlParameter category = new SqlParameter("@Category", SqlDbType.Int);
            category.Value = product.Category;
            cmd.Parameters.Add(category);

            SqlParameter latitude = new SqlParameter("@Latitude", SqlDbType.Decimal);
            latitude.Value = product.Latitude;
            cmd.Parameters.Add(latitude);

            SqlParameter longitude = new SqlParameter("@Longitude", SqlDbType.Decimal);
            longitude.Value = product.Longitude;
            cmd.Parameters.Add(longitude);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = product.Img;
            cmd.Parameters.Add(img);

            return db.ExecuteAndClose(cmd);
        }

        public int DeleteProductFull(Product product)
        {
            string sql = "DeleteProductFull";

            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter productID = new SqlParameter("@ProductID", SqlDbType.Int);
            productID.Value = product.ID;
            cmd.Parameters.Add(productID);

            return db.ExecuteAndClose(cmd);
        }


    }
}