using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Habit
{
    [Key]
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Motivation { get; set; }
    public int Count { get; set; }
    public DateTime Date { get; set; }
    public List<HabitCheck> Checks { get; set; } = new List<HabitCheck>();
    public bool IsFinished { get; set; } = false;
}