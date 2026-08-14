using System;

namespace BruteForceDetector.Models
{
    public class SecurityReport
    {
        public string AttackType { get; set; }
        public string TargetUser { get; set; }
        public string SourceIp { get; set; }
        public int FailedAttempts { get; set; }
        public int TimeWindowMinutes { get; set; }
        public string RiskLevel { get; set; }
        public string Recommendation { get; set; }
        public DateTime DetectedAt { get; set; }
    }
}