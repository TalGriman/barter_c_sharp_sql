using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Category()
        {
        }

        public Category(int iD, string description, string icon)
        {
            ID = iD;
            Description = description;
            Icon = icon;
        }
    }
}