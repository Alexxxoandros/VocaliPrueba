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
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        await Task.CompletedTask;
    }


    private void DoWork(object? state)
    {
        var timeEx = _configuration.GetSection("TimeEx").Get<TimeEx>();
        var now = DateTime.Now;
        var target =
            new DateTime(now.Year, now.Month, now.Day, timeEx.Hora, timeEx.Minuto,
                timeEx.Segundo); // Set to 23:59:59 today
        if (now < target)
        {
            _logger.LogInformation("Executing scheduled work.");
            _start.StartMethod();
        }
        else
        {
            // Calculate the next run time
            var nextRun = target.AddDays(1);
            var waitTime = nextRun - now;
            // Reschedule the timer
            _timer?.Change(waitTime, TimeSpan.FromDays(1));
        }
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