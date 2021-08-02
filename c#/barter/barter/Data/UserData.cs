using barter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace barter.Data
{
    public class UserData
    {
        private readonly DbConnection db = new DbConnection();

        public int CreateUser(User user)
        {
            string sql = "AddUser";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter email = new SqlParameter("@Email", SqlDbType.Text);
            email.Value = user.Email;
            cmd.Parameters.Add(email);

            SqlParameter password = new SqlParameter("@Password", SqlDbType.Text);
            password.Value = user.Password;
            cmd.Parameters.Add(password);

            SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.Text);
            firstName.Value = user.FirstName;
            cmd.Parameters.Add(firstName);

            SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.Text);
            lastName.Value = user.LastName;
            cmd.Parameters.Add(lastName);

            SqlParameter phoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.Text);
            phoneNumber.Value = user.PhoneNumber;
            cmd.Parameters.Add(phoneNumber);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = user.Img;
            cmd.Parameters.Add(img);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

            //run the command
            db.ExecuteAndClose(cmd);
            return Convert.ToInt32(cmd.Parameters["@ID"].Value);
        }

        public User LoginUser(User user)
        {
            string sql = "SelectUser";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter email = new SqlParameter("@Email", SqlDbType.Text);
            email.Value = user.Email;
            cmd.Parameters.Add(email);

            SqlParameter password = new SqlParameter("@Password", SqlDbType.Text);
            password.Value = user.Password;
            cmd.Parameters.Add(password);

            DataTable dt = db.ReadAndClose(cmd);
            List<User> userList = db.ConvertDataTable<User>(dt);
            if (userList.Count == 0)
            {
                return null;
            }
            return userList[0];
        }


        public int UpdateImage(User user)
        {
            string sql = "UpdateUserImage";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userId = new SqlParameter("@ID", SqlDbType.Int);
            userId.Value = user.ID;
            cmd.Parameters.Add(userId);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = user.Img;
            cmd.Parameters.Add(img);

            //run the command
            return db.ExecuteAndClose(cmd);
        }

        public int SelectCurrentUserRating(User user)
        {
            string sql = "UserAvgRating";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userID = new SqlParameter("@ID", SqlDbType.Int);
            userID.Value = user.ID;
            cmd.Parameters.Add(userID);

            cmd.Parameters.Add("@Result", SqlDbType.Int);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;

            //run the command
            db.ExecuteAndClose(cmd);
            return Convert.ToInt32(cmd.Parameters["@Result"].Value);
        }

        public User UpdateUserDetails(User user)
        {
            string sql = "UpdateUserDetails";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userID = new SqlParameter("@ID", SqlDbType.Int);
            userID.Value = user.ID;
            cmd.Parameters.Add(userID);

            SqlParameter password = new SqlParameter("@Password", SqlDbType.Text);
            password.Value = user.Password;
            cmd.Parameters.Add(password);

            SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.Text);
            firstName.Value = user.FirstName;
            cmd.Parameters.Add(firstName);

            SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.Text);
            lastName.Value = user.LastName;
            cmd.Parameters.Add(lastName);

            SqlParameter phoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.Text);
            phoneNumber.Value = user.PhoneNumber;
            cmd.Parameters.Add(phoneNumber);

            //run the command
            DataTable dt = db.ReadAndClose(cmd);
            List<User> userList = db.ConvertDataTable<User>(dt);
            if (userList.Count == 0)
            {
                return null;
            }
            return userList[0];
        }


        public int ActivateRegularAccount(User user)
        {
            string sql = "ActivateRegularAccount";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter email = new SqlParameter("@Email", SqlDbType.Text);
            email.Value = user.Email;
            cmd.Parameters.Add(email);

            SqlParameter password = new SqlParameter("@Password", SqlDbType.Text);
            password.Value = user.Password;
            cmd.Parameters.Add(password);

            //run the command
            return db.ExecuteAndClose(cmd);        
        }

        public User CheckIfFacebookUserIsExist(User user)
        {
            string sql = "CheckIfFacebookUserIsExist";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params

            SqlParameter email = new SqlParameter("@Email", SqlDbType.Text);
            email.Value = user.Email;
            cmd.Parameters.Add(email);

            SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.Text);
            firstName.Value = user.FirstName;
            cmd.Parameters.Add(firstName);

            SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.Text);
            lastName.Value = user.LastName;
            cmd.Parameters.Add(lastName);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = user.Img;
            cmd.Parameters.Add(img);

            cmd.Parameters.Add("@Result", SqlDbType.Int);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;

            //run the command
            DataTable dt = db.ReadAndClose(cmd);

            if (Convert.ToInt32(cmd.Parameters["@Result"].Value) == -1)
            {
                User temp = new User(-1, "", "", "", "", "", "", false, false);
                return temp;
            }
             
            List<User> userList = db.ConvertDataTable<User>(dt);
            if (userList.Count == 0)
            {
                return null;
            }
            return userList[0];
        }


        public User AddFacebookUser(User user)
        {
            string sql = "AddFacebookUser";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params

            SqlParameter email = new SqlParameter("@Email", SqlDbType.Text);
            email.Value = user.Email;
            cmd.Parameters.Add(email);

            SqlParameter firstName = new SqlParameter("@FirstName", SqlDbType.Text);
            firstName.Value = user.FirstName;
            cmd.Parameters.Add(firstName);

            SqlParameter lastName = new SqlParameter("@LastName", SqlDbType.Text);
            lastName.Value = user.LastName;
            cmd.Parameters.Add(lastName);

            SqlParameter phoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.Text);
            phoneNumber.Value = user.PhoneNumber;
            cmd.Parameters.Add(phoneNumber);

            SqlParameter img = new SqlParameter("@Img", SqlDbType.Text);
            img.Value = user.Img;
            cmd.Parameters.Add(img);

            //run the command
            DataTable dt = db.ReadAndClose(cmd);
            List<User> userList = db.ConvertDataTable<User>(dt);
            if (userList.Count == 0)
            {
                return null;
            }
            return userList[0];
        }

        public int DeleteUser(User user)
        {
            string sql = "DeleteUser";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter userId = new SqlParameter("@ID", SqlDbType.Int);
            userId.Value = user.ID;
            cmd.Parameters.Add(userId);

            //run the command
            return db.ExecuteAndClose(cmd);
        }

        public OtherProfileStartDetails OtherProfileStartDetails(Rate rateUser)
        {
            string sql = "otherProfileStartDetails";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter currentUserID = new SqlParameter("@currentUserID", SqlDbType.Int);
            currentUserID.Value = rateUser.CurrentUserID;
            cmd.Parameters.Add(currentUserID);

            SqlParameter otherUserID = new SqlParameter("@otherUserID", SqlDbType.Int);
            otherUserID.Value = rateUser.OtherUserID;
            cmd.Parameters.Add(otherUserID);

            DataTable dt = db.ReadAndClose(cmd);
            List<OtherProfileStartDetails> userList = db.ConvertDataTable<OtherProfileStartDetails>(dt);
            if (userList.Count == 0)
            {
                return null;
            }
            return userList[0];
        }

        public int HandleRating(Rate details)
        {
            string sql = "handleRating";
            SqlCommand cmd = db.CreateCommand(sql, db.Connect(), "proc");

            //create params
            SqlParameter otherUserID = new SqlParameter("@otherUserID", SqlDbType.Int);
            otherUserID.Value = details.OtherUserID;
            cmd.Parameters.Add(otherUserID);

            SqlParameter currentUserID = new SqlParameter("@currentUserID", SqlDbType.Int);
            currentUserID.Value = details.CurrentUserID;
            cmd.Parameters.Add(currentUserID);

            SqlParameter points = new SqlParameter("@points", SqlDbType.Int);
            points.Value = details.Points;
            cmd.Parameters.Add(points);

            cmd.Parameters.Add("@newAvg", SqlDbType.Int);
            cmd.Parameters["@newAvg"].Direction = ParameterDirection.Output;

            //run the command
            db.ExecuteAndClose(cmd);
            return Convert.ToInt32(cmd.Parameters["@newAvg"].Value);
        }

    }
}