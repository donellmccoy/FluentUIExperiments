using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Stateless;

namespace FluentUIExperiments.Extensions;

public static class StateMachineExtensions
{
    public static ICommand CreateCommand<TState, TTrigger>(this StateMachine<TState, TTrigger> stateMachine, TTrigger trigger)
    {
        return new RelayCommand
        (
            () => stateMachine.Fire(trigger),
            () => stateMachine.CanFire(trigger)
        );
    }
}