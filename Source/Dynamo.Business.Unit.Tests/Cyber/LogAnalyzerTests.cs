﻿using Dynamo.Business.Shared.Cyber.LogAnalyzer;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Dynamo.Business.Unit.Tests.Cyber
{
    public class LogAnalyzerTests
    {
        [Test]
        public void Can_solve_TMYC_2025_spring_week_3_part_1()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("server_logs.json");
            var logEntries = JsonSerializer.Deserialize<List<LogEntry>>(stream);
            logEntries = logEntries.OrderBy(x => x.TimeStampString).ToList();
            Assert.That(logEntries.Count, Is.EqualTo(226));
            foreach (var logEntry in logEntries)
            {
                var xxxx = logEntry.StatusCode;
            }
            var earliestLoginFailed = logEntries.First(x => x.StatusCode == 401 || x.StatusCode == 403);
            Assert.That(earliestLoginFailed.TimeStampString, Is.EqualTo("2023-09-15T00:33:07.207000Z"));
        }

        [Test]
        public void Can_get_unauthorized_requests()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("server_logs.json");
            var logEntries = JsonSerializer.Deserialize<List<LogEntry>>(stream);
            var logAnalyzer = new LogAnalyzer(logEntries);
            var unauthorized = logAnalyzer.FindUnauthorizedRequests();
            Assert.That(unauthorized.Count, Is.EqualTo(4));
        }

        [Test]
        public void Can_get_repeated_failed_logins()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("server_logs.json");
            var logEntries = JsonSerializer.Deserialize<List<LogEntry>>(stream);
            var logAnalyzer = new LogAnalyzer(logEntries);
            var failedLogins = logAnalyzer.FindFailedLogins();
            Assert.That(failedLogins.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_get_ips_with_suspious_response_times()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("server_logs.json");
            var logEntries = JsonSerializer.Deserialize<List<LogEntry>>(stream);
            var logAnalyzer = new LogAnalyzer(logEntries);
            var suspiciousResponseTimes = logAnalyzer.FindSuspiciousResponseTimes();
            Assert.That(suspiciousResponseTimes.Count, Is.EqualTo(7));
        }

        [Test]
        public void Can_solve_TMYC_2025_spring_week_3_part_2()
        {
            var stream = FileGetter.GetMemoryStreamFromFile("server_logs.json");
            var logEntries = JsonSerializer.Deserialize<List<LogEntry>>(stream);
            var logAnalyzer = new LogAnalyzer(logEntries);
            var unauthorized = logAnalyzer.FindUnauthorizedRequests();
            var failedLogins = logAnalyzer.FindFailedLogins();
            var suspiciousResponseTimes = logAnalyzer.FindSuspiciousResponseTimes();

            // Find the IP that fits all three criteria
            var common = unauthorized.Intersect(failedLogins).Intersect(suspiciousResponseTimes).ToList();
            Assert.That(common.Count, Is.EqualTo(1));
            Assert.That(common[0], Is.EqualTo("45.33.49.78"));
        }
    }
}
