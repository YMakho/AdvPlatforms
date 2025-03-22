using System.Collections.Concurrent;

namespace AdvPlaces.Api.Services
{
    public interface IAdvPlatformService
    {
        Task Upload(string path);
        ConcurrentBag<string> FindPlatformsByLocation(string location);
    }
}
