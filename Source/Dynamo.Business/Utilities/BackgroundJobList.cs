using System;
using System.Linq;
using Csla;
using Dynamo.Business.Shared.Utilities;

namespace Dynamo.Business.Utilities
{
    [Serializable]
    public class BackgroundJobList : ReadOnlyListBase<BackgroundJobList, BackgroundJob>
    {
        [Fetch]
        private void Fetch([Inject] IBackgroundJobDataService dataService, [Inject] IChildDataPortal<BackgroundJob> backgroundJobPortal)
        {
            using (LoadListMode)
            {
                var data = dataService.GetAll().Select(d => backgroundJobPortal.FetchChild(d));
                AddRange(data);
            }
        }
    }
}
