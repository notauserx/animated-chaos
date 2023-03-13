namespace Solution.Domain;

public class ProductName : ValueObject
{
    public string Value { get; private set; }

    public ProductName(string name)
    {
        Value = name;
    }

    public static ProductName FromString(string productName)
    {
        return new ProductName(productName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
