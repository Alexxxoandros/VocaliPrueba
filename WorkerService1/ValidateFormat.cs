using NAudio.Wave;

namespace WorkerService1;

public class ValidateFormat
{
    private readonly ILogger<ValidateFormat> _logger;

    public ValidateFormat(ILogger<ValidateFormat> logger)
    {
        _logger = logger;
    }

    public bool Validate(string file)
    {
        try
        {
            using (var mp3 = new Mp3FileReader(file))
            {
                return true;
            }
        }
        catch (InvalidDataException)
        {
            _logger.LogInformation("File " + file + " is invalid");
            return false;
        }
    }
}