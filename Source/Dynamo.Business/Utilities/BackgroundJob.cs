﻿using Csla;
using System;
using Dynamo.Business.Shared.Utilities;

namespace Dynamo.Business.Utilities
{
    [Serializable]
    public class BackgroundJob : BusinessBase<BackgroundJob>
    {
        public static readonly PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(b => b.Id);
        public Guid Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<JobStatus> JobStatusProperty = RegisterProperty<JobStatus>(nameof(JobStatus));
        public JobStatus JobStatus
        {
            get => GetProperty(JobStatusProperty);

            set => SetProperty(JobStatusProperty, value);
        }

        public static readonly PropertyInfo<JobType> JobTypeProperty = RegisterProperty<JobType>(nameof(JobType));
        public JobType JobType
        {
            get => GetProperty(JobTypeProperty);
            set => SetProperty(JobTypeProperty, value);
        }

        public static readonly PropertyInfo<DateTime> CreatedProperty = RegisterProperty<DateTime>(nameof(Created));
        public DateTime Created
        {
            get => GetProperty(CreatedProperty);
            set => SetProperty(CreatedProperty, value);
        }

        public static readonly PropertyInfo<DateTime> LastUpdatedProperty = RegisterProperty<DateTime>(nameof(LastUpdated));
        public DateTime LastUpdated
        {
            get => GetProperty(LastUpdatedProperty);
            set => SetProperty(LastUpdatedProperty, value);
        }

        public static readonly PropertyInfo<string> JobOutputProperty = RegisterProperty<string>(nameof(JobOutput));
        public string JobOutput
        {
            get => GetProperty(JobOutputProperty);
            set => SetProperty(JobOutputProperty, value);
        }

        public static readonly PropertyInfo<string> FileNameProperty = RegisterProperty<string>(nameof(FileName));
        public string FileName
        {
            get => GetProperty(FileNameProperty);
            set => SetProperty(FileNameProperty, value);
        }

        [Create]
        private void Create()
        {
            Id = Guid.NewGuid();
            JobStatus = JobStatus.Initializing;
            Created = DateTime.Now;
            LastUpdated = DateTime.Now;
            JobOutput = string.Empty;
            FileName = string.Empty;
            base.Child_Create();
        }

        [Fetch]
        private void Fetch(Guid id, [Inject] IBackgroundJobDataService dataService)
        {
            var data = dataService.Get(id);
            using (BypassPropertyChecks)
                Csla.Data.DataMapper.Map(data, this);
            BusinessRules.CheckRules();
        }

        [Fetch]
        private void Fetch(string id, [Inject] IBackgroundJobDataService dataService)
        {
            var data = dataService.Get(Guid.Parse(id));
            using (BypassPropertyChecks)
                Csla.Data.DataMapper.Map(data, this);
            BusinessRules.CheckRules();
        }

        [FetchChild]
        private void Fetch(BackgroundJobEntity data)
        {
            using (BypassPropertyChecks)
                Csla.Data.DataMapper.Map(data, this);
        }

        [Insert]
        // [Test, Ignore("takes too long")]
        private void Insert([Inject] IBackgroundJobDataService dataService)
        {
            using (BypassPropertyChecks)
            {
                var entity = new BackgroundJobEntity
                {
                    Id = this.Id,
                    JobStatus = this.JobStatus,
                    JobType = this.JobType,
                    Created = this.Created,
                    LastUpdated = DateTime.Now,
                    JobOutput = this.JobOutput,
                    FileName = this.FileName
                };
                var result = dataService.Insert(entity);
            }
        }

        [Update]
        private void Update([Inject] IBackgroundJobDataService dataService)
        {
            using (BypassPropertyChecks)
            {
                var entity = new BackgroundJobEntity
                {
                    Id = this.Id,
                    JobStatus = this.JobStatus,
                    JobType = this.JobType,
                    Created = this.Created,
                    LastUpdated = DateTime.Now,
                    JobOutput = this.JobOutput,
                    FileName = this.FileName
                };
                var result = dataService.Update(entity);
            }
        }

        [DeleteSelf]
        private void DeleteSelf([Inject] IBackgroundJobDataService dataService)
        {
            Delete(ReadProperty(IdProperty), dataService);
        }

        [Delete]
        private void Delete(Guid id, [Inject] IBackgroundJobDataService dataService)
        {
            dataService.Delete(id);
        }
    }
}
