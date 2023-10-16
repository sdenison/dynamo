using System;

namespace Dynamo.Business.Shared.Utilities
{
    public class BackgroundJobEntity
    {
        public Guid Id { get; set; }
        public JobStatus JobStatus { get; set; }
        public JobType JobType { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string JobOutput { get; set; }
        public string FileName { get; set; }
    }
}
