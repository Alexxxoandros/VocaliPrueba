namespace WorkerService1;

internal class Validations
{
    private readonly ValidateFormat _validateFormat;
    private readonly ValidateSize _validateSize;

    public Validations(ValidateSize validateSize, ValidateFormat validateFormat)
    {
        _validateSize = validateSize;
        _validateFormat = validateFormat;
    }

    internal List<string> ValidateFiles(List<string> filesList)
    {
        var validatedFiles = new List<string>();
        foreach (var file in filesList)
            if (_validateSize.Validate(file) && _validateFormat.Validate(file))
                validatedFiles.Add(file);
        return validatedFiles;
    }
}