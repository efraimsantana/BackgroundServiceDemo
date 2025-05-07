using Medallion.Threading;

namespace BackgroundServiceDemo;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDistributedLockProvider _lockProvider;

    public Worker(ILogger<Worker> logger, IDistributedLockProvider lockProvider)
    {
        _logger = logger;
        _lockProvider = lockProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var @lock = _lockProvider.CreateLock("ScheduledBackgroundJob");
        
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));
        
        do
        {
            try
            {
                await using var handle = await @lock.TryAcquireAsync(TimeSpan.FromSeconds(30), stoppingToken);
                if (handle is not null)
                {
                    await DoWorkAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                // ✅ Log the error and keep going
                _logger.LogError(ex, "Error occurred while executing background job.");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken)
               && !stoppingToken.IsCancellationRequested);
   
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5_000, stoppingToken);
        
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    }
}