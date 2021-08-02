using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class ProductFullDetails
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string Product_Description { get; set; }
        public string Address { get; set; }
        public string Img { get; set; }
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Category_Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int CategoryID { get; set; }
        public int Rating { get; set; }
        public bool Favorite { get; set; }
        public string Exchanges { get; set; }

        public ProductFullDetails()
        {
        }

        public ProductFullDetails(int userID, int productID, string product_Description, string address, string img, DateTime date, double latitude, double longitude, string category_Description, string email, string phoneNumber, string title, string userName, int categoryID, int rating, bool favorite, string exchanges)
        {
            UserID = userID;
            ProductID = productID;
            Product_Description = product_Description;
            Address = address;
            Img = img;
            Date = date;
            Latitude = latitude;
            Longitude = longitude;
            Category_Description = category_Description;
            Email = email;
            PhoneNumber = phoneNumber;
            Title = title;
            UserName = userName;
            CategoryID = categoryID;
            Rating = rating;
            Favorite = favorite;
            Exchanges = exchanges;
        }
    }
}