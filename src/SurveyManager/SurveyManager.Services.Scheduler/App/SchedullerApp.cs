using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

using SurveyManager.Services.Scheduler.Jobs;
using SurveyManager.Services.Scheduler.DTOs;

using Newtonsoft.Json;

namespace SurveyManager.Services.Scheduler.App
{
    public class SchedulerApp
    {
        public IScheduler sched { get; set; }

        public SchedulerApp()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            sched = schedFact.GetScheduler();
        }

        public void SchedulleJob()
        {
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<TestJob>()
                .WithIdentity("JobTeste", "SurveyManager")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("TriggerTeste", "SurveyManager")
              .StartAt(new DateTimeOffset(new DateTime(2015,10,21,18,37,40)))
              .Build();

            sched.ScheduleJob(job, trigger);
        }

        public void RegisterOneTimeJob(IJob job, DateTime runAt)
        {
            Guid jobId = Guid.NewGuid();            
        }

        public void RegisterApiCallJob(ScheduleApiCallDTO dto, DateTime runAt)
        {
            Guid id = Guid.NewGuid();

            IJobDetail job = JobBuilder.Create<CallApiJob>()
                .WithIdentity(string.Format("API Call - {0}", id), "SurveyManager")
                .UsingJobData("apiAddress", dto.apiAddress)
                .UsingJobData("apiMethod", dto.apiMethod)
                .UsingJobData("apiResource", dto.apiResource)
                .UsingJobData("apiPayload", JsonConvert.SerializeObject(dto.apiPayload))
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity(string.Format("API Call Trigger - {0}", id ), "SurveyManager")
              .StartAt(new DateTimeOffset(runAt))
              .Build();

                sched.ScheduleJob(job, trigger);
        }

        public List<IJob> ListJobs()
        {
            throw new NotImplementedException();
        }

        public IJob GetJob(string jobName)
        {
            throw new NotImplementedException();
        }

        public void DeleteJob()
        {
            throw new NotImplementedException();
        }

        public void UpdateJobSchedule(IJob job, ITrigger newTrigger)
        {
            throw new NotImplementedException();
        }
    }
}
