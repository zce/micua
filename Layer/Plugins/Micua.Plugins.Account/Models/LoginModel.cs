using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Micua.Plugins.Account.Account.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string Redirect { get; set; }
    }
}