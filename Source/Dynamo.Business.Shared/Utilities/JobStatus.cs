using System.ComponentModel;

namespace Dynamo.Business.Shared.Utilities
{
    public enum JobStatus
    {
        [Description("Initializing")]
        Initializing,
        [Description("Sent to S3")]
        Submitted,
        [Description("Job Running")]
        Running,
        [Description("Finished!")]
        FinishedSuccess,
        [Description("Error!")]
        FinishedError,
    }
}
