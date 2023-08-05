using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentUIExperiments.Messages;

namespace FluentUIExperiments.ViewModels;

public abstract class DcwViewModelBase : ObservableRecipient
{
    protected virtual void SendBusyMessage(bool isBusy)
    {
        Messenger.Send(BusyMessage.Create(isBusy));
    }
}