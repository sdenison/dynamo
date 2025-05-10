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
            return LogEntries
                .Where(x => x.StatusCode == 401 || x.StatusCode == 403)
                .GroupBy(x => x.IpAddress)
                .Where(g => g.Count() >= 5)
                .Select(g => g.Key)
                .ToList();
        }

        public List<string> FindSuspiciousResponseTimes()
        {
            return LogEntries.Where(x =>
                x.ResponseTime > 5000
                ).Select(x => x.IpAddress).Distinct().ToList();
        }
    }
}
