namespace Solution.Tests;

public class ShoppingCartTests
{
    [Fact]
    public void shopping_cart_is_initially_empty()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        Assert.Equal(0, cart.GetNumberOfProducts());
    }

    [Fact]
    public void when_a_product_is_added_to_the_cart_the_number_of_products_is_increased()
    {
        ShoppingCart cart = ShoppingCart.CreateEmptyCart();

        var currentProductCount = 0;
        Assert.Equal(currentProductCount, cart.GetNumberOfProducts());

        cart.StoreProduct(new Product(new ProductName("Product A"),
                                      ProductPrice.FromDecimal(1),
                                      ProductQuantity.FromInt(1),
                                      null));

        currentProductCount++;
        Assert.Equal(currentProductCount, cart.GetNumberOfProducts());
    }

    [Fact]
    public void sum_of_items_returns_the_sum_of_product_prices()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(1),
                                      null));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(1),
                                      null));

        Assert.Equal(30, cart.GetSumOfItems());

    }

    [Fact]
    public void products_can_have_discounts()
    {

        var discountedProduct = new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(1),
                                      new ThreeForThePriceOfTwo()
                                );

        Assert.True(discountedProduct.HasADiscount);
    }

    [Fact]
    public void products_can_have_no_discounts()
    {

        var discountedProduct = new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(1),
                                      null
                                );

        Assert.False(discountedProduct.HasADiscount);
    }

}

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

public class ThreeForThePriceOfTwoDiscountTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void when_less_than_three_items_discount_does_not_apply(int quantity)
    {
        var pricePerItem = 20;
        var discountedProduct = new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(pricePerItem),
                                      ProductQuantity.FromInt(quantity),
                                      new ThreeForThePriceOfTwo()
                                );

        var expected = pricePerItem * quantity;

        Assert.Equal(expected, discountedProduct.GetPriceWithDiscount());
    }

    [Theory]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(18)]
    [InlineData(3)]
    public void when_quantity_is_a_multiple_of_three_discount_applies_to_each_multiple(int quantity)
    {
        var pricePerItem = 20;
        var multipleOfThree = quantity / 3;
        var discountedProduct = new Product(new ProductName("Jeans"),
                                     ProductPrice.FromDecimal(pricePerItem),
                                      ProductQuantity.FromInt(quantity),
                                      new ThreeForThePriceOfTwo()
                                );

        var expected = pricePerItem * 2 * multipleOfThree;
        var discountedPrice = discountedProduct.GetPriceWithDiscount();

        Assert.Equal(expected, discountedPrice);
    }

    [Fact]
    public void when_quantity_is_four_three_items_get_discounted_and_one_item_gets_no_discount()
    {
        var pricePerItem = 20;
        var discountedProduct = new Product(new ProductName("Jeans"),
                                    ProductPrice.FromDecimal(pricePerItem),
                                     ProductQuantity.FromInt(4),
                                     new ThreeForThePriceOfTwo()
                               );

        var expected = pricePerItem * 2 + pricePerItem;
        var discountedPrice = discountedProduct.GetPriceWithDiscount();

        Assert.Equal(expected, discountedPrice);
    }

    [Fact]
    public void when_quantity_is_five_three_items_get_discounted_and_one_item_gets_no_discount()
    {
        var pricePerItem = 20;
        var discountedProduct = new Product(new ProductName("Jeans"),
                                    ProductPrice.FromDecimal(pricePerItem),
                                     ProductQuantity.FromInt(5),
                                     new ThreeForThePriceOfTwo()
                               );

        var expected = 2 * pricePerItem + pricePerItem + pricePerItem;
        var discountedPrice = discountedProduct.GetPriceWithDiscount();

        Assert.Equal(expected, discountedPrice);
    }
}