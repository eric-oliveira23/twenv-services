using System.Text.Json.Serialization;

namespace TwenvService.Domain.Entities;

public class Finance(decimal amount, string description, bool isExpense)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; } = amount;
    public string Description { get; set; } = description;
    public bool IsExpense { get; set; } = isExpense; // true = expense, false = income
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}