using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Parcelas.Pagar
{
    public class PagarParcelaUseCase : IPagarParcelaUseCase
    {
        private readonly IParcelaRepository _parcelaRepository;

        public PagarParcelaUseCase(IParcelaRepository parcelaRepository)
        {
            _parcelaRepository = parcelaRepository ??
                throw new ArgumentNullException(nameof(parcelaRepository));
        }

        public async Task Execute(PagarParcelaInput input)
        {
            var parcelaDb = await ValidateInput(input);

            parcelaDb.DataPagamento = input.DataPagamento.Date;

            var pago = await _parcelaRepository.Pagar(parcelaDb);

            if (!pago)
                throw new ParcelaCoreApplicationException(ParcelaError.UnableToPagarParcela(parcelaDb.Uid));
        }

        private async Task<Parcela> ValidateInput(PagarParcelaInput input)
        {
            if (!Guid.TryParse(input.Uid, out var parcelaUid))
                throw new ParcelaCoreApplicationException(ParcelaError.UidInvalido(input.Uid));

            var parcela = await _parcelaRepository.FindByUid(parcelaUid) ??
                throw new ParcelaNotFoundException(ParcelaError.ParcelaNotFoundByUid(parcelaUid));

            return parcela;
        }
    }
}
