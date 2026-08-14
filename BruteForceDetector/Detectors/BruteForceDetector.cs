using System;
using System.Collections.Generic;
using System.Linq;
using BruteForceDetector.Models;

namespace BruteForceDetector.Detectors
{
    public class BruteForceDetector
    {
        private readonly int _timeWindowMinutes;

        public BruteForceDetector(int timeWindowMinutes)
        {
            _timeWindowMinutes = timeWindowMinutes;
        }

        public List<SecurityReport> Analyze(List<LoginAttempt> loginAttempts)
        {
            List<SecurityReport> reports = new List<SecurityReport>();

            var failedAttempts = loginAttempts
                .Where(x => !x.IsSuccessful)
                .GroupBy(x => new
                {
                    x.Username,
                    x.IpAddress
                });

            foreach (var group in failedAttempts)
            {
                List<LoginAttempt> attempts = group
                    .OrderBy(x => x.Timestamp)
                    .ToList();

                for (int i = 0; i < attempts.Count; i++)
                {
                    DateTime startTime = attempts[i].Timestamp;

                    List<LoginAttempt> attemptsInWindow = attempts
                        .Where(x =>
                            x.Timestamp >= startTime &&
                            x.Timestamp <= startTime.AddMinutes(_timeWindowMinutes))
                        .ToList();

                    if (attemptsInWindow.Count >= 3)
                    {
                        int failedCount = attemptsInWindow.Count;

                        string riskLevel =
                            CalculateRiskLevel(failedCount);

                        string recommendation =
                            GetRecommendation(riskLevel);

                        SecurityReport report = new SecurityReport();

                        report.AttackType = "Brute Force";
                        report.TargetUser = group.Key.Username;
                        report.SourceIp = group.Key.IpAddress;
                        report.FailedAttempts = failedCount;
                        report.TimeWindowMinutes = _timeWindowMinutes;
                        report.RiskLevel = riskLevel;
                        report.Recommendation = recommendation;
                        report.DetectedAt = DateTime.Now;

                        reports.Add(report);

                        break;
                    }
                }
            }

            return reports;
        }

        private string CalculateRiskLevel(int failedCount)
        {
            if (failedCount >= 10)
            {
                return "CRITICAL";
            }

            if (failedCount >= 7)
            {
                return "HIGH";
            }

            if (failedCount >= 5)
            {
                return "MEDIUM";
            }

            return "LOW";
        }

        private string GetRecommendation(string riskLevel)
        {
            if (riskLevel == "CRITICAL")
            {
                return "Immediately block IP and lock account";
            }

            if (riskLevel == "HIGH")
            {
                return "Temporarily block IP";
            }

            if (riskLevel == "MEDIUM")
            {
                return "Enable rate limiting";
            }

            return "Monitor suspicious activity";
        }
    }
}