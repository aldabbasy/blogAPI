using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using VML;

namespace BLL
{
    public class USER_BLL
    {
        #region declarations 
        blogsDBEntities db = new blogsDBEntities();
        #endregion

        #region hash functions
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        public static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        #endregion

        public List<User> getUsers()
        {
            try
            {
                return db.Users.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public User authenticateUser(LoginViewModel user)
        {
            try
            {
                using (var ctx = new blogsDBEntities())
                {
                    User _existinguser = ctx.Users.Where(o => o.username == user.username && o.password == user.password).FirstOrDefault();
                    if (_existinguser != null)
                    {
                        return _existinguser;
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int registerUser(UserViewModel user)
        {
            try
            {
                using (var ctx = new blogsDBEntities())
                {
                    User _existinguser = ctx.Users.Where(o => o.id == user.id).FirstOrDefault();
                    var _user = Mapper.Map<User>(user);
                    int _uid = _user.id;
                    
                    if (_existinguser != null)
                    {
                        ctx.Entry(_user).State = EntityState.Modified;
                    }
                    else
                    {
                        _uid = generateUID();
                        _user.id = _uid;
                        ctx.Users.Add(_user);
                    }
                    ctx.SaveChanges();
                    return _uid;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private int generateUID()
        {
            int uid = 0;
            if (db.Users.FirstOrDefault() != null)
            {
                uid = db.Users.Max(i => i.id) + 1;
            }
            else
            {
                uid = 1;
            }

            return uid;
        }
    }
}
