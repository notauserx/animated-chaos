namespace Solution.Tests;

public class DummyObjectTests
{
    [Fact]
    public void dummy_object_with_same_values_should_be_equal()
    {
        var one = new DummyObject("abc");
        var another = new DummyObject("abc");

        Assert.Equal(one, another);
    }

    [Fact]
    public void dummy_object_with_different_values_should_not_be_equal()
    {
        var one = new DummyObject("abc");
        var another = new DummyObject("ab");

        Assert.NotEqual(one, another);
    }
}