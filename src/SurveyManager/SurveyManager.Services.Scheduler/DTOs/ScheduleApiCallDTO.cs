namespace SurveyManager.Services.Scheduler.DTOs
{
    public class ScheduleApiCallDTO : SchedulerDTO
    {
        public string apiAddress { get; set; }
        public string apiResource { get; set; }
        public string apiMethod { get; set; }
        public object apiPayload { get; set; }
    }
}
