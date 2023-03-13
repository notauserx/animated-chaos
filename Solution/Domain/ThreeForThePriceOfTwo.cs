namespace Solution.Domain;

public class ThreeForThePriceOfTwo : IProductDiscountStrategy
{
    public decimal GetDiscountedPrice(Product product)
    {
        var maxThreeItems = product.Quantity.Value / 3;
        var remainingItems = product.Quantity.Value - maxThreeItems * 3;

        return remainingItems * product.Price.Value + maxThreeItems * 2 * product.Price.Value;
    }
}
