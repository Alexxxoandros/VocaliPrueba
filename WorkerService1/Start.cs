namespace WorkerService1;

internal class Start(
    ILogger<Start> logger,
    Validations validations,
    Mp3Recover mp3Recover,
    SendToInvoxBatch sendToInvox)
{
    private readonly ILogger<Start> _logger = logger;
    private readonly Mp3Recover _mp3Recover = mp3Recover;
    private readonly SendToInvoxBatch _sendToInvox = sendToInvox;
    private readonly Validations _validations = validations;

    public async Task StartMethod()
    {
        _logger.LogInformation("StartMethod called");
        var files = _mp3Recover.Recover();
        var validatedFiles = _validations.ValidateFiles(files);
        await _sendToInvox.ProcessBatch(validatedFiles);       
    }
}