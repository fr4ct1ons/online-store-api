using OnlineStoreAPI.Storage;

namespace OnlineStoreAPI.Services;

public class StorageSaver : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Saving... Current Time - {DateTime.Now}");
            try
            {
                ApplicationStorage.SaveToJsonFile();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}