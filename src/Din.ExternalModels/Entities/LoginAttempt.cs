using System;
using System.Collections.Generic;
using System.Text;
using UAParser;

namespace Din.ExternalModels.Entities
{
    public class LoginAttempt
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string PublicIp { get; set; }
        public DateTime DateAndTime { get; set; }
        public LoginStatus Status { get; set; }

        public LoginAttempt()
        {

        }

        public LoginAttempt(string username, string device, string os, string browser, string publicIp,
            DateTime dateAndTime, LoginStatus status)
        {
            Username = username;
            Device = device;
            Os = os;
            Browser = browser;
            PublicIp = publicIp;
            DateAndTime = dateAndTime;
            Status = status;
        }
    }

    public enum LoginStatus
    {
        Failed,
        Success
    }
}
