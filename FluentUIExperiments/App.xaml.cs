using System.IO;
using System.Windows;
using System.Windows.Threading;
using FluentUIExperiments.Models;
using FluentUIExperiments.Options;
using FluentUIExperiments.Services;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using NavigationService = Wpf.Ui.Mvvm.Services.NavigationService;

namespace FluentUIExperiments;

public partial class App
{
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(builder =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", false, true);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ApplicationHostService>();
            services.AddMemoryCache();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddScoped<INavigationWindow, Views.Windows.MainWindow>();
            services.AddScoped<ViewModels.MainWindowViewModel>();
            services.AddScoped<Views.Pages.WorkflowPage>();
            services.AddScoped<ViewModels.WorkflowViewModel>();
            services.AddScoped<Views.Pages.DataPage>();
            services.AddScoped<ViewModels.DataViewModel>();
            services.AddScoped<Views.Pages.SettingsPage>();
            services.AddScoped<ViewModels.SettingsViewModel>();

            services.Configure<AppSettings>(context.Configuration);

            services.AddDbContextFactory<DataCenterWorkflowContext>(optionsBuilder =>
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DataCenterWorkflow", builder =>
                {
                    builder.EnableRetryOnFailure(3);
                });
            });

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