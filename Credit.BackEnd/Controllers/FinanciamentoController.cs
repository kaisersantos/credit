using Credit.Core.Application.UseCases.Financiamentos.Create;
using Microsoft.AspNetCore.Mvc;

namespace Credit.Presentation.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciamentosController : ControllerBase
    {
        private readonly ICreateFinanciamentoUseCase _createFinanciamentoUseCase;

        public FinanciamentosController(ICreateFinanciamentoUseCase createFinanciamentoUseCase)
        {
            _createFinanciamentoUseCase = createFinanciamentoUseCase ??
                throw new ArgumentNullException(nameof(createFinanciamentoUseCase));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateFinanciamentoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Create(CreateFinanciamentoInput input)
        {
            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            return Ok(createdFinanciamento);
        }
    }
}
