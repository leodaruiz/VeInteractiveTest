using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeInteractiveTest.Domain;

namespace VeInteractive.Web.Controllers
{
    public class CreatePasswordController : ApiController
    {
        // POST api/CreatePassword
        public string Post([FromBody]string userId)
        {
            var password = new MyToken();
            var expirationDate = DateTime.Now.AddSeconds(30);
            var pass = password.Generate(userId, expirationDate);

            return pass;
        }

    }
}