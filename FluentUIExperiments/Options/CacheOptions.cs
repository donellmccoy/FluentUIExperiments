﻿using System;

namespace FluentUIExperiments.Options;

public class CacheOptions
{
    public TimeSpan AbsoluteExpirationRelativeToNow
    {
        get;
        set;
    }
}