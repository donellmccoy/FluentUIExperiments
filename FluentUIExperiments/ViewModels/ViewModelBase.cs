using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentUIExperiments.Enumerations;
using FluentUIExperiments.Messages;

namespace FluentUIExperiments.ViewModels;

public abstract class ViewModelBase : ObservableRecipient
{
    protected virtual void SendBusyMessage(BusyType busyType)
    {
        SendBusyMessage(busyType == BusyType.Busy);
    }

    protected virtual void SendBusyMessage(bool isBusy)
    {
        Messenger.Send(BusyMessage.Create(isBusy));
    }

    protected void Activate(bool isActive)
    {
        IsActive = isActive;
    }
}