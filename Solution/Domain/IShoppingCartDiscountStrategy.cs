namespace Solution.Domain;

public interface IShoppingCartDiscountStrategy
{
    bool IsApplicable(ShoppingCart cart);
    decimal GetDiscountedPrice(ShoppingCart cart);
}
