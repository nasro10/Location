using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationVoiture.DAL
{
    public class CustomMemebershipProvider
    {
        readonly WebContext Context = new WebContext();


        public bool CreateUser(string username, string password, string email)
        {

            try
            {
                User NewUser = new User
                {
                    UserName = username,
                    Password = password,
                    Email = email,
                    SignUpDate = DateTime.UtcNow,
                };

                Context.Users.Add(NewUser);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool ValidateUser(string username, string password)
        {
            User User = null;
            User = Context.Users.FirstOrDefault(Usr => Usr.UserName == username && Usr.Password == password);

            if (User != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(string username)
        {
            User User = null;
            User = Context.Users.FirstOrDefault(Usr => Usr.UserName == username);
            if (User != null)
            {
                Context.Users.Remove(User);
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}