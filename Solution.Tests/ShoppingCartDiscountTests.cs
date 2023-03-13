namespace Solution.Tests;

public class ShoppingCartDiscountTests
{
    [Fact]
    public void test_case_one()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(2),
                                      null));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(2),
                                      null));

        var discountStrategy = new TwoTShirtAndTwoJeansDiscountStrategy();

        Assert.True(discountStrategy.IsApplicable(cart));

        var discountedPrice = discountStrategy.GetDiscountedPrice(cart);

        Assert.Equal(30, discountedPrice);
    }
}
