using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using VML;
using DAL;

namespace blogAPI.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        #region declarations
        USER_BLL user_bll = new USER_BLL();
        BLOG_BLL blog_bll = new BLOG_BLL();
        #endregion

        [HttpPost]
        public object InitialData()
        {
            try
            {
                var _users = user_bll.getUsers();
                var _blogs = blog_bll.getBlogs();
                return new { users = _users, blogs = _blogs };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public object Register(UserViewModel user)
        {
            try
            {
                user.password = USER_BLL.GenerateSHA256String(user.password);
                var _generatedID = user_bll.registerUser(user);

                return new { id = _generatedID, password = user.password };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int Login(LoginViewModel user)
        {
            try
            {
                user.password = USER_BLL.GenerateSHA256String(user.password);
                var _user = user_bll.authenticateUser(user);

                return _user.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
