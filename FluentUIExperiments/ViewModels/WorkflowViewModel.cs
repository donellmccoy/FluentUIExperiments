using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentUIExperiments.Models;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;

public partial class WorkflowViewModel : DcwViewModelBase, INavigationAware
{
    #region Fields

    [ObservableProperty]
    private bool _isEnabled = true;

    [ObservableProperty]
    private bool _inProgress;

    [ObservableProperty]
    private ObservableCollection<County> _counties = new();

    [ObservableProperty]
    private County _selectedCounty;

    [ObservableProperty]
    private ObservableCollection<TypeOfWork> _typesOfWork = new();

    [ObservableProperty]
    private TypeOfWork _selectedTypeOfWork;

    [ObservableProperty]
    private ObservableCollection<TypeOfInstrument> _typesOfInstruments = new();

    [ObservableProperty]
    private ObservableCollection<int> _numberOfUnits = new(){ 1, 10, 50, 100};

    [ObservableProperty]
    private TypeOfInstrument _selectedTypeOfInstrument;

    [ObservableProperty]
    private ObservableCollection<TypeOfCountBy> _typesOfCountBy = new();

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
    private bool _displayAvailableWork;

    [ObservableProperty]
    private int _receivedValue;

    #endregion

    #region Constructors

    public WorkflowViewModel()
    {
        Counties = new ObservableCollection<County>
        {
            new() { CountyId = 1, Name = "Miami/Dade" },
            new() { CountyId = 2, Name = "Orange" },
            new() { CountyId = 3, Name = "Brevard" },
        };

        TypesOfInstruments = new ObservableCollection<TypeOfInstrument>
        {
            new() { TypeOfInstrumentId = 1, Name = "TypeOfInstrument 1" },
            new() { TypeOfInstrumentId = 2, Name = "TypeOfInstrument 2" },
            new() { TypeOfInstrumentId = 3, Name = "TypeOfInstrument 3" },
        };
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

    protected override void OnActivated()
    {
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