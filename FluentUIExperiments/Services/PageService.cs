using System;
using System.Windows;
using Wpf.Ui.Mvvm.Contracts;

namespace FluentUIExperiments.Services;

public class PageService : IPageService
{
    #region Fields

    /// <summary>
    /// Service which provides the instances of pages.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new instance and attaches the <see cref="IServiceProvider"/>.
    /// </summary>
    public PageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public T GetPage<T>() where T : class
    {
        if (typeof(FrameworkElement).IsAssignableFrom(typeof(T)) is false)
        {
            throw new InvalidOperationException("The page should be a WPF control.");
        }

        return (T)_serviceProvider.GetService(typeof(T));
    }

    /// <inheritdoc />
    public FrameworkElement GetPage(Type pageType)
    {
        if (typeof(FrameworkElement).IsAssignableFrom(pageType) is false)
        {
            throw new InvalidOperationException("The page should be a WPF control.");
        }

        return _serviceProvider.GetService(pageType) as FrameworkElement;
    }

    #endregion
}
