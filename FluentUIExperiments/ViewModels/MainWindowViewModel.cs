using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace FluentUIExperiments.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationItems = new();

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationFooter = new();

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new();

    [ObservableProperty]
    private ObservableCollection<County> _counties = new();

    [ObservableProperty]
    private County _selectedCounty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _inProgress;


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


    public MainWindowViewModel()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        Counties = new ObservableCollection<County>
        {
            new() { CountyId = 1, Name = "Miami/Dade" },
            new() { CountyId = 2, Name = "Orange" },
            new() { CountyId = 3, Name = "Brevard" },
        };

        ApplicationTitle = "Data Center Workflow";

        NavigationItems = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Workflow",
                PageTag = "dashboard",
                Icon = SymbolRegular.DesktopFlow20,
                PageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationItem
            {
                Content = "Administration",
                PageTag = "data",
                Icon = SymbolRegular.ApprovalsApp16,
                PageType = typeof(Views.Pages.DataPage)
            }
        };

        NavigationFooter = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Settings",
                PageTag = "settings",
                Icon = SymbolRegular.Settings24,
                PageType = typeof(Views.Pages.SettingsPage)
            }
        };

        TrayMenuItems = new ObservableCollection<MenuItem>
        {
            new()
            {
                Header = "Home",
                Tag = "tray_home"
            }
        };

        _isInitialized = true;
    }
}

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
}
