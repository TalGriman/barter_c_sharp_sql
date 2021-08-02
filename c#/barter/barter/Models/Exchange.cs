using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class Exchange
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int[] Categories { get; set; }

        public Exchange()
        {
        }

        public Exchange(int productID, int[] categories)
        {
            ProductID = productID;
            Categories = categories;
        }

        public Exchange(int productID, int categoryID)
        {
            ProductID = productID;
            CategoryID = categoryID;
        }
    }
}