namespace WorkerService1;

internal class SendToInvox
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SendToInvox> _logger;

    public SendToInvox(ILogger<SendToInvox> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void ProcessInBatches(List<string> filesToProcess)
    {
        var batch = new List<string>();
        foreach (var file in filesToProcess)
        {
            batch.Add(file);
            if (batch.Count == 3)
            {
                SendBatch(batch);

                batch.Clear();
            }
        }

        if (batch.Count > 0) SendBatch(batch);
    }

    private async Task SendBatch(List<string> batch)
    {
        var pathFiles = _configuration.GetValue<string>("PathFilesResult");


        foreach (var file in batch)
            using (var client = new HttpClient())
            {
                //llamar al metodo put senAudio de la api vocaliApi
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(file)), "file", file);
                var response = await client.PutAsync("https://localhost:7146/Vocali", content);

                //esperar a que response devuelva 
                Console.WriteLine(file + "Procesada");

                var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                var newFile = Path.GetFileNameWithoutExtension(file) + date + ".txt";


                var textContent = await response.Content.ReadAsStringAsync();
                var a = File.ReadAllBytes(textContent);

                File.WriteAllBytes(pathFiles, a);
            }
    }
}