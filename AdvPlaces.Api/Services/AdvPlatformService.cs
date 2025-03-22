using AdvPlaces.Api.Model;
using System.Collections.Concurrent;

namespace AdvPlaces.Api.Services
{
    public sealed class AdvPlatformService : IAdvPlatformService
    {
        public ConcurrentDictionary<string, ConcurrentBag<string>> PlatformsByLocation = new ConcurrentDictionary<string, ConcurrentBag<string>>();
        private readonly ILogger<AdvPlatformService> _logger;

        public AdvPlatformService(ILogger<AdvPlatformService> logger)
        {
            _logger = logger;
        }

        private async Task<string[]> LoadFile(string path)
        {
            try
            {
                return await File.ReadAllLinesAsync(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ApplicationException(ex.Message);
            }

        }
        public ConcurrentBag<string> FindPlatformsByLocation(string location)
        {
            if (!PlatformsByLocation.ContainsKey(location))
            {
                _logger.LogError($"Platform(s) with location: {location} not found");
                throw new ApplicationException($"Platform with location: {location} not found");
            }               
            return PlatformsByLocation[location];
        }
        public async Task Upload(string path)
        {
            PlatformsByLocation.Clear();
            var array = await LoadFile(path);
            var tempDictionary = new ConcurrentDictionary<string, ConcurrentBag<string>>();

            Parallel.ForEach(array, line =>
            {
                var parts = line.Split([':', ','], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                {
                    _logger.LogError($"Invalid line format: {line}");
                    return;
                }
                for (int i = 1; i < parts.Length; i++)
                {
                    try
                    {
                        var newPlatform = AdvPlatform.Initialize(parts[0].Trim(), parts[i].Trim());
                        tempDictionary.AddOrUpdate(
                                newPlatform.PlatformLocation,
                                newBag => new ConcurrentBag<string> { newPlatform.PlatformName },
                                (newBag, existingBag) =>
                                {
                                    existingBag.Add(newPlatform.PlatformName);
                                    return existingBag;
                                });
                    }
                    catch (DomainException ex)
                    {
                        // Логируем ошибку валидации
                        _logger.LogError(ex, $"Validation error in line: {line}. Part: {parts[i].Trim()}");
                    }
                    catch (Exception ex)
                    {
                        // Логируем другие ошибки
                        _logger.LogDebug(ex, $"Unexpected error in line: {line}. Part: {parts[i].Trim()}");
                    }
                }
            });
            PlatformsByLocation = tempDictionary;
        }
    }
}
