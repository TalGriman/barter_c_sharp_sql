using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class Rate
    {
        public int CurrentUserID { get; set; }
        public int OtherUserID { get; set; }
        public int Points { get; set; }

        public Rate()
        {
        }

        public Rate(int currentUserID, int otherUserID, int points)
        {
            CurrentUserID = currentUserID;
            OtherUserID = otherUserID;
            Points = points;
        }

    }
}