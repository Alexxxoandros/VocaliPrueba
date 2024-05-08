namespace VocaliAPi;

public interface IProcessMp3
{
    Task<Result> Process(IFormFile file);
}