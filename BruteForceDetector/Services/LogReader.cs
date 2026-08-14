using System;
using System.Collections.Generic;
using System.IO;
using BruteForceDetector.Models;

namespace BruteForceDetector.Services
{
    public class LogReader
    {
        public List<LoginAttempt> ReadLogs(string filePath)
        {
            List<LoginAttempt> attempts =
                new List<LoginAttempt>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                try
                {
                    string[] parts = line.Split('|');

                    if (parts.Length != 4)
                    {
                        Console.WriteLine(
                            "[WARNING] Geçersiz log formatı: " + line
                        );

                        continue;
                    }

                    DateTime timestamp;

                    if (!DateTime.TryParse(parts[0], out timestamp))
                    {
                        Console.WriteLine(
                            "[WARNING] Geçersiz tarih: " + line
                        );

                        continue;
                    }

                    string username = parts[1].Trim();
                    string ipAddress = parts[2].Trim();
                    string status = parts[3].Trim().ToUpper();

                    if (string.IsNullOrWhiteSpace(username) ||
                        string.IsNullOrWhiteSpace(ipAddress))
                    {
                        Console.WriteLine(
                            "[WARNING] Eksik kullanıcı veya IP: " + line
                        );

                        continue;
                    }

                    if (status != "FAILED" && status != "SUCCESS")
                    {
                        Console.WriteLine(
                            "[WARNING] Geçersiz login durumu: " + line
                        );

                        continue;
                    }

                    LoginAttempt attempt =
                        new LoginAttempt();

                    attempt.Timestamp = timestamp;
                    attempt.Username = username;
                    attempt.IpAddress = ipAddress;
                    attempt.IsSuccessful =
                        status == "SUCCESS";

                    attempts.Add(attempt);
                }
                catch
                {
                    Console.WriteLine(
                        "[WARNING] Log satırı okunamadı: " + line
                    );
                }
            }

            return attempts;
        }
    }
}