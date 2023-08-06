using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using FluentUIExperiments.Models;
using FluentUIExperiments.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FluentUIExperiments;

public partial class App
{
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(builder =>
        {
            builder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ApplicationHostService>();

            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();

            services.AddScoped<INavigationWindow, Views.Windows.MainWindow>();
            services.AddScoped<ViewModels.MainWindowViewModel>();
            services.AddScoped<Views.Pages.WorkflowPage>();
            services.AddScoped<ViewModels.WorkflowViewModel>();
            services.AddScoped<Views.Pages.DataPage>();
            services.AddScoped<ViewModels.DataViewModel>();
            services.AddScoped<Views.Pages.SettingsPage>();
            services.AddScoped<ViewModels.SettingsViewModel>();

            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));

        }).Build();

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await Host.StartAsync();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await Host.StopAsync();

        Host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}