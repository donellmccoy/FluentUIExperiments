namespace FluentUIExperiments.Models;

public class FilterData
{
    public int Id
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }

    public string Discriminator
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public bool IsEnabled
    {
        get;
        set;
    }

    public bool IsVisible
    {
        get;
        set;
    }
}