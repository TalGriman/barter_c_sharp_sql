using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class UserSelectedCategory
    {
        public int CategoryID { get; set; }
        public int CurrentUserID { get; set; }

        public UserSelectedCategory()
        {
        }

        public UserSelectedCategory(int categoryID, int currentUserID)
        {
            CategoryID = categoryID;
            CurrentUserID = currentUserID;
        }
    }
}