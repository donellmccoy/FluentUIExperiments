﻿namespace FluentUIExperiments.Models;

public class County
{
    public int CountyId
    {
        get;
        set;
    }

    public string Name
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