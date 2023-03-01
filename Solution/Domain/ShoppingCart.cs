namespace Solution.Domain;

public class ShoppingCart
{
    public IList<Product> Products { get; private set; }

    //public IShoppingCartDiscountStrategy? DiscountStrategy { get; private set; }

    private ShoppingCart()
    {
        Products = new List<Product>();
    }
    public void StoreProduct(Product product)
    {
        Products.Add(product);
    }

    public int GetNumberOfProducts() => Products.Count;

    public static ShoppingCart CreateEmptyCart() => new();

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

    public IProductDiscountStrategy DiscountStrategy { get; private set; }

    public Product(ProductName name, ProductPrice price, ProductQuantity? quantity, IProductDiscountStrategy discountStrategy)
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

public interface IProductDiscountStrategy
{
    decimal GetDiscountedPrice(Product product);
}

public class ThreeForThePriceOfTwo : IProductDiscountStrategy
{
    public decimal GetDiscountedPrice(Product product)
    {
        var maxThreeItems = product.Quantity.Value / 3;
        var remainingItems = product.Quantity.Value - maxThreeItems * 3;

        return remainingItems * product.Price.Value + maxThreeItems * 2 * product.Price.Value;
    }
}

public interface IShoppingCartDiscountStrategy
{
    bool IsApplicable(ShoppingCart cart);
    decimal GetDiscountedPrice(ShoppingCart cart);
}

public class TwoTShirtAndTwoJeansDiscountStrategy : IShoppingCartDiscountStrategy
{
    public static ProductName TShirtProductName => new ProductName("T-Shirt");
    public static ProductName JeansProductName => new ProductName("Jeans");
    public decimal GetDiscountedPrice(ShoppingCart cart)
    {
        if(!IsApplicable(cart))
        {
            return cart.GetSumOfItems();
        }

        var tShirtPrice = cart.Products.First(x => x.Name == TShirtProductName).Price.Value;
        var jeansPrice = cart.Products.First(x => x.Name == JeansProductName).Price.Value;

        // get total tshirts and jeans
        var totalTShirts = cart.Products.Count(p => p.Name == TShirtProductName);
        var totalJeans = cart.Products.Count(p => p.Name == JeansProductName);

        var twoTShirtGroup =    totalTShirts / 2;
        var twoJeansGroup =     totalJeans / 2;

        // the discount set would be the minimun of twoJeans grounp and two t-shirt group.
        var discountSetCount = Math.Min(twoJeansGroup, twoTShirtGroup);

        var discountedTShirtsCount    = discountSetCount * 2;
        var nonDiscountedTShirtsCount = totalTShirts  - discountedTShirtsCount;

        var discountedJeansCount    = discountSetCount * 2;
        var nonDiscountedJeansCount = totalJeans    - discountedJeansCount;

        // now calculate the discounted price
        var tShirtPriceTotal = (discountedTShirtsCount * tShirtPrice) / 2 + nonDiscountedTShirtsCount * tShirtPrice;
        var jeansPriceTotal = (discountedJeansCount * jeansPrice) / 2 + nonDiscountedJeansCount * jeansPrice;

        return tShirtPriceTotal + jeansPriceTotal;
    }

    public bool IsApplicable(ShoppingCart cart)
    {
        var tShirtCount = cart.Products.Where(p => p.Name == TShirtProductName).Count(i => i.Quantity.Value);
        var jeansCount  = cart.Products.Where(p => p.Name == JeansProductName).Count(i => i.Quantity.Value);

        return tShirtCount >= 2 && jeansCount >= 2;
    }
}
