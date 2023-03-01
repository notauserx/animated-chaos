namespace Solution.Domain;

public class ShoppingCart
{
    public IList<Product> Products { get; private set; }

    private ShoppingCart()
    {
        Products = new List<Product>();
    }
    public void StoreProduct(Product product)
    {
        Products.Add(product);
    }

    public int GetNumberOfProducts() => Products.Count;

    public static ShoppingCart CreateEmptyCart() => new ShoppingCart();

    public decimal GetSumOfItems()
    {
        return Products.Sum(p => p.GetPriceWithDiscount());
    }
}


public class Product
{
    public ProductName Name { get; private set; }
    public ProductPrice Price { get; private set; }

    // TODO :: implement as a value ofje
    public ProductQuantity Quantity { get; private set; }

    public IDiscountStrategy DiscountStrategy { get; private set; }

    public Product(ProductName name, ProductPrice price, ProductQuantity? quantity, IDiscountStrategy discountStrategy)
    {
        Name = name;
        Price = price;
        Quantity = quantity ?? new ProductQuantity(1);
        DiscountStrategy = discountStrategy;
    }

    public bool HasADiscount => DiscountStrategy is not null;

    /// <summary>
    /// Returns the discounted price is the product has a discount
    /// Otherwise returns the product price * product quantity
    /// </summary>
    /// <returns></returns>
    public decimal GetPriceWithDiscount()
    {
        return HasADiscount
            ? DiscountStrategy.GetDiscountedPrice(this)
            : GetTotalPrice();
    }

    private decimal GetTotalPrice()
    {
        return Price.Value * Quantity.Value;
    }

}

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

public interface IDiscountStrategy
{
    decimal GetDiscountedPrice(Product product);
}

public class ThreeForThePriceOfTwo : IDiscountStrategy
{
    public decimal GetDiscountedPrice(Product product)
    {
        var maxThreeItems = product.Quantity.Value / 3;
        var remainingItems = product.Quantity.Value - maxThreeItems * 3;

        return remainingItems * product.Price.Value + maxThreeItems * 2 * product.Price.Value;
    }
}