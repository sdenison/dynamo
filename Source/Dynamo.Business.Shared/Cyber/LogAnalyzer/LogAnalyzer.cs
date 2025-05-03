using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.Cyber.LogAnalyzer
{
    public class LogAnalyzer
    {
        private List<LogEntry> LogEntries { get; set; }

        public LogAnalyzer(List<LogEntry> logEntries)
        {
            LogEntries = logEntries.OrderBy(x => x.TimeStampString).ToList();
        }

        public List<string> FindUnauthorizedRequests()
        {
            return LogEntries.Where(x =>
                x.Endpoint.StartsWith("/admin/config") ||
                x.Endpoint.StartsWith("/admin/users") ||
                x.Endpoint.StartsWith("/sensitive")
                ).Select(x => x.IpAddress).Distinct().ToList();
        }

        public List<string> FindFailedLogins()
        {
            var failedLogins = new Dictionary<string, int>();
            foreach (var logEntry in LogEntries.Where(x => x.StatusCode == 401 || x.StatusCode == 403))
            {
                if (!failedLogins.ContainsKey(logEntry.IpAddress))
                    failedLogins.Add(logEntry.IpAddress, 1);
                else
                    failedLogins[logEntry.IpAddress] += 1;
            }
            var ipList = new List<string>();
            foreach (var ip in failedLogins.Keys)
            {
                if (failedLogins[ip] >= 5)
                {
                    ipList.Add(ip);
                }
            }
            return ipList;
        }

        public List<string> FindSuspiciousResponseTimes()
        {
            return LogEntries.Where(x =>
                x.ResponseTime > 5000
                ).Select(x => x.IpAddress).Distinct().ToList();
        }
    }
}
