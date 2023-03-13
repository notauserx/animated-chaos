namespace Solution.Domain;

public class ProductPrice : ValueObject
{
    public decimal Value { get; private set; }

    private ProductPrice(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Product price have to be positive");

        Value = amount;
    }

    public static ProductPrice FromDecimal(decimal amount) => new ProductPrice(amount);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
