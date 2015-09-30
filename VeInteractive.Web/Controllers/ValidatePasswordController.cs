using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeInteractiveTest.Domain;

namespace VeInteractive.Web.Controllers
{
    public class ValidatePasswordController : ApiController
    {
        // POST api/ValidatePassword
        public bool Post([FromBody]UserDTO user)
        {
            var password = new MyToken();
            var pass = false;

            //try {
            pass = password.Validate(user.Password, user.UserID, DateTime.Now);
            //} catch { }

            return pass;
        }
    }
}