using System.CommandLine;
using System.Diagnostics;
using Amazon.S3.Model;
using Microsoft.Extensions.DependencyInjection;
using Csla;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Config;
using Dynamo.IO.S3.Models;
using Dynamo.IO.S3.Services;

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
            try
            {
                Console.WriteLine($"Running job for file {jobId}");
                var provider = DependencyInjection.GetBaseServices();
                var dataPortal = provider.GetService<IDataPortal<BackgroundJob>>();
                var backgroundJob = dataPortal?.Fetch(jobId);

                var storageService = new StorageService();
                var s3Request = new Dynamo.IO.S3.Models.S3Object()
                {
                    BucketName = "test-dynamo-file-store2",
                    Name = backgroundJob?.Id.ToString()
                };
                var fileStream = storageService.DownloadFileAsync(s3Request).Result;
                MemoryStream memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);

                backgroundJob.JobStatus = JobStatus.Running;
                backgroundJob = backgroundJob.Save();

                if (backgroundJob.JobType == JobType.BusyBox)
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var secondsToSleep = BusyBox.GetSecondFromStream(memoryStream);
                    BusyBox.Sleep(secondsToSleep);
                    backgroundJob.JobStatus = JobStatus.FinishedSuccess;
                    stopWatch.Stop();
                    backgroundJob.JobOutput =
                        $"The job ran successfully for {secondsToSleep} seconds. stopWatch.Elapsed = {stopWatch.Elapsed}";
                }

                backgroundJob = backgroundJob.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Baseexception Error: {ex.GetBaseException().Message}");
            }
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
