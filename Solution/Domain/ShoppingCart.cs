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
