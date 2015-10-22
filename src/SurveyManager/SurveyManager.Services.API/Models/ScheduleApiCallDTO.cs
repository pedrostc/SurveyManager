using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Services.API.Models
{
    public class ScheduleApiCallDTO
    {
        public string apiAddress { get; set; }
        public string apiResource { get; set; }
        public string apiMethod { get; set; }
        public object apiPayload { get; set; }
    }
}
