using Blog.Common;
using Blog.DataAccessLayer;
using Blog.Entities;
using Blog.Entities.ViewModels;
using Makale.Common;
using System;

namespace Blog.BusinessLayer
{
    public class UserManagement
    {
        Repository<User> repoUser = new Repository<User>();
        ResultManagement<User> result = new ResultManagement<User>();

        public ResultManagement<User> ActivateUser(Guid id)
        {

            result.Obj = repoUser.Find(x => x.ActivationGuid == id);

            if (result.Obj != null)
            {
                if (result.Obj.IsActive)
                    result.Results.Add("User is already active.");

                result.Obj.IsActive = true;
                repoUser.Update();
            }
            else
                result.Results.Add("No user found.");

            return result;
        }

        public ResultManagement<User> DeleteUser(int id)
        {
            User u = repoUser.Find(x => x.Id == id);

            if (u != null)
            {
                int sonuc = repoUser.Delete(u);

                if (sonuc == 0)
                {
                    result.Results.Add("User couldn't be deleted.");
                    return result;
                }
            }
            else
            {
                result.Results.Add("User not found.");
            }
            return result;
        }

        public ResultManagement<User> GetUser(int id)
        {
            result.Obj = repoUser.Find(x => x.Id == id);

            if (result.Obj == null)
                result.Results.Add("User not found.");

            return result;
        }

        public ResultManagement<User> LogIn(LogInVM model)
        {
            result.Obj = repoUser.Find(x => x.Username == model.Username && x.Password == model.Password);

            if (result.Obj != null)
            {
                if (!result.Obj.IsActive)
                    result.Results.Add("User not active. Please check your e-mail.");
            }
            else
                result.Results.Add("Username or password is invalid");

            return result;
        }

        public ResultManagement<User> SaveUser(RegisterVM model)
        {
            result.Obj = repoUser.Find(x => x.Username == model.Username || x.Email == model.Email);

            if (result.Obj != null)
            {
                if (result.Obj.Username == model.Username)
                    result.Results.Add("Username exists.");

                if (result.Obj.Email == model.Email)
                    result.Results.Add("E-mail exist.");
            }

            else
            {
                int insertResult = repoUser.Insert(new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    ActivationGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                });

                if (insertResult > 0)
                {
                    result.Obj = repoUser.Find(x => x.Username == model.Username && x.Email == model.Email);

                    // Send activation mail

                    string siteURL = ConfigHelper.Get<string>("SiteRootUri");

                    string activateURL = $"{siteURL}/Home/UserActivate/{result.Obj.ActivationGuid}";

                    string body = $"Hello{result.Obj.Username} <br>Click <a href='{activateURL}' target='_blank'> to activate your account</a>";
                    MailHelper.SendMail(body, result.Obj.Email, "Account Activation");
                }
            }
            return result;
        }

        public ResultManagement<User> UpdateUser(User model)
        {
            User user = repoUser.Find(x => x.Username == model.Username || x.Email == model.Email);

            if (user != null && user.Id != model.Id)
            {
                if (user.Username == model.Username)
                    result.Results.Add("Username exists.");

                if (user.Email == model.Email)
                    result.Results.Add("E-mail exists.");

                return result;
            }

            result.Obj = repoUser.Find(x => x.Id == model.Id);
            result.Obj.Email = model.Email;
            result.Obj.Username = model.Username;
            result.Obj.Password = model.Password;

            if (string.IsNullOrEmpty(model.ProfileImageFile) == false)
                result.Obj.ProfileImageFile = model.ProfileImageFile;

            int res = repoUser.Update();

            if (res == 0)
                result.Results.Add("Profile couldn't be updated.");

            return result;
        }
    }
}