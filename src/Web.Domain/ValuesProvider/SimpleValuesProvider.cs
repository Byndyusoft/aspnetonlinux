namespace Web.Domain.ValuesProvider
{
    public class SimpleValuesProvider : IValuesProvider
    {
        public string GetValue(ValuesControllerConfiguration source)
        {
            return source.Value;
        }
    }
}