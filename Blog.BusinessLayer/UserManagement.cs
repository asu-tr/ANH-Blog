using Blog.DataAccessLayer;
using Blog.Entities;
using System;

namespace Blog.BusinessLayer
{
    public class UserManagement
    {
        Repository<User> repoUser = new Repository<User>();

        public void SaveUser(User u)
        {
            User user = repoUser.Find(x => x.Username == u.Username || x.Email == u.Email);

            if (user != null)
            {
                ErrorLayer err = new ErrorLayer();

                if (u.Username == user.Username)
                    err.Errors.Add("Username exists.");

                if (u.Email == user.Email)
                    err.Errors.Add("E-mail exists.");

            }
            else
            {
                User newUser = new User();

                newUser.Username = u.Username;
                newUser.Email = u.Email;
                newUser.Password = u.Password;
                newUser.IsActive = false;
                newUser.IsAdmin = false;
                newUser.ActivationGuid = Guid.NewGuid();

                repoUser.Insert(newUser);
            }
        }
    }
}