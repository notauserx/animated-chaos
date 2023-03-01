namespace Solution.Domain;

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