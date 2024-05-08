using Newtonsoft.Json;

namespace WorkerService1;

internal class SendToInvoxBatch
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SendToInvoxBatch> _logger;

    public SendToInvoxBatch(IConfiguration configuration, ILogger<SendToInvoxBatch> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    internal async Task<List<Result>> ProcessBatch(List<string> files)
    {
        var responses = new List<Result>();

        for (var i = 0; i < files.Count; i += 3)
        {
            var batch = files.GetRange(i, Math.Min(3, files.Count - i));
            _logger.LogInformation("Call batchResponse " + i);

            var batchResponses = await SendBatch(batch);
            responses.AddRange(batchResponses);
        }

        SaveResult(responses);
        return responses;
    }

    private async Task<List<Result>> SendBatch(List<string> batch)
    {
        var responses = new List<Result>();
        _logger.LogInformation("Init foreach");

        foreach (var file in batch)
        {
            using (var client = new HttpClient())
            {
                var res = new Result
                {
                    Response = null,
                    FileName = null
                };

                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(file)), "file", file);
                _logger.LogInformation("Calling response");

                var response = await client.PutAsync("https://localhost:7146/Vocali", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var rest = JsonConvert.DeserializeObject<Result>(responseContent);
                    res.Response = rest.Response;
                    res.FileName = rest.FileName;
                }

                responses.Add(res);
            }
        }

        return responses;
    }

    private void SaveResult(List<Result> responses)
    {
        var pathFiles = _configuration.GetValue<string>("PathFilesResult");

        foreach (var res in responses.Where(res => true))
        {
            if (pathFiles != null) File.WriteAllText(Path.Combine(pathFiles, res.FileName), res.Response);
        }
    }
}
