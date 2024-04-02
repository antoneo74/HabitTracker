using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Data;
using Data.Models;
using ReactiveUI;

namespace App.ViewModels
{
    internal class TrackerViewModel:ViewModelBase
    {
        public TrackerViewModel(MainWindowViewModel main)
        {
            m = main;

            using var context = new HabitDbContext();

            var lastHabitId = context.Habits.OrderByDescending(h => h.Id).Select(i => i.Id).First();

            var items = context.HabitChecks.OrderBy(x => x.Date).Where(i => i.HabitId == lastHabitId);

            Click = ReactiveCommand.Create<HabitCheck>(ClickCheckBox);

            ListItems = new ObservableCollection<HabitCheck>(items);

            context.SaveChanges();
        }

        public ObservableCollection<HabitCheck> ListItems { get; }

        public ReactiveCommand<HabitCheck, Unit> Click { get; }

        public MainWindowViewModel m;

        public void ClickCheckBox(HabitCheck item)
        {
            var context = new HabitDbContext();

            context.HabitChecks.First(x => x.Id == item.Id).IsChecked = item.IsChecked;

            context.SaveChanges();

            bool lastHabitCheckIsChecked = context.HabitChecks.OrderByDescending(x => x.Id).First().IsChecked;

            if (lastHabitCheckIsChecked)
            {
                context.Habits.First(x => x.Id == item.HabitId).IsFinished = true;

                context.SaveChanges();

                m.ContentViewModel = new FinishScreenViewModel();
            }
        }
    }
}