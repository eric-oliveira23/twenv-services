using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwenvService.Application.Usecases.Finance;
using TwenvService.Domain.Entities;

namespace TwenvService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceController(
        ListFinancesUseCase listFinancesUseCase,
        GetFinanceByIdUseCase getFinanceByIdUseCase,
        CreateFinanceUseCase createFinanceUseCase,
        UpdateFinanceUseCase updateFinanceUseCase,
        DeleteFinanceUseCase deleteFinanceUseCase)
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateFinance([FromBody] Finance finance)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            finance.UserId = userId;
            await createFinanceUseCase.ExecuteAsync(finance);
            return CreatedAtAction(nameof(GetFinanceById), new { id = finance.Id }, finance);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Finance>>> GetAllFinances([FromQuery] string? type = null)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var finances = await listFinancesUseCase.ExecuteAsync(userId, type);
            return Ok(finances);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Finance>> GetFinanceById(Guid id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var finance = await getFinanceByIdUseCase.ExecuteAsync(id, userId);
            if (finance == null) return NotFound("Finance not found.");
            return Ok(finance);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateFinance(Guid id, [FromBody] Finance finance)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            finance.UserId = userId;
            finance.Id = id;
            await updateFinanceUseCase.ExecuteAsync(finance);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFinance(Guid id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            await deleteFinanceUseCase.ExecuteAsync(id, userId);
            return Ok();
        }
    }
}