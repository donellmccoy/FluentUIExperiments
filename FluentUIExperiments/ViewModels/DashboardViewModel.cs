using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;
public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private ObservableCollection<County> _counties = new();

    [ObservableProperty]
    private County _selectedCounty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _inProgress;

    public DashboardViewModel()
    {
        Counties = new ObservableCollection<County>
        {
            new() { CountyId = 1, Name = "Miami/Dade" },
            new() { CountyId = 2, Name = "Orange" },
            new() { CountyId = 3, Name = "Brevard" },
        };
    }

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

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
    }
}
