using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Business.Utilities
{
    public enum JobStatus
    {
        Initializing,
        Submitted,
        Running,
        FinishedSuccess,
        FinishedError,
    }
}
