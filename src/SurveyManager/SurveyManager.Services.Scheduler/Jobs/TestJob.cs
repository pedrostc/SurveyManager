using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics;

namespace SurveyManager.Services.Scheduler.Jobs
{
    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {  



            System.Diagnostics.EventLog appLog =
                new System.Diagnostics.EventLog();
            appLog.Source = "SurveyManager";
            appLog.WriteEntry("An entry to the Application event log.");
        }
    }
}
