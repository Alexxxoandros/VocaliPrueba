namespace WorkerService1;

internal class ValidateSize
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ValidateSize> _logger;

    public ValidateSize(ILogger<ValidateSize> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    internal bool Validate(string file)
    {
        var minSize = int.Parse(_configuration.GetSection("MinSize").Value);
        var maxSize = int.Parse(_configuration.GetSection("MaxSize").Value);

        var info = new FileInfo(file);
        if (info.Length > minSize && info.Length < maxSize) return true;

        _logger.LogInformation("The file {file} does not meet the required size parameters",
            DateTime.UtcNow.ToLongTimeString());
        return false;
    }
}