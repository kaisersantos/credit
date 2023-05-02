using Credit.Core.Domain.Entities;
using Credit.Core.Domain.Exceptions.Credito;

namespace Credit.Core.Domain.UnitTest
{
    public class CreditoTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ab")]
        public void ShouldOccurInvalidValueError(string valor)
        {
            var ex = Assert.Throws<CreditoCoreDomainException>(() =>
            {
                var credito = Credito.GetTipoCredito(valor);
            });


            Assert.Equal(CreditoError.InvalidValue.Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData("d")]
        [InlineData("D")]
        public void ShouldReturnObjectCreditoDiretoString(string valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoDireto>(credito);
        }

        [Theory]
        [InlineData('d')]
        [InlineData('D')]
        public void ShouldReturnObjectCreditoDiretoChar(char valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoDireto>(credito);
        }

        [Theory]
        [InlineData("c")]
        [InlineData("C")]
        public void ShouldReturnObjectCreditoConsignadoString(string valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoConsignado>(credito);
        }

        [Theory]
        [InlineData('c')]
        [InlineData('C')]
        public void ShouldReturnObjectCreditoConsignadoChar(char valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoConsignado>(credito);
        }

        [Theory]
        [InlineData("j")]
        [InlineData("J")]
        public void ShouldReturnObjectCreditoPessoaJuridicaString(string valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoPessoaJuridica>(credito);
        }

        [Theory]
        [InlineData('j')]
        [InlineData('J')]
        public void ShouldReturnObjectCreditoPessoaJuridicaChar(char valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoPessoaJuridica>(credito);
        }

        [Theory]
        [InlineData("f")]
        [InlineData("F")]
        public void ShouldReturnObjectCreditoPessoaFisicaString(string valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoPessoaFisica>(credito);
        }

        [Theory]
        [InlineData('f')]
        [InlineData('F')]
        public void ShouldReturnObjectCreditoPessoaFisicaChar(char valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoPessoaFisica>(credito);
        }

        [Theory]
        [InlineData("i")]
        [InlineData("I")]
        public void ShouldReturnObjectCreditoImobiliarioString(string valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoImobiliario>(credito);
        }

        [Theory]
        [InlineData('i')]
        [InlineData('I')]
        public void ShouldReturnObjectCreditoImobiliarioChar(char valor)
        {
            var credito = Credito.GetTipoCredito(valor);

            Assert.IsType<CreditoImobiliario>(credito);
        }

        [Fact]
        public void ShouldCalculateJurosCreditoDireto()
        {
            var credito = new CreditoDireto();

            Assert.Equal(200, credito.CalcularJuros(1000, 10));
        }

        [Fact]
        public void ShouldCalculateJurosCreditoConsignado()
        {
            var credito = new CreditoConsignado();

            Assert.Equal(100, credito.CalcularJuros(1000, 10));
        }

        [Fact]
        public void ShouldCalculateJurosCreditoPessoaJuridica()
        {
            var credito = new CreditoPessoaJuridica();

            Assert.Equal(500, credito.CalcularJuros(1000, 10));
        }

        [Fact]
        public void ShouldCalculateJurosCreditoPessoaFisica()
        {
            var credito = new CreditoPessoaFisica();

            Assert.Equal(300, credito.CalcularJuros(1000, 10));
        }

        [Fact]
        public void ShouldCalculateJurosCreditoImobiliario()
        {
            var credito = new CreditoImobiliario();

            Assert.Equal(900, credito.CalcularJuros(1000, 10));
        }
    }
}
