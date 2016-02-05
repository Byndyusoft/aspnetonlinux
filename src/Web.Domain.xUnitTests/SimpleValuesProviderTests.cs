namespace Web.Domain.xUnitTests
{
    using ValuesProvider;
    using Xunit;

    public class SimpleValuesProviderTests
    {
        private readonly SimpleValuesProvider _valuesProvider;

        public SimpleValuesProviderTests()
        {
            _valuesProvider = new SimpleValuesProvider();
        }

        [Fact]
        public void ShouldReturnValueFromConfiguration1()
        {
            // Given
            var configiration = new ValuesControllerConfiguration { Value = "aaa", Value1 = "bbb", Value2 = "ccc" };

            // When
            var v = _valuesProvider.GetValue(configiration);

            // Then
            Assert.Equal(configiration.Value, v);
        }

        [Fact]
        public void ShouldReturnValueFromConfiguration2()
        {
            // Given
            var configiration = new ValuesControllerConfiguration { Value = "aaa", Value1 = "bbb", Value2 = "ccc" };

            // When
            var v = _valuesProvider.GetValue(configiration);

            // Then
            Assert.Equal(configiration.Value, v);
        }
    }
}