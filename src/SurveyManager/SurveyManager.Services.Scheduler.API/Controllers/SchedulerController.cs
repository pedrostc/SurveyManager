using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SurveyManager.Services.Scheduler.App;
using SurveyManager.Services.Scheduler.DTOs;

namespace SurveyManager.Services.Scheduler.API.Controllers
{
    public class SchedulerController : ApiController
    {
        // GET: api/Scheduler
        public IEnumerable<JobInfoDTO> Get()
        {
            SchedulerApp app = new SchedulerApp();
            return app.ListJobs();
        }

        // GET: api/Scheduler/5
        public JobInfoDTO Get(string name, string groupName)
        {
            SchedulerApp app = new SchedulerApp();
            return app.GetJob(new FindJobDTO()
            {
                JobName = name,
                GroupName = groupName
            });
        }

        // POST: api/Scheduler
        public object Post([FromBody]ScheduleApiCallDTO value)
        {
            SchedulerApp app = new SchedulerApp();
            app.RegisterApiCallJob(value);
            return value.apiPayload;
        }

        // PUT: api/Scheduler/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Scheduler/5
        public void Delete(int id)
        {
        }
    }
}
