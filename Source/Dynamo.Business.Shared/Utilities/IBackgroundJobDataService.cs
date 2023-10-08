using System;
using System.Collections.Generic;

namespace Dynamo.Business.Shared.Utilities
{
    public interface IBackgroundJobDataService
    {
        public IEnumerable<BackgroundJobEntity> GetAll();
        public BackgroundJobEntity Get(Guid id);
        public BackgroundJobEntity Insert(BackgroundJobEntity entity);
        public BackgroundJobEntity Update(BackgroundJobEntity entity);
        public bool Delete(Guid id);

    }
}
