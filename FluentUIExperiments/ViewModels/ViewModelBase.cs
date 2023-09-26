using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentUIExperiments.Enumerations;
using FluentUIExperiments.Messages;
using Microsoft.Extensions.Logging;

namespace FluentUIExperiments.ViewModels;

public abstract class ViewModelBase : ObservableRecipient
{
    private readonly ILogger<WorkflowViewModel> _logger;

    protected ViewModelBase()
    {
        
    }

    protected ViewModelBase(ILogger<WorkflowViewModel> logger)
    {
        _logger = logger;
    }

    protected virtual void SetBusyState(BusyStateType busyStateType)
    {
        SetBusyState(busyStateType == BusyStateType.Busy);
    }

    protected virtual void SetBusyState(bool isBusy)
    {
        Messenger.Send(BusyMessage.Create(isBusy));
    }

    protected void Activate(bool isActive)
    {
        IsActive = isActive;
    }

    protected virtual void HandleException(Exception ex)
    {
        _logger.LogError("exception: {exception}", ex);
    }
}