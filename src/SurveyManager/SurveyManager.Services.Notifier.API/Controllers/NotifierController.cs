using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Newtonsoft.Json;
using SurveyManager.Services.Notifier.contract;
using SurveyManager.Services.Notifier.DTO;
using SurveyManager.Services.Notifier.Notifiers;

namespace SurveyManager.Services.Notifier.API.Controllers
{
    public class NotifierController : ApiController
    {
        public INotifier Notifier { get; set; }

        // POST: api/Notifier
        public object Post([FromBody]NotifierInvokeDTO msg)
        {
            try
            {
                Notifier = new SESNotifier();
                Notifier.Send(msg.Subject, msg.Body, msg.Destinations);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
