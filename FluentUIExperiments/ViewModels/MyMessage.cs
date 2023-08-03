using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FluentUIExperiments.ViewModels;

public class MyMessage : ValueChangedMessage<int>
{
    public MyMessage(int user) : base(user)
    {
    }
}