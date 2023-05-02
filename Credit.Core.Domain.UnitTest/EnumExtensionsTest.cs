using Credit.Core.Domain.Exceptions.EnumExtensions;
using Credit.Core.Domain.Extensions;

namespace Credit.Core.Domain.UnitTest
{
    enum TestEnum
    {
        Testb = 'b',
        TestG = 'G'
    }
    enum TestAmbiguousEnum
    {
        Testk = 'k',
        TestK = 'K'
    }

    public class EnumExtensionsTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ab")]
        public void ShouldOccurNullOrEmptyError(string valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnum<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.NullOrEmpty<TestEnum>(valor).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ab")]
        public void ShouldOccurNullOrEmptyErrorIgnoreCase(string valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnumIgnoreCase<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.NullOrEmpty<TestEnum>(valor).Key, ex.Errors.First().Key);
        }

        [Fact]
        public void ShouldOccurEmptyError()
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = '\0'.ToEnum<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.EmptyValue<TestEnum>('\0').Key, ex.Errors.First().Key);
        }

        [Fact]
        public void ShouldOccurEmptyErrorIgnoreCase()
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = '\0'.ToEnumIgnoreCase<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.EmptyValue<TestEnum>('\0').Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("f")]
        [InlineData("z")]
        [InlineData("A")]
        [InlineData("F")]
        [InlineData("Z")]
        public void ShouldOccurInvalidValueErrorString(string valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnum<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.InvalidValue<TestEnum>(valor[0]).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("f")]
        [InlineData("z")]
        [InlineData("A")]
        [InlineData("F")]
        [InlineData("Z")]
        public void ShouldOccurInvalidValueErrorStringIgnoreCase(string valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnumIgnoreCase<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.InvalidValue<TestEnum>(valor[0]).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData('a')]
        [InlineData('f')]
        [InlineData('z')]
        [InlineData('A')]
        [InlineData('F')]
        [InlineData('Z')]
        public void ShouldOccurInvalidValueErrorChar(char valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnum<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.InvalidValue<TestEnum>(valor).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData('a')]
        [InlineData('f')]
        [InlineData('z')]
        [InlineData('A')]
        [InlineData('F')]
        [InlineData('Z')]
        public void ShouldOccurInvalidValueErrorCharIgnoreCase(char valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnumIgnoreCase<TestEnum>();
            });

            Assert.Equal(EnumExtensionsError.InvalidValue<TestEnum>(valor).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData("k")]
        [InlineData("K")]
        public void ShouldOccurAmbiguousValueErrorStringIgnoreCase(string valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnumIgnoreCase<TestAmbiguousEnum>();
            });

            Assert.Equal(EnumExtensionsError.AmbiguousValue<TestAmbiguousEnum>(valor[0]).Key, ex.Errors.First().Key);
        }

        [Theory]
        [InlineData('k')]
        [InlineData('K')]
        public void ShouldOccurAmbiguousValueErrorCharIgnoreCase(char valor)
        {
            var ex = Assert.Throws<EnumExtensionsCoreDomainException>(() =>
            {
                var testEnum = valor.ToEnumIgnoreCase<TestAmbiguousEnum>();
            });

            Assert.Equal(EnumExtensionsError.AmbiguousValue<TestAmbiguousEnum>(valor).Key, ex.Errors.First().Key);
        }


        [Fact]
        public void ShouldReturnTestEnumTestKUpperString()
        {
            var testEnum = "K".ToEnum<TestAmbiguousEnum>();

            Assert.Equal(TestAmbiguousEnum.TestK, testEnum);
        }


        [Fact]
        public void ShouldReturnTestEnumTestKLowerString()
        {
            var testEnum = "k".ToEnum<TestAmbiguousEnum>();

            Assert.Equal(TestAmbiguousEnum.Testk, testEnum);
        }


        [Fact]
        public void ShouldReturnTestEnumTestKUpperChar()
        {
            var testEnum = 'K'.ToEnum<TestAmbiguousEnum>();

            Assert.Equal(TestAmbiguousEnum.TestK, testEnum);
        }


        [Fact]
        public void ShouldReturnTestEnumTestKLowerChar()
        {
            var testEnum = 'k'.ToEnum<TestAmbiguousEnum>();

            Assert.Equal(TestAmbiguousEnum.Testk, testEnum);
        }

        [Theory]
        [InlineData("b")]
        [InlineData("B")]
        public void ShouldReturnTestEnumTestBStringIgnoreCase(string valor)
        {
            var testEnum = valor.ToEnumIgnoreCase<TestEnum>();

            Assert.Equal(TestEnum.Testb, testEnum);
        }

        [Theory]
        [InlineData('b')]
        [InlineData('B')]
        public void ShouldReturnTestEnumTestBCharIgnoreCase(char valor)
        {
            var testEnum = valor.ToEnumIgnoreCase<TestEnum>();

            Assert.Equal(TestEnum.Testb, testEnum);
        }

        [Theory]
        [InlineData("g")]
        [InlineData("G")]
        public void ShouldReturnTestEnumTestGStringIgnoreCase(string valor)
        {
            var testEnum = valor.ToEnumIgnoreCase<TestEnum>();

            Assert.Equal(TestEnum.TestG, testEnum);
        }

        [Theory]
        [InlineData('g')]
        [InlineData('G')]
        public void ShouldReturnTestEnumTestGCharIgnoreCase(char valor)
        {
            var testEnum = valor.ToEnumIgnoreCase<TestEnum>();

            Assert.Equal(TestEnum.TestG, testEnum);
        }
    }
}
