using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FluentUIExperiments.Messages;

public class BusyMessage : ValueChangedMessage<bool>
{
    public BusyMessage(bool isBusy) : base(isBusy)
    {
    }

    public static BusyMessage Create(bool isBusy)
    {
        return new BusyMessage(isBusy);
    }
}