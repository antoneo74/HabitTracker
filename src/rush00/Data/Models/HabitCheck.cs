using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class HabitCheck
{
    [Key]
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsChecked { get; set; }

    [ForeignKey(nameof(Habit.Id))]
    public long HabitId { get; set; }
}