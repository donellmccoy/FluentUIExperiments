using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentUIExperiments.Services;
using FluentUIExperiments.Services.Interfaces;
using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.ViewModels;

public partial class SettingsViewModel : ObservableRecipient, INavigationAware
{
    #region Fields

    private readonly ICacheService _cacheService;
    private bool _isInitialized;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty]
    private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

    #endregion

    #region Constructors

    public SettingsViewModel(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    #endregion

    #region Commands

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":

                if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
                {
                    break;
                }

                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

                break;

            default:

                if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
                {
                    break;
                }

                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

                break;
        }
    }

    #endregion

    #region Methods

    public void OnNavigatedTo()
    {
        if (_isInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
        AppVersion = $"FluentUIExperiments - {GetAssemblyVersion()}";

        _isInitialized = true;
    }

    private static string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
    }

    #endregion
}
