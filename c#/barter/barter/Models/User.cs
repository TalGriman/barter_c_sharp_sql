using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barter.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Img { get; set; }
        public bool IsActive { get; set; }
        public bool IsFaceBook { get; set; }


        public User()
        {

        }

        public User(int iD, string email, string firstName, string lastName, string phoneNumber, string password, string img, bool isActive, bool isFaceBook)
        {
            ID = iD;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            Img = img;
            IsActive = isActive;
            IsFaceBook = isFaceBook;
        }

    }
}