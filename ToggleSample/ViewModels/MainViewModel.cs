using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ToggleSample.ViewModels;

public class MainViewModel : ViewModelBase
{
    public IList<Secret> Secrets { get; } = new List<Secret> { new("Dog", true), new("Cat", false) };
}

public class Secret : ViewModelBase
{
    private bool confirmed;

    public Secret(string text, bool willBeCorrectOnConfirmation)
    {
        Text = text;
        IsCorrect = this.WhenAnyValue(x => x.Confirmed, isConfirmed => isConfirmed && willBeCorrectOnConfirmation);
        IsWrong = this.WhenAnyValue(x => x.Confirmed, isConfirmed => isConfirmed && !willBeCorrectOnConfirmation);
    }

    public IObservable<bool> IsWrong { get; }

    public string Text { get; }

    [Reactive]
    public bool Confirmed { get; set; }

    public IObservable<bool> IsCorrect { get; }
}