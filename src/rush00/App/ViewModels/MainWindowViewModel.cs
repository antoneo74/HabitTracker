using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Linq;
using Data;
using Data.Models;

namespace App.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase? _contentViewModel;

    public MainWindowViewModel()
    {
        CreateContent();
    }

    public void CreateContent()
    {
        if (new HabitDbContext().Habits.Any() && !new HabitDbContext().Habits.ToList().Last().IsFinished)
        {
            ContentViewModel = new TrackerViewModel(this);
        }
        else
        {
            var startView = new HabitViewModel();

            ContentViewModel = startView;

            Observable.Merge(startView.OkCommand).Take(1).Subscribe(FillNewHabit);
        }
    }

    public ViewModelBase? ContentViewModel
    {
        get => _contentViewModel;
        set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void FillNewHabit(Habit x)
    {
        using var context = new HabitDbContext();

        DateTime date = x.Date;

        for (var day = 0; day < x.Count; ++day)
        {
            var habitCheck = new HabitCheck() { Date = date.AddDays(day), IsChecked = false };

            x.Checks.Add(habitCheck);

            context.HabitChecks.Add(habitCheck);
        }
        context.Habits.Add(x);

        context.SaveChanges();

        ContentViewModel = new TrackerViewModel(this);
    }
}
