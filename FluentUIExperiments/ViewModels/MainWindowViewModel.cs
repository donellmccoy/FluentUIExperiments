using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentUIExperiments.Enumerations;
using FluentUIExperiments.Messages;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace FluentUIExperiments.ViewModels;
public partial class MainWindowViewModel : ViewModelBase, INavigationAware
{
    #region Fields

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
    private bool _isBusy;

    [ObservableProperty]
    private bool _inProgress;

    #endregion

    #region Constructors

    public MainWindowViewModel()
    {
        if (_isInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    #endregion

    #region Methods

    public void OnNavigatedTo()
    {
        Activate(true);
    }

    public void OnNavigatedFrom()
    {
        Activate(false);
    }

    protected override void OnActivated()
    {
    }

    protected override void OnDeactivated()
    {
    }

    private void InitializeViewModel()
    {
        Messenger.Register<MainWindowViewModel, BusyMessage>(this, OnSetBusyMessage);

        SetBusyState(BusyStateType.NotBusy);

        ApplicationTitle = "Data Center Workflow";

        NavigationItems = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Workflow",
                PageTag = "dashboard",
                Icon = SymbolRegular.DesktopFlow20,
                PageType = typeof(Views.Pages.WorkflowPage)
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

        SetInitializationState(true);
    }

    private static void OnSetBusyMessage(MainWindowViewModel recipient, BusyMessage message)
    {
        recipient.IsBusy = message.Value;
    }

    private void SetInitializationState(bool isInitialized)
    {
        _isInitialized = isInitialized;
    }

    #endregion
}