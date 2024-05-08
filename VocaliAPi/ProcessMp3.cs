﻿namespace VocaliAPi;

internal class ProcessMp3 : IProcessMp3
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProcessMp3> _logger;

    public ProcessMp3(ILogger<ProcessMp3> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public Task<Result> Process(IFormFile file)
    {
        var send = false;
        var attempts = 0;

        while (!send && attempts < 3)
            try
            {
                Thread.Sleep(5000);
                var random = new Random();
                var failureProbability = random.Next(1, 101);
                if (failureProbability <= 20) throw new Exception("Error in send process");

                var result = Response();
                send = true;
                return Task.FromResult(result);
            }
            catch (Exception e)
            {
                attempts++;
            }

        throw new Exception("Error in send process");
    }

    private Result Response()
    {
        var pathFiles = _configuration.GetValue<string>("PathFilesTextResult");

        var files = Directory.GetFiles(pathFiles, "*.txt");
        var random = new Random();
        var index = random.Next(files.Length);
        var randomFilePath = files[index];
        var date = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");

        var result = new Result
        {
            Response = File.ReadAllText(randomFilePath),
            FileName = Path.GetFileNameWithoutExtension(randomFilePath) + "_" + date + ".txt"
        };

        return result;
    }
}
