# Brute Force Detector - Security Log Analysis System

A lightweight cybersecurity tool built with C# that analyzes authentication logs and detects potential brute-force attacks in real time.

The system identifies repeated failed login attempts based on usernames, IP addresses, and time windows, then evaluates the detected activity with risk levels and security recommendations. It also generates automated JSON security reports for further analysis.

## Key Features

- **Brute-Force Detection:** Identifies repeated failed login attempts from the same IP address and targeting the same user.
- **IP & User Analysis:** Correlates authentication attempts using source IP addresses and usernames.
- **Time-Window Analysis:** Detects suspicious login activity within a configurable time window.
- **Risk Assessment:** Classifies detected attacks as LOW, MEDIUM, HIGH, or CRITICAL.
- **Security Recommendations:** Provides recommended actions based on the detected risk level.
- **Multiple Attack Detection:** Detects multiple brute-force attacks within the same log file.
- **Invalid Log Handling:** Safely handles malformed, incomplete, or invalid log entries without crashing.
- **JSON Security Reports:** Automatically generates structured security reports containing detected attack details.
- **Console-Based Security Monitoring:** Provides a clear terminal interface for security analysis.

## Technologies Used

- **Language:** C#
- **Framework:** .NET
- **Architecture:** Object-Oriented Programming
- **Data Processing:** File I/O & Log Parsing
- **Reporting:** JSON
- **Security Concepts:** Brute-Force Detection, Authentication Monitoring, Risk Assessment

## Project Structure

```text
BruteForceDetector/
│
├── Detectors/
│   └── BruteForceDetector.cs
│       # Brute-force attack detection logic
│
├── Models/
│   ├── LoginAttempt.cs
│   │   # Authentication attempt model
│   │
│   └── SecurityReport.cs
│       # Security report model
│
├── Services/
│   ├── LogReader.cs
│   │   # Authentication log parser
│   │
│   └── ReportService.cs
│       # JSON security report generator
│
├── Logs/
│   └── login.log
│       # Sample authentication logs
│
├── Reports/
│   └── security-report.json
│       # Generated security report
│
└── Program.cs
    # Application entry point
