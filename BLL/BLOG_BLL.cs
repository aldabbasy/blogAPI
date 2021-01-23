using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VML;

namespace BLL
{
    public class BLOG_BLL
    {
        #region declarations 
        blogsDBEntities db = new blogsDBEntities();
        #endregion

        public List<Blog> getBlogs()
        {
            try
            {
                return db.Blogs.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int submitBlog(BlogViewModel blog)
        {
            try
            {
                using (var ctx = new blogsDBEntities())
                {
                    Blog _existingBlog = ctx.Blogs.Where(o => o.id == blog.id).FirstOrDefault();
                    var _blog = Mapper.Map<Blog>(blog);
                    int _bid = _blog.id;

                    if (_existingBlog != null)
                    {
                        ctx.Entry(_blog).State = EntityState.Modified;
                    }
                    else
                    {
                        _bid = generateBID();
                        _blog.id = _bid;
                        ctx.Blogs.Add(_blog);
                    }
                    ctx.SaveChanges();
                    return _bid;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int generateBID()
        {
            int bid = 0;
            if (db.Blogs.FirstOrDefault() != null)
            {
                bid = db.Blogs.Max(i => i.id) + 1;
            }
            else
            {
                bid = 1;
            }

            return bid;
        }
    }
}
