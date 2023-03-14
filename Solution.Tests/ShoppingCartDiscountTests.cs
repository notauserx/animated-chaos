namespace Solution.Tests;

public class ShoppingCartDiscountTests
{
    [Fact]
    public void when_less_than_two_tshirt_and_two_jeans_in_cart_discounted_price_is_not_set()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(1)));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                      ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(2)));

        var discountStrategy = new TwoTShirtAndTwoJeansDiscountStrategy();

        Assert.False(discountStrategy.IsApplicable(cart));

        var discountedPrice = discountStrategy.GetDiscountedPrice(cart);

        // not setting discounted price for t - shirt 5HKD and jeans 10hkd
        var expectedPrice = 1 * 10 + 2 * 20;

        Assert.Equal(expectedPrice, discountedPrice);
    }

    [Fact]
    public void when_two_tshirt_and_two_jeans_in_cart_discounted_price_is_set()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(2)));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                      ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(2)));

        var discountStrategy = new TwoTShirtAndTwoJeansDiscountStrategy();

        Assert.True(discountStrategy.IsApplicable(cart));

        var discountedPrice = discountStrategy.GetDiscountedPrice(cart);

        // setting original price for the first pair and the discounted price for the second pair
        var expectedPrice = (1 * 10 + 1 * 20) + (1 * 5 + 1 * 10);

        Assert.Equal(expectedPrice, discountedPrice);
    }

    [Fact]
    public void only_two_set_items_in_cart__gets_discounted_price()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(3)));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                      ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(3)));

        var discountStrategy = new TwoTShirtAndTwoJeansDiscountStrategy();

        Assert.True(discountStrategy.IsApplicable(cart));

        var discountedPrice = discountStrategy.GetDiscountedPrice(cart);

        // setting discounted price only for the second pair
        var expectedPrice = 1 * 5 + 1 * 10 + 2 * 10 + 2 * 20;

        Assert.Equal(expectedPrice, discountedPrice);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(10)]
    public void all_two_set_items_in_cart__gets_discounted_price(int setCount)
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(2 * setCount)));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                      ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(2 * setCount)));

        var discountStrategy = new TwoTShirtAndTwoJeansDiscountStrategy();

        Assert.True(discountStrategy.IsApplicable(cart));

        var discountedPrice = discountStrategy.GetDiscountedPrice(cart);

        // setting original price for the first pair and the discounted price for the second pair for each pair
        var expectedPricePerSet = (1 * 10) + (1 * 20) + (1 * 5) + (1 * 10);
        var expectedPrice = expectedPricePerSet * setCount;

        Assert.Equal(expectedPrice, discountedPrice);
    }
}
