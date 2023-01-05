using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Mail
{
    public class MailModel
    {
        public string SendMail { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string OrderNo { get; set; }
    }
}
