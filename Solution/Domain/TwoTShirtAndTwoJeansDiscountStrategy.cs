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
        var totalTShirts = cart.Products.Where(p => p.Name == TShirtProductName).Sum(i => i.Quantity.Value);
        var totalJeans = cart.Products.Where(p => p.Name == JeansProductName).Sum(i => i.Quantity.Value);

        var twoTShirtGroup =    totalTShirts / 2;
        var twoJeansGroup =     totalJeans / 2;

        // the discount set would be the minimun of twoJeans grounp and two t-shirt group.
        var discountSetCount = Math.Min(twoJeansGroup, twoTShirtGroup);

        // for each set, a single t-shirt is discounted
        var discountedTShirtsCount    = discountSetCount;
        var nonDiscountedTShirtsCount = totalTShirts  - discountedTShirtsCount;

        // for each set, a single jeans is discounted
        var discountedJeansCount    = discountSetCount;
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
