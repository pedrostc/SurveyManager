using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Services.Notifier.Notifiers
{
    public class SESNotifier : EmailNotifier
    {
        public override string SmtpAddress => "email-smtp.us-east-1.amazonaws.com";
        public override int SmtpPort => 587;
        public override bool UseSSL => true;
        public override string SmtpUser => "AKIAIVJPEWJWWQZUG3VQ";
        public override string SmtpPassword => "Ap+fp9MILSLi++itMCe7NX96d49YXvzsDTyo3XA2o4xF";

        public SESNotifier() : base() { }
    }
}
