namespace Solution.Domain;

public class Product
{
    public ProductName Name { get; private set; }
    public ProductPrice Price { get; private set; }

    // TODO :: implement as a value ofje
    public ProductQuantity Quantity { get; private set; }

    public Product(ProductName name, ProductPrice price, ProductQuantity? quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity ?? new ProductQuantity(1);
    }

    public decimal GetTotalPrice()
    {
        return Price.Value * Quantity.Value;
    }

}
