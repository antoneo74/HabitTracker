using System;
using System.Reactive;
using Data.Models;
using ReactiveUI;

namespace App.ViewModels
{
    internal class HabitViewModel : ViewModelBase
    {
        public HabitViewModel()
        {
            var isValidObservable = this.WhenAnyValue(
                x => x.Title,
                x => x.Motivation,
                x => x.Count,
                (title, motivation, count) => !string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(motivation) && count != 0);

            OkCommand = ReactiveCommand.Create(
                () => new Habit { Title = Title, Motivation = Motivation, Date = Date.DateTime, Count = Count }, isValidObservable);
        }

        private string _title = string.Empty;

        private string _motivation = string.Empty;

        private int _count = 0;

        public DateTimeOffset Date { get; set; } = new DateTimeOffset(DateTime.Now);

        public ReactiveCommand<Unit, Habit> OkCommand { get; }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Motivation
        {
            get => _motivation;
            set => this.RaiseAndSetIfChanged(ref _motivation, value);
        }

        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }
    }
}