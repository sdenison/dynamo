﻿using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
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

        [Create]
        private void Create()
        {
            Id = Guid.NewGuid();
            JobStatus = JobStatus.Initializing;
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

        [Insert]
        private void Insert([Inject] IBackgroundJobDataService dataService)
        {
            using (BypassPropertyChecks)
            {
                var entity = new BackgroundJobEntity
                {
                    Id = this.Id,
                    JobStatus = this.JobStatus,
                    JobType = this.JobType
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
                    JobType = this.JobType
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