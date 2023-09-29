using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dynamo.Business.Shared.Utilities
{
    public interface IBackgroundJobDataService
    {
        public BackgroundJobEntity Get(Guid id);
        public BackgroundJobEntity Insert(BackgroundJobEntity entity);
        public BackgroundJobEntity Update(BackgroundJobEntity entity);
        public bool Delete(Guid id);

    }
}
