namespace WorkerService1;

public class Validations(ValidateSize validateSize, ValidateFormat validateFormat)
{
    public List<string> ValidateFiles(List<string> filesList)
    {
        return filesList.Where(file => validateSize.Validate(file) && validateFormat.Validate(file)).ToList();
    }
} 