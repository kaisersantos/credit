using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Application.UseCases.Parcelas;
using Credit.Core.Application.UseCases.Parcelas.Pagar;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.ParcelaUseCase
{
    public class PagarParcelaUseCaseBusinessTest
    {
        private readonly IPagarParcelaUseCase _pagarParcelaUseCase;
        private readonly Mock<IParcelaRepository> _parcelaRepositoryMock;

        public PagarParcelaUseCaseBusinessTest()
        {
            _parcelaRepositoryMock = new Mock<IParcelaRepository>();

            _pagarParcelaUseCase = new PagarParcelaUseCase(_parcelaRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("String inválido")]
        public async Task ShouldOccurUidInvalidoError(string uid)
        {
            var input = ParcelaMocks.GetPagarParcelaInputMock;

            input.Uid = uid;

            var ex = await Assert.ThrowsAsync<ParcelaCoreApplicationException>(
                () => _pagarParcelaUseCase.Execute(input));

            Assert.Equal(ParcelaError.UidInvalido(uid).Key, ex.Errors.First().Key);

            _parcelaRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _parcelaRepositoryMock.Verify(m => m.Pagar(It.IsAny<Parcela>()), Times.Never);
            _parcelaRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurParcelaNotFoundByUidError()
        {
            var input = ParcelaMocks.GetPagarParcelaInputMock;

            _parcelaRepositoryMock
                .Setup(x => x.FindByUid(Guid.Parse(input.Uid)))
                .ReturnsAsync(null as Parcela);

            var ex = await Assert.ThrowsAsync<ParcelaNotFoundException>(
                () => _pagarParcelaUseCase.Execute(input));

            Assert.Equal(ParcelaError.ParcelaNotFoundByUid(Guid.Parse(input.Uid)).Key, ex.Errors.First().Key);

            _parcelaRepositoryMock.Verify(m => m.FindByUid(Guid.Parse(input.Uid)), Times.Once);
            _parcelaRepositoryMock.Verify(m => m.Pagar(It.IsAny<Parcela>()), Times.Never);
            _parcelaRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditParcelaError()
        {
            var input = ParcelaMocks.GetPagarParcelaInputMock;
            var parcelaMock = ParcelaMocks.GetParcelaMock;

            _parcelaRepositoryMock
                .Setup(x => x.FindByUid(Guid.Parse(input.Uid)))
                .ReturnsAsync(parcelaMock);

            _parcelaRepositoryMock
                .Setup(x => x.Pagar(parcelaMock))
                .ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ParcelaCoreApplicationException>(
                () => _pagarParcelaUseCase.Execute(input));

            Assert.Equal(ParcelaError.UnableToPagarParcela(Guid.Parse(input.Uid)).Key, ex.Errors.First().Key);

            _parcelaRepositoryMock.Verify(m => m.FindByUid(Guid.Parse(input.Uid)), Times.Once);
            _parcelaRepositoryMock.Verify(m => m.Pagar(parcelaMock), Times.Once);
            _parcelaRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditParcela()
        {
            var input = ParcelaMocks.GetPagarParcelaInputMock;
            var parcelaMock = ParcelaMocks.GetParcelaMock;

            _parcelaRepositoryMock
                .Setup(x => x.FindByUid(Guid.Parse(input.Uid)))
                .ReturnsAsync(parcelaMock);

            _parcelaRepositoryMock
                .Setup(x => x.Pagar(parcelaMock))
                .ReturnsAsync(true);

            await _pagarParcelaUseCase.Execute(input);

            _parcelaRepositoryMock.Verify(m => m.FindByUid(Guid.Parse(input.Uid)), Times.Once);
            _parcelaRepositoryMock.Verify(m => m.Pagar(parcelaMock), Times.Once);
            _parcelaRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
