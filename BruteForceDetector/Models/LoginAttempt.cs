using System;

namespace BruteForceDetector.Models
{
    public class LoginAttempt
    {
        public string Username { get; set; }

        public string IpAddress { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsSuccessful { get; set; }
    }
}