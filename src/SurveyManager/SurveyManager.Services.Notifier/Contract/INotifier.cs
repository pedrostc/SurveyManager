using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Services.Notifier.contract
{
    public interface INotifier
    {
        void Send(string subject, string body, List<string> destinations);
        void SendHtml(string subject, string body, List<string> destinations);
    }
}
