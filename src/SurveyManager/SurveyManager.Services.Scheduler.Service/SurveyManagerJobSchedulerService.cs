using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

using SurveyManager.Services.Scheduler.Jobs;

namespace SurveyManager.Services.Scheduler.Service
{
    public partial class SurveyManagerJobSchedulerService : ServiceBase
    {
        public SurveyManagerJobSchedulerService()
        {
            InitializeComponent();
        }

        public IScheduler sched { get; set; }
        protected override void OnStart(string[] args)
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            sched = schedFact.GetScheduler();
            sched.Start();
        }

        protected override void OnStop()
        {
            sched.Shutdown();
        }
    }
}
