namespace Solution.Domain;

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
        var tShirtCount = 0;
        var jeansCount = 0;

        foreach (var cartProduct in cart.Products)
        {
            if (cartProduct.Name.Equals(TShirtProductName))
            {
                tShirtCount += cartProduct.Quantity.Value;
            }
            else if (cartProduct.Name.Equals(JeansProductName))
            {
                jeansCount += cartProduct.Quantity.Value;
            }
        }

        return tShirtCount >= 2 && jeansCount >= 2;
    }
}
