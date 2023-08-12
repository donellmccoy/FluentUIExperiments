namespace FluentUIExperiments.Options;

public class AppSettings
{
    public AppSettings()
    {
        AllowedRetries = 3;
    }

    public int AllowedRetries
    {
        get; set;
    }
}
