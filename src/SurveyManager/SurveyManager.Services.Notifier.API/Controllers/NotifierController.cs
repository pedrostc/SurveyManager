using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace SurveyManager.Services.Notifier.API.Controllers
{
    public class NotifierController : ApiController
    {
        // GET: api/Notifier
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notifier/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notifier
        public object Post([FromBody]string value)
        {
            return JsonConvert.DeserializeObject(value);
        }

        // PUT: api/Notifier/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notifier/5
        public void Delete(int id)
        {
        }
    }
}
