﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FluentUIExperiments.Models1;

/// <summary>
/// Lookup table containing the departments within the Adventure Works Cycles company.
/// </summary>
public partial class Department
{
    /// <summary>
    /// Primary key for Department records.
    /// </summary>
    public short DepartmentId { get; set; }

    /// <summary>
    /// Name of the department.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Name of the group to which the department belongs.
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }
}