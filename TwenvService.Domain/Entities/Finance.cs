using System.Text.Json.Serialization;

namespace TwenvService.Domain.Entities;

public class Finance
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsExpense { get; set; } // true = expense, false = income
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Finance(decimal amount, string description, bool isExpense)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Description = description;
        IsExpense = isExpense;
        CreatedAt = DateTime.UtcNow;
    }
}