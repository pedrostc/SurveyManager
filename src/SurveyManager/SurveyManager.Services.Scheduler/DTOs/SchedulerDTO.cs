using System;

namespace SurveyManager.Services.Scheduler.DTOs
{
    public abstract class SchedulerDTO
    {
        public string jobGroup { get; set; }
        public string jobName { get; set; }
        public DateTime runAt { get; set; }
    }
}
