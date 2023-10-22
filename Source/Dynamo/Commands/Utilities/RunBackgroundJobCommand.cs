using System.CommandLine;
using System.Diagnostics;
using Amazon.S3.Model;
using Microsoft.Extensions.DependencyInjection;
using Csla;
using Dynamo.Business.Shared.AdventOfCode.Cloth;
using Dynamo.Business.Shared.AdventOfCode.Warehouse;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Config;
using Dynamo.IO.S3.Models;
using Dynamo.IO.S3.Services;
using Dynamo.Business.Shared.AdventOfCode.Guard;
using Dynamo.Business.Shared.AdventOfCode.Sleigh;
using Dynamo.Business.Shared.AdventOfCode.Fuel;

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
            var backgroundJob = dataPortal?.Fetch(jobId);
            try
            {
                Console.WriteLine($"Running job for file {jobId}");

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
                var fileContents = FileReader.ReadFileContents(memoryStream);
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                switch (backgroundJob.JobType)
                {
                    case JobType.BusyBox:
                        stopWatch.Start();
                        var secondsToSleep = int.Parse(fileContents[0]);
                        BusyBox.Sleep(secondsToSleep);
                        stopWatch.Stop();
                        backgroundJob.JobOutput =
                            $"The job ran successfully for {secondsToSleep} seconds. stopWatch.Elapsed = {stopWatch.Elapsed}";
                        break;
                    case JobType.Day2Step1:
                        var boxScanner = new BoxScanner();
                        var checkSum = boxScanner.GetCheckSum(fileContents);
                        backgroundJob.JobOutput = $"The checksum is {checkSum}.";
                        break;
                    case JobType.Day2Step2:
                        var day2BoxScanner = new BoxScanner();
                        var matching = day2BoxScanner.GetMatchingOffByOneLetter(fileContents);
                        backgroundJob.JobOutput = $"The one that's off by one letter is {matching}";
                        break;
                    case JobType.Day3Step1:
                        var claims = Sheet.ParseClaims(fileContents);
                        var part1Sheet = new Sheet();
                        var overlappingClaims = part1Sheet.FindOverlap(claims);
                        backgroundJob.JobOutput = $"Number of overlapping claims is {overlappingClaims.Count}.";
                        break;
                    case JobType.Day3Step2:
                        var part2Claims = Sheet.ParseClaims(fileContents);
                        var part2Sheet = new Sheet();
                        var claimsWithNoOverlap = part2Sheet.FindClaimsWithNoOverlap(part2Claims);
                        backgroundJob.JobOutput = $"The ID of the claim with no overlap is {claimsWithNoOverlap[0].Id}.";
                        break;
                    case JobType.Day4Step1:
                        var timeline = new GuardEventTimeline(fileContents);
                        var guard = timeline.GetGuardWithMostMinutesAsleepForOneTime();
                        var mostMinutesAsleep = guard.Sleeping[guard.TimeTheySleptTheMost()];
                        var timeSleepingMost = guard.Sleeping.Single(x => x.Value == mostMinutesAsleep);
                        backgroundJob.JobOutput =
                            $"The ID of the guard is {guard.GuardId} and the minute is {timeSleepingMost.Key.Minute}. Puzzle answer = {guard.GuardId * timeSleepingMost.Key.Minute}";
                        break;
                    case JobType.Day4Step2:
                        var timeline2 = new GuardEventTimeline(fileContents);
                        var guard2 = timeline2.GetGuardWithMostMinutesAsleepForOneTime();
                        var mostMinutesAsleep2 = guard2.Sleeping[guard2.TimeTheySleptTheMost()];
                        var timeSleepingMost2 = guard2.Sleeping.Single(x => x.Value == mostMinutesAsleep2);
                        backgroundJob.JobOutput =
                            $"The ID of the guard is {guard2.GuardId} and the minute is {timeSleepingMost2.Key.Minute}. Puzzle answer = {guard2.GuardId * timeSleepingMost2.Key.Minute}";
                        break;
                    case JobType.Day7Step1:
                        var instructions = new JobRunner(fileContents);
                        var stepNames = instructions.GetStepNamesInOrder();
                        backgroundJob.JobOutput = $"The steps in order are {stepNames}.";
                        break;
                    case JobType.Day7Step2:
                        var instructions2 = new JobRunner(fileContents, 60);
                        var secondsTaken = instructions2.GetSecondsTakenToRun(5);
                        backgroundJob.JobOutput = $"The number of seconds taken was {secondsTaken}.";
                        break;
                    case JobType.Day11Step1:
                        int puzzleInput = int.Parse(fileContents[0]);
                        var grid = new FuelCellGrid(300, puzzleInput);
                        var maxPowerIdentifier = grid.GetMaxPowerCoordinates(3);
                        backgroundJob.JobOutput =
                            $"The X,Y coordinates with max power is ({maxPowerIdentifier.Coordinates.X},{maxPowerIdentifier.Coordinates.Y})";
                        break;
                    case JobType.Day11Step2:
                        int puzzleInput2 = int.Parse(fileContents[0]);
                        var grid2 = new FuelCellGrid(300, puzzleInput2);
                        var maxPowerIdentifier2 = grid2.GetMaxPower();
                        backgroundJob.JobOutput =
                            $"(X,Y) = ({maxPowerIdentifier2.Coordinates.X},{maxPowerIdentifier2.Coordinates.Y}), WindowSize = {maxPowerIdentifier2.WindowSize}, Power = {maxPowerIdentifier2.Power}";
                        break;
                    default:
                        throw new Exception($"No processor for JobType {backgroundJob.JobType}.");
                }
                backgroundJob.JobStatus = JobStatus.FinishedSuccess;
                backgroundJob = backgroundJob.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Baseexception Error: {ex.GetBaseException().Message}");
                backgroundJob.JobStatus = JobStatus.FinishedError;
                backgroundJob.JobOutput = $"Error: {ex.GetBaseException().Message}";
                backgroundJob = backgroundJob.Save();
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
