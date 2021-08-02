using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class OtherProfileStartDetails
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Img { get; set; }
        public bool IsFaceBook { get; set; }
        public int Rating { get; set; }
        public int CurrentUserPoints { get; set; }

        public OtherProfileStartDetails()
        {
        }

        public OtherProfileStartDetails(string email, string firstName, string lastName, string phoneNumber, string img, bool isFaceBook, int rating, int currentUserPoints)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Img = img;
            IsFaceBook = isFaceBook;
            Rating = rating;
            CurrentUserPoints = currentUserPoints;
        }
    }
}