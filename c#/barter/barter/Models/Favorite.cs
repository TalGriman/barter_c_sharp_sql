using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class Favorite
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }

        public Favorite()
        {
        }

        public Favorite(int userID, int productID)
        {
            UserID = userID;
            ProductID = productID;
        }
    }
}