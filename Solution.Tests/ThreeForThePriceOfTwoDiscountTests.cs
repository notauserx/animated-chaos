namespace Solution.Tests;

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
