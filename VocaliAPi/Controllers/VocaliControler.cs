using Microsoft.AspNetCore.Mvc;

namespace VocaliAPi.Controllers;

[ApiController]
[Route("Vocali")]

public class VocaliControler : ControllerBase
{
    private readonly IProcessMp3? _mp3Process;
    
    public VocaliControler(IProcessMp3 mp3Process)
    {
        _mp3Process = mp3Process;
    }

    /// <summary>
    /// This method is used to send audio data.
    /// </summary>
    /// <param name="file">The audio file to be processed.</param>
    /// <returns>The result of the audio processing.</returns>
    [HttpPut]
    public async Task<IActionResult> SendAudio(IFormFile file)
    {
        var data = await _mp3Process?.Process(file)!;
        var res = new Result();
        res.FileName = data.FileName;
        res.Response = data.Response;
        return Ok(data);
    }
}