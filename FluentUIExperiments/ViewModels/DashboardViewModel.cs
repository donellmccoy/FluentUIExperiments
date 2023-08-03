using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;

namespace FluentUIExperiments.ViewModels;
public partial class DashboardViewModel : ObservableRecipient, INavigationAware
{
    #region Fields

    [ObservableProperty]
    private ObservableCollection<County> _counties = new();

    [ObservableProperty]
    private County _selectedCounty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _inProgress;

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

    public DashboardViewModel()
    {
        Counties = new ObservableCollection<County>
        {
            new() { CountyId = 1, Name = "Miami/Dade" },
            new() { CountyId = 2, Name = "Orange" },
            new() { CountyId = 3, Name = "Brevard" },
        };
    }

    #endregion

    #region Commands

    [RelayCommand(CanExecute = nameof(CanCompletedUserEvents), AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task GetCompletedUserEventsAsync()
    {
        IsBusy = true;

        await Task.Delay(5000);

        IsBusy = false;
    }

    private static bool CanCompletedUserEvents()
    {
        return true;
    }

    #endregion

    #region Methods

    protected override void OnActivated()
    {
        Messenger.Register<DashboardViewModel, MyMessage>(this, MyMessageHandler);
    }

    private static void MyMessageHandler(DashboardViewModel viewModel, MyMessage message)
    {
        viewModel.ReceivedValue = message.Value;
    }

    protected override void OnDeactivated()
    {
        Messenger.Unregister<MyMessage>(this);
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