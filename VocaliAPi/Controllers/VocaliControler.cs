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
    /// <returns>Returns a random file out of 4 possible.</returns>
    [HttpPut]
    public async Task<IActionResult> SendAudio(IFormFile file)
    {
        var data = await _mp3Process?.Process(file)!;
        var res = new Result();
        return Ok(data);
    }
}