using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class Product
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Img { get; set; }
        public DateTime Date { get; set; }

        public Product()
        {
        }

        public Product(int iD, int userID, string title, string description, string address, int category, double latitude, double longitude, string img, DateTime date)
        {
            ID = iD;
            UserID = userID;
            Title = title;
            Description = description;
            Address = address;
            Category = category;
            Latitude = latitude;
            Longitude = longitude;
            Img = img;
            Date = date;
        }
    }
}