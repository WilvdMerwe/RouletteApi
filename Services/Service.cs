namespace RouletteApi.Services;

public class Service
{
    protected readonly ILogger Logger;

    public Service(ILogger logger)
    {
        Logger = logger;
    }

    public void LogException(string message, Exception ex)
    {
        Logger.LogError("{Message} - {Exception} - {StackTrace}", message, ex.Message, ex.StackTrace);
    }
}
