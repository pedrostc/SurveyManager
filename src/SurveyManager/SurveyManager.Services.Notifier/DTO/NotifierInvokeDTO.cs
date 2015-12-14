using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Services.Notifier.DTO
{
    public class NotifierInvokeDTO
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Destinations { get; set; }  
    }
}
