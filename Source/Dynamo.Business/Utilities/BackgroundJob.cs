using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
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

    }
}
