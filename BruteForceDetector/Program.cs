using System;
using System.Collections.Generic;
using System.IO;
using BruteForceDetector.Models;
using BruteForceDetector.Detectors;
using BruteForceDetector.Services;

namespace BruteForceDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("       BRUTE FORCE DETECTOR");
            Console.WriteLine("====================================");
            Console.WriteLine();

            string logPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs",
                "login.log"
            );

            if (!File.Exists(logPath))
            {
                Console.WriteLine("Log dosyası bulunamadı!");
                Console.WriteLine(logPath);
                Console.ReadLine();
                return;
            }

            LogReader logReader = new LogReader();

            List<LoginAttempt> loginAttempts =
                logReader.ReadLogs(logPath);

            Console.WriteLine(
                loginAttempts.Count + " login kaydı okundu."
            );

            Console.WriteLine();
            Console.WriteLine("Login kayıtları analiz ediliyor...");
            Console.WriteLine();

            foreach (LoginAttempt attempt in loginAttempts)
            {
                string status;

                if (attempt.IsSuccessful)
                {
                    status = "SUCCESS";
                }
                else
                {
                    status = "FAILED";
                }

                Console.WriteLine(
                    "{0} | User: {1} | IP: {2} | Status: {3}",
                    attempt.Timestamp.ToString("HH:mm:ss"),
                    attempt.Username,
                    attempt.IpAddress,
                    status
                );
            }

            global::BruteForceDetector.Detectors.BruteForceDetector detector =
                new global::BruteForceDetector.Detectors.BruteForceDetector(5);

            List<SecurityReport> reports =
                detector.Analyze(loginAttempts);

            Console.WriteLine();

            if (reports.Count > 0)
            {
                foreach (SecurityReport report in reports)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("        !!! BRUTE FORCE DETECTED !!!");
                    Console.WriteLine("==============================================");

                    Console.WriteLine(
                        "Attack Type       : " + report.AttackType
                    );

                    Console.WriteLine(
                        "Target User       : " + report.TargetUser
                    );

                    Console.WriteLine(
                        "Source IP         : " + report.SourceIp
                    );

                    Console.WriteLine(
                        "Failed Attempts   : " + report.FailedAttempts
                    );

                    Console.WriteLine(
                        "Time Window       : " +
                        report.TimeWindowMinutes +
                        " minutes"
                    );

                    Console.WriteLine(
                        "Risk Level        : " + report.RiskLevel
                    );

                    Console.WriteLine(
                        "Recommendation    : " +
                        report.Recommendation
                    );

                    Console.WriteLine(
                        "Detected At       : " +
                        report.DetectedAt
                    );

                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                }

                string reportsFolder = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Reports"
                );

                Directory.CreateDirectory(reportsFolder);

                string reportPath = Path.Combine(
                    reportsFolder,
                    "security-report.json"
                );

                ReportService reportService =
                    new ReportService();

                reportService.SaveReports(
                    reports,
                    reportPath
                );

                Console.WriteLine(
                    "Toplam saldırı sayısı: " + reports.Count
                );

                Console.WriteLine();

                Console.WriteLine(
                    "Security report oluşturuldu:"
                );

                Console.WriteLine(reportPath);
            }
            else
            {
                Console.WriteLine(
                    "Herhangi bir brute force saldırısı tespit edilmedi."
                );
            }

            Console.WriteLine();
            Console.WriteLine("Program tamamlandı.");
            Console.ReadLine();
        }
    }
}