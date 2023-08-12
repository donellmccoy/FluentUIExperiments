﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentUIExperiments.Enumerations;
using FluentUIExperiments.Models;
using FluentUIExperiments.Services;
using FluentUIExperiments.Services.Interfaces;
using Meziantou.Framework;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;

public partial class WorkflowViewModel : ViewModelBase, INavigationAware
{
    #region Fields

    private bool _isInitialized;

    private readonly ICacheService _cacheService;
    private readonly ILogger<WorkflowViewModel> _logger;

    [ObservableProperty]
    private bool _isEnabled = true;

    [ObservableProperty]
    private bool _isReadOnly;

    [ObservableProperty]
    private bool _inProgress;

    [ObservableProperty]
    private IEnumerable<int> _numberOfUnits = new[] { 1, 10, 50, 100 };

    [ObservableProperty]
    private IEnumerable<County> _counties = Enumerable.Empty<County>();

    [ObservableProperty]
    private County _selectedCounty;

    [ObservableProperty]
    private IEnumerable<TypeOfWork> _typesOfWork = Enumerable.Empty<TypeOfWork>();

    [ObservableProperty]
    private TypeOfWork _selectedTypeOfWork;

    [ObservableProperty]
    private IEnumerable<TypeOfInstrument> _typesOfInstruments = Enumerable.Empty<TypeOfInstrument>();

    [ObservableProperty]
    private TypeOfInstrument _selectedTypeOfInstrument;

    [ObservableProperty]
    private IEnumerable<TypeOfCountBy> _typesOfCountBy = Enumerable.Empty<TypeOfCountBy>();

    [ObservableProperty]
    private TypeOfCountBy _selectedTypeOfCountBy;

    [ObservableProperty]
    private int _selectedNumberOfUnits;

    [ObservableProperty]
    private bool _includeMcnWithCriminalAndSuspect;

    [ObservableProperty]
    private bool _includeAutoCompletedCriminalAndSuspect;

    [ObservableProperty]
    private bool _includeAutoCompletedJunkDocuments;

    [ObservableProperty]
    private bool _includeAvailableWork;

    [ObservableProperty]
    private int _receivedValue;

    private TaskNotifier<int> _requestTask;

    #endregion

    #region Constructors

    public WorkflowViewModel(ICacheService cacheService, ILogger<WorkflowViewModel> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    #endregion

    #region Properties

    public Task<int> RequestTask
    {
        get => _requestTask;
        set => SetPropertyAndNotifyOnCompletion(ref _requestTask, value);
    }

    public ObservableGroupedCollection<string, County> Contacts { get; private set; } = new();

    #endregion

    #region Commands

    [RelayCommand(CanExecute = nameof(CanCompletedUserEvents), AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task GetCompletedUserEventsAsync()
    {
        EnableControls(false);
        SendBusyMessage(BusyType.Busy);

        await Task.Delay(2000);

        SendBusyMessage(BusyType.NoBusy);
        EnableControls(true);
    }

    private static bool CanCompletedUserEvents()
    {
        return true;
    }

    #endregion

    #region Methods

    protected async override void OnActivated()
    {
        if (_isInitialized)
        {
            return;
        }

        try
        {
            EnableControls(false);
            SendBusyMessage(BusyType.Busy);

            await InitializeViewModelAsync();

            _isInitialized = true;
        }
        catch (AggregateException ex)
        {
            _isInitialized = false;
            _logger.LogError("exception: {exception}", ex);
        }
        finally
        {
            SendBusyMessage(BusyType.NoBusy);
            EnableControls(true);
        }
    }

    private async Task InitializeViewModelAsync()
    {
        var (counties, typeOfInstruments, typeOfWorks, typeOfCountBys) = await TaskEx.WhenAll
        (
            _cacheService.GetCounties(),
            _cacheService.GetTypesOfInstruments(),
            _cacheService.GetTypesOfWork(),
            _cacheService.GetTypesOfCountBy()
        );

        Counties = counties;
        TypesOfInstruments = typeOfInstruments;
        TypesOfWork = typeOfWorks;
        TypesOfCountBy = typeOfCountBys;
    }

    private void EnableControls(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    protected override void OnDeactivated()
    {

    }

    public void OnNavigatedTo()
    {
        Activate(true);
    }

    public void OnNavigatedFrom()
    {
        Activate(false);
    }

    #endregion
}