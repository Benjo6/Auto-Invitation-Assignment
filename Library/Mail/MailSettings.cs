using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mail
{
    public class MailSettings
    {
        public String Mail { get; set; }
        public String DisplayName { get; set; }
        public String Password { get; set; }
        public String Host { get; set; }
        public int Port { get; set; }
    }
}
