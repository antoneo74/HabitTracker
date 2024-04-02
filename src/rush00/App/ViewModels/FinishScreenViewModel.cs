using Data;
using Data.Models;
using System.Linq;

namespace App.ViewModels
{
    internal class FinishScreenViewModel : ViewModelBase
    {
        private readonly Habit _habit;

        private readonly int _countCheckedDays;

        public FinishScreenViewModel()
        {
            _habit = new HabitDbContext().Habits.ToList().Last();

            _countCheckedDays = new HabitDbContext().HabitChecks.Count(x => x.IsChecked == true && x.HabitId == _habit.Id);
        }
        public string Statistic => $"{_countCheckedDays}/{_habit.Count} days checked";

        public string Motivation => $"Finally: {_habit.Motivation}";
    }
}
