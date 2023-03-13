namespace Solution.Domain;

public interface IProductDiscountStrategy
{
    decimal GetDiscountedPrice(Product product);
}
