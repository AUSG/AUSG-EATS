namespace AUSG.Eats.Order.Domain;

public class CartItem
{
    private long? _id;

    public CartItem(long? id)
    {
        _id = id;
    }

    public override bool Equals(object? obj)
    {
        // use null propagation (잘 이해 못하고 있습니다.)
        // see TC: CSharpTests#compare_with_null_instance_returns_false
        if (obj is not CartItem other)
            return false;
        return _id == other._id;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return _id.GetHashCode();
    }
}
