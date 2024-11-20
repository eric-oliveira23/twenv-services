using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwenvService.Data;
using TwenvService.Domain.Entities;
using System.Security.Claims;

namespace TwenvService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceController(FinancesDbContext context) : ControllerBase
    {
        // POST: api/finance
        [HttpPost]
        public async Task<IActionResult> CreateFinance([FromBody] Finance? finance)
        {
            if (finance == null)
            {
                return BadRequest("Invalid finance data.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            finance.UserId = int.Parse(userId);

            context.Finances.Add(finance);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFinance), new { id = finance.Id }, finance);
        }

        // GET: api/finance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Finance>>> GetFinances([FromQuery] string? type = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var finances = context.Finances
                .Where(f => f.UserId == int.Parse(userId));

            if (!string.IsNullOrEmpty(type))
            {
                type = type.ToLower();
                finances = type switch
                {
                    "income" => finances.Where(f => !f.IsExpense),
                    "expense" => finances.Where(f => f.IsExpense),
                    _ => finances
                };
            }

            var result = await finances.ToListAsync();
            return Ok(result);
        }

        // GET: api/finance/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Finance>> GetFinance(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var finance = await context.Finances
                .Where(f => f.Id == id && f.UserId == int.Parse(userId))
                .FirstOrDefaultAsync();

            if (finance == null)
            {
                return NotFound("Finance not found or access denied.");
            }

            return Ok(finance);
        }
    }
}
