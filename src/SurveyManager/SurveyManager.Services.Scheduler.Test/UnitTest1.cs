using System;
using SurveyManager.Services.Scheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SurveyManager.Services.Scheduler.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            App.SchedulerManager sman = new App.SchedulerManager();
            var result = sman.GetTasks();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddTaskTest()
        {
            App.SchedulerManager sman = new App.SchedulerManager();
            var result = sman.RegisterTask("teste", "notepad", new DateTime(2015, 10, 21, 10, 33, 40));

            Assert.IsNotNull(result);
        }
    }
}
