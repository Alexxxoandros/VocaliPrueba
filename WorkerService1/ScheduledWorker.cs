namespace WorkerService1;

internal class ScheduledWorker : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ScheduledWorker> _logger;
    private readonly Start _start;
    private Timer? _timer;

    public ScheduledWorker(ILogger<ScheduledWorker> logger, Start start, IConfiguration configuration)
    {
        _logger = logger;
        _start = start;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timeEx = _configuration.GetSection("TimeEx").Get<TimeEx>();
        var now = DateTime.Now;
        var target =
            new DateTime(now.Year, now.Month, now.Day, timeEx.Hora, timeEx.Minuto,
                timeEx.Segundo); 
        if (now > target)
        {            
            target = target.AddDays(1);
        }

        var delay = target - now;
        _timer = new Timer(DoWork, null, delay, TimeSpan.FromDays(1));
        await Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Executing scheduled work.");
        _start.StartMethod();
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stopping scheduled work.");
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}
