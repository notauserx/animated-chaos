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
                                      ProductQuantity.FromInt(1)));

        currentProductCount++;
        Assert.Equal(currentProductCount, cart.GetNumberOfProducts());
    }

    [Fact]
    public void sum_of_items_returns_the_sum_of_product_prices()
    {
        var cart = ShoppingCart.CreateEmptyCart();

        cart.StoreProduct(new Product(new ProductName("T-Shirt"),
                                      ProductPrice.FromDecimal(10),
                                      ProductQuantity.FromInt(1)));

        cart.StoreProduct(new Product(new ProductName("Jeans"),
                                      ProductPrice.FromDecimal(20),
                                      ProductQuantity.FromInt(1)));

        Assert.Equal(30, cart.GetSumOfItems());

    }
}
