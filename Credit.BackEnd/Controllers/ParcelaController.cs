using Credit.Core.Application.UseCases.Parcelas.Pagar;
using Microsoft.AspNetCore.Mvc;

namespace Credit.Presentation.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelasController : ControllerBase
    {
        private readonly IPagarParcelaUseCase _pagarParcelaUseCase;

        public ParcelasController(IPagarParcelaUseCase pagarParcelaUseCase)
        {
            _pagarParcelaUseCase = pagarParcelaUseCase ??
                throw new ArgumentNullException(nameof(pagarParcelaUseCase));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Pagar(PagarParcelaInput input)
        {
            await _pagarParcelaUseCase.Execute(input);

            return NoContent();
        }
    }
}
