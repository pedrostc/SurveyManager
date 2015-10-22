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
using System.Reflection;
using Quartz.Impl.Matchers;

namespace SurveyManager.Services.Scheduler.App
{
    public class SchedulerApp
    {
        const string JOB_GROUP = "SurveyManager";

        public IScheduler sched { get; set; }

        public SchedulerApp()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            sched = schedFact.GetScheduler();
        }

        private void RegisterJob(IJobDetail job, ITrigger trigger)
        {
            sched.ScheduleJob(job, trigger);
        }

        private Dictionary<string, object> CreateJobDataMapDictionary(object source)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Type sourceType = source.GetType();

            foreach(PropertyInfo pInfo in sourceType.GetProperties())
            {
                result.Add(pInfo.Name, pInfo.GetValue(source).ToString());
            }

            return result;
        }

        private IJobDetail CreateJobDetail(SchedulerDTO dto)
        {
            Dictionary<string, object> jobData = CreateJobDataMapDictionary(dto);

            IJobDetail job = JobBuilder.Create<CallApiJob>()
                .WithIdentity(dto.jobName, dto.jobGroup)
                .Build();
            
           foreach(KeyValuePair<string, object> data in jobData)
            {
                job.JobDataMap.Add(data);
            }

            return job;
        }

        private ITrigger CreateJobTrigger(SchedulerDTO dto)
        {
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity(string.Format("Trigger - {0}", dto.jobName), dto.jobGroup)
             .StartAt(new DateTimeOffset(dto.runAt))
             .Build();

            return trigger;
        }

        public void RegisterApiCallJob(ScheduleApiCallDTO dto)
        {
            IJobDetail job = CreateJobDetail(dto);
            ITrigger trigger = CreateJobTrigger(dto);

            RegisterJob(job, trigger);
        }

        public List<JobInfoDTO> ListJobs()
        {
            List<JobInfoDTO> result = new List<JobInfoDTO>();

            IList<string> jobGroups = sched.GetJobGroupNames();

            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = sched.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = sched.GetJobDetail(jobKey);
                    result.Add(new JobInfoDTO()
                    {
                        Name = jobKey.Name,
                        Group = jobKey.Group,
                        Type = detail.JobType.Name
                    });
                }
            }

            return result;
        }

        public JobInfoDTO GetJob(FindJobDTO dto)
        {
            var jobMatcher = GroupMatcher<JobKey>.GroupEquals(dto.GroupName);
            var jobKeys = sched.GetJobKeys(jobMatcher);

            var key = jobKeys.FirstOrDefault(k => k.Name == dto.JobName);

            var detail = sched.GetJobDetail(key);

            return new JobInfoDTO()
            {
                Name = detail.Key.Name,
                Group = detail.Key.Group,
                Type = detail.JobType.Name
            };

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
