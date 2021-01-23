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
    public class BlogsController : ApiController
    {
        #region declarations
        USER_BLL user_bll = new USER_BLL();
        BLOG_BLL blog_bll = new BLOG_BLL();
        #endregion

        [HttpPost]
        public object CreateBlog(BlogViewModel blog)
        {
            try
            {
                var _generatedID = blog_bll.submitBlog(blog);

                return new { id = _generatedID };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
