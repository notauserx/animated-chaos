namespace Solution.Domain;

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
