using System;
using System.Collections.Generic;
using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Csla;
using Dynamo.Business.Utilities;
using Dynamo.Config;

namespace Dynamo.Commands.Utilities
{
    public class RunBackgroundJobCommand : Command
    {
        public RunBackgroundJobCommand() : base("runbackgroundjob", "Runs background jobs")
        {
            var jobIdOption = CreateJobIdOption();
            this.SetHandler(RunBackgroundJob, jobIdOption);
        }

        public void RunBackgroundJob(string jobId)
        {
            var provider = DependencyInjection.GetBaseServices();
            var dataPortal = provider.GetService<IDataPortal<BackgroundJob>>();
            var backgroundJob = dataPortal.Fetch(jobId);
            backgroundJob.JobStatus = JobStatus.Running;
            backgroundJob = backgroundJob.Save();
        }

        private Option<string> CreateJobIdOption()
        {
            var description = "Job ID to run";
            var option = new Option<string>(new string[2] { "--job-id", "-j" }, description);
            option.IsRequired = true;
            option.Arity = ArgumentArity.ExactlyOne;
            Add(option);
            return option;
        }
    }
}
