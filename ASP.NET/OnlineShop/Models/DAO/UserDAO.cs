using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;
namespace Models.DAO
{
    /// <summary>
    /// Dùng để làm với với Database
    /// </summary>
    public class UserDAO
    {

        OnlineShopDbContext dbContext = null;

        private static UserDAO instance;

        public static UserDAO Instance
        {
            get { if (instance == null) instance = new UserDAO(); return instance; }
            private set { instance = value; }
        }

        private UserDAO()
        {
            dbContext = new OnlineShopDbContext();
        }

        public string Login(string userName, string passWord)
        {
            var result = dbContext.User.SingleOrDefault(m => m.UserName.Equals(userName));

            if (result == null)
            {
                return Resource.Login_UserNotExist;
            }
            else
            {
                if (result.UserName != userName)
                {
                    return Resource.Login_UserNameDiffernce;
                }
                else if (result.Password != passWord)
                {
                    return Resource.Login_PasswordDiffernce;
                }
                else
                {
                    if (result.Status == false)
                    {
                        return Resource.Login_UserLock;
                    }
                    else
                    {
                        return Resource.Login_Success;
                    }

                }
            }
        }

        public long Insert(User user)
        {
            var result = dbContext.User.SingleOrDefault(m => m.UserName.Equals(user.UserName));
            if (result != null && result.ID > 0)
            {
                return -1;
            }
            else
            {
                dbContext.User.Add(user);
                dbContext.SaveChanges();
            }

            return user.ID;
        }


        public User GetByUserName(string userName)
        {
            return dbContext.User.SingleOrDefault(m => m.UserName == userName);
        }

        public User GetByUserID(long ID)
        {
            return dbContext.User.Find(ID);
        }

        public IEnumerable<User> GetListAllUser(string searchString, int pageNumer, int pageSize)
        {
            IQueryable<User> model = dbContext.User.OrderByDescending(m => m.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.UserName.Contains(searchString) || m.Name.Contains(searchString));
            }
            return model.OrderByDescending(m => m.CreateDate).ToPagedList(pageNumer, pageSize);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user">user you want to update</param>
        /// <returns></returns>
        public bool Update(User user)
        {
            try
            {

                var UserUpdate = dbContext.User.Find(user.ID);

                if (!string.IsNullOrEmpty(user.Password))
                {
                    UserUpdate.Password = user.Password;
                }

                UserUpdate.Name = user.Name;
                UserUpdate.Address = user.Address;
                UserUpdate.Email = user.Email;
                UserUpdate.Phone = user.Phone;
                UserUpdate.Status = user.Status;
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {

            try
            {
                var user = dbContext.User.Find(id);
                dbContext.User.Remove(user);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ChangeStatus(long userID)
        {
            var user = dbContext.User.Find(userID);
            user.Status = !user.Status;
            dbContext.SaveChanges();

            return user.Status;
        }
    }
}
