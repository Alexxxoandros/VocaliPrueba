namespace WorkerService1;

public class Mp3Recover
{
    private readonly IConfiguration _configuration;

    public Mp3Recover()
    {
    }

    public Mp3Recover(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<string> Recover()
    {
        var pathFiles = _configuration.GetValue<string>("PathFiles");
        var exist = Directory.Exists(pathFiles);

        if (!exist) Directory.CreateDirectory(pathFiles);

        var files = Directory.GetFiles(pathFiles);

        files = files.Where(file => file.EndsWith(".mp3")).ToArray();


        return files.ToList();
    }
}