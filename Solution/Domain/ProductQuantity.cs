namespace Solution.Domain;

public class ProductQuantity : ValueObject
{
    public int Value { get; private set; }

    public ProductQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException("Cannot have a negetive quantity");

        Value = quantity;
    }
    public static ProductQuantity FromInt(int quantity) => new ProductQuantity(quantity);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
