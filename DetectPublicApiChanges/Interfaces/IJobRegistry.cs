using System.Collections.Generic;

namespace DetectPublicApiChanges.Interfaces
{
    public interface IJobRegistry
    {
        IJob GetJob(string name);
        IEnumerable<IJob> GetJobs();
    }
}
