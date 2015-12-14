using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Services.Notifier.contract;

namespace SurveyManager.Services.Notifier
{
    public class NotifierApp
    {
        public INotifier Notifier { get; set; }

        public void Notificar(string msg, List<string> destinatarios)
        {
            try
            {
                //Notifier.Send(msg, destinatarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> Listar()
        {
            List<string> result = new List<string>();
            try
            {
                //result = Notifier.List();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
