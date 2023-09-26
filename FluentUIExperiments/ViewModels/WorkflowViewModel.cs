using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentUIExperiments.Enumerations;
using FluentUIExperiments.Extensions;
using FluentUIExperiments.Models;
using FluentUIExperiments.Options;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;

public partial class WorkflowViewModel : ViewModelBase, INavigationAware
{
    #region Fields

    private bool _isInitialized;
    private readonly IOptions<AppSettings> _options;
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
    private IEnumerable<DataCenter> _dataCenters = Enumerable.Empty<DataCenter>();

    [ObservableProperty]
    private DataCenter _selectedDataCenter;

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
    private IEnumerable<TypeOfCountBy> _typesOfCountBys = Enumerable.Empty<TypeOfCountBy>();

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
    private bool _saveLastFilterSettings;

    [ObservableProperty]
    private int _receivedValue;

    private TaskNotifier<int> _requestTask;

    #endregion

    #region Constructors

    public WorkflowViewModel(IOptions<AppSettings> options, ICacheService cacheService, ILogger<WorkflowViewModel> logger) : base(logger)
    {
        _options = options;
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
        SetEnabledState(false);
        SetBusyState(BusyStateType.Busy);

        await Task.Delay(2000);

        SetBusyState(BusyStateType.NotBusy);
        SetEnabledState(true);
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
            SetEnabledState(false);
            SetBusyState(BusyStateType.Busy);

            await InitializeViewModelAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
            SetInitializationState(false);
            //show error message
        }
        finally
        {
            SetBusyState(BusyStateType.NotBusy);
            SetEnabledState(true);
        }

        return;

        async Task InitializeViewModelAsync()
        {
            var data = await _cacheService.GetFilterDataAsync();

            Counties = data.GetCounties();
            TypesOfInstruments = data.GetTypesOfInstruments();
            TypesOfWork = data.GetTypesOfWork();
            TypesOfCountBys = data.GetTypesOfCountBy();

            SelectedNumberOfUnits = 50;
            SaveLastFilterSettings = true;
        }
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

    private void SetInitializationState(bool isInitialized)
    {
        _isInitialized = isInitialized;
    }

    private void SetEnabledState(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    private void SetReadOnlyState(bool isReadOnly)
    {
        IsReadOnly = isReadOnly;
    }

    #endregion
}
