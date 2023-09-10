namespace FluentUIExperiments.Models;

public class FilterSettings
{
    public int FilterSettingsId
    {
        get;
        set;
    }

    public int CountyId
    {
        get;
        set;
    }

    public string TypeOfWorkId
    {
        get;
        set;
    }

    public int TypeOfInstrumentId
    {
        get;
        set;
    }


    public string TypeOfCountById
    {
        get;
        set;
    }

    public bool IncludeMcnWithCriminalAndSuspect
    {
        get;
        set;
    }

    public bool IncludeAutoCompletedCriminalAndSuspect
    {
        get;
        set;
    }

    public bool  IncludeAutoCompletedJunkDocuments
    {
        get;
        set;
    }

    public bool IncludeAvailableWork
    {
        get;
        set;
    }

    public string UserName
    {
        get;
        set;
    }

}