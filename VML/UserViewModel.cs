using System;

namespace VML
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
