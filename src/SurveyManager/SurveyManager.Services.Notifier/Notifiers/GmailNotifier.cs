using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Services.Notifier.Notifiers
{
    public class GmailNotifier : EmailNotifier
    {
        public override string SmtpAddress => "smtp.gmail.com";
        public override int SmtpPort => 587;
        public override bool UseSSL => true;
        public override string SmtpUser => "pedrost.rj@gmail.com";
        public override string SmtpPassword => "tii0ou@8";

        public GmailNotifier() : base() { }
    }
}
