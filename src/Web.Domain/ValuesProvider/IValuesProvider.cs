namespace Web.Domain.ValuesProvider
{
    public interface IValuesProvider
    {
        string GetValue(ValuesControllerConfiguration source);
    }
}