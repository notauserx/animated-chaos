using Solution.Framework;

namespace Solution.Tests;

/// <summary>
/// A value object for demo purposes
/// </summary>
public class DummyObject : ValueObject
{
    public string Value { get; private set; }

    public DummyObject(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
public class ValueObjectTests
{
    private static DummyObject One = new ("abc");
    private static DummyObject SameAsOne = new ("abc");
    private static DummyObject Another = new ("ab");

    [Fact]
    public void value_object_with_same_values_should_be_equal()
    {
        Assert.Equal(One, SameAsOne);
    }

    [Fact]
    public void value_object_with_different_values_should_not_be_equal()
    {
        Assert.NotEqual(One, Another);
    }

    [Fact]
    public void comparing_value_object_with_same_values_with_equality_operator_should_return_true()
    {
        Assert.True(One == SameAsOne);
    }

    [Fact]
    public void comparing_value_object_with_same_values_with_non_equality_operator_should_return_false()
    {
        Assert.False(One != SameAsOne);
    }

    [Fact]
    public void comparing_value_object_with_different_values_with_non_equality_operator_should_return_true()
    {
        Assert.True(One != Another);
    }

    [Fact]
    public void comparing_value_object_with_different_values_with_equality_operator_should_return_false()
    {
        Assert.False(One == Another);
    }
}