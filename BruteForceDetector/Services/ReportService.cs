using System;
using System.Collections.Generic;
using System.IO;
using BruteForceDetector.Models;

namespace BruteForceDetector.Services
{
    public class ReportService
    {
        public void SaveReports(
            List<SecurityReport> reports,
            string filePath)
        {
            string json = "[\n";

            for (int i = 0; i < reports.Count; i++)
            {
                SecurityReport report = reports[i];

                json += "  {\n";

                json += "    \"AttackType\": \"" +
                        Escape(report.AttackType) + "\",\n";

                json += "    \"TargetUser\": \"" +
                        Escape(report.TargetUser) + "\",\n";

                json += "    \"SourceIp\": \"" +
                        Escape(report.SourceIp) + "\",\n";

                json += "    \"FailedAttempts\": " +
                        report.FailedAttempts + ",\n";

                json += "    \"TimeWindowMinutes\": " +
                        report.TimeWindowMinutes + ",\n";

                json += "    \"RiskLevel\": \"" +
                        Escape(report.RiskLevel) + "\",\n";

                json += "    \"Recommendation\": \"" +
                        Escape(report.Recommendation) + "\",\n";

                json += "    \"DetectedAt\": \"" +
                        report.DetectedAt.ToString("yyyy-MM-dd HH:mm:ss") +
                        "\"\n";

                json += "  }";

                if (i < reports.Count - 1)
                {
                    json += ",";
                }

                json += "\n";
            }

            json += "]";

            File.WriteAllText(filePath, json);
        }

        private string Escape(string value)
        {
            if (value == null)
            {
                return "";
            }

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
        }
    }
}