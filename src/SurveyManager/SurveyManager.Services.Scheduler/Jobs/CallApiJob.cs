using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using RestSharp;
using Newtonsoft.Json;

namespace SurveyManager.Services.Scheduler.Jobs
{
    public class CallApiJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
            appLog.Source = "SurveyManagerSchedulerJob";

            var dataMap = context.JobDetail.JobDataMap;            
            try
            {
                RestClient client = new RestClient(dataMap["apiAddress"].ToString());
                RestRequest request = new RestRequest(dataMap["apiResource"].ToString(), DecodeApiMethod(dataMap));
                request.AddJsonBody((dataMap["apiPayload"].ToString()));

                IRestResponse response = client.Execute(request);

                appLog.WriteEntry(string.Format("A api foi chamada e retornou: {0}", response.Content));
            }
            catch(Exception ex)
            {
                appLog.WriteEntry(string.Format("Ocorreu um erro ao chamar a api - {0}: \r\n {1}", ex.Message, ex.StackTrace));
            }
        }

        private Method DecodeApiMethod(JobDataMap dataMap)
        {
            switch(dataMap["apiMethod"].ToString().ToLower())
            {
                case "post":
                    return Method.POST;
                case "get":
                    return Method.GET;
                case "put":
                    return Method.PUT;
                case "delete":
                    return Method.DELETE;
                default:
                    throw new Exception("O método cadastrado não é compatível.");
            }
        }
    }
}
