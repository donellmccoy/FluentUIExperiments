using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentUIExperiments.Models;
using FluentUIExperiments.Services;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;

public partial class WorkflowViewModel : DcwViewModelBase, INavigationAware
{

    #region Fields

    private bool _isInitialized;

    private readonly IDataService _dataService;

    [ObservableProperty]
    private bool _isEnabled = true;

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

    #endregion

    #region Constructors

    public WorkflowViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    #endregion

    public ObservableGroupedCollection<string, County> Contacts { get; private set; } = new();

    #region Commands

    [RelayCommand(CanExecute = nameof(CanCompletedUserEvents), AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task GetCompletedUserEventsAsync()
    {
        EnableSearchControls(false);
        SendBusyMessage(true);

        await Task.Delay(2000);

        SendBusyMessage(false);
        EnableSearchControls(true);
    }

    private static bool CanCompletedUserEvents()
    {
        return true;
    }

    private void EnableSearchControls(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    #endregion

    #region Methods

    protected async override void OnActivated()
    {
        if (_isInitialized is false)
        {
            try
            {
                EnableSearchControls(false);
                SendBusyMessage(true);

                await InitializeAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                SendBusyMessage(false);
                EnableSearchControls(true);
            }
        }
    }

    private async Task InitializeAsync()
    {
        Counties = await _dataService.GetCounties();
        TypesOfInstruments = await _dataService.GetTypesOfInstruments();
        TypesOfWork = await _dataService.GetTypesOfWork();
        TypesOfCountBy = await _dataService.GetTypesOfCountBy();

        _isInitialized = true;
    }

    protected override void OnDeactivated()
    {

    }

    public void OnNavigatedTo()
    {
        IsActive = true;
    }

    public void OnNavigatedFrom()
    {
        IsActive = false;
    }

    #endregion
}