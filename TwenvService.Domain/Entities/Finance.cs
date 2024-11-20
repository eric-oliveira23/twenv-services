using System.Text.Json.Serialization;

namespace TwenvService.Domain.Entities;

public class Finance(decimal amount, string description, bool isExpense)
{
  
    public int Id { get; set; }
    public decimal Amount { get; init; } = amount;
    public string Description { get; init; } = description;
    public bool IsExpense { get; init; } = isExpense; // true = expense, false = income
    public int UserId { get; set; }
}