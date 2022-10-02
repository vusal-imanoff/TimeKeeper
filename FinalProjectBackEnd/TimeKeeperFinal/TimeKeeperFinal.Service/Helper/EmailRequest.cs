using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.Helper
{
    public class EmailRequest
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string SecretKey { get; set; }
    }
}
