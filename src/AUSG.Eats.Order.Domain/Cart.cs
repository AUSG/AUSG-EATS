namespace AUSG.Eats.Order.Domain;

public class Cart
{
    private readonly List<CartItem> _items = new();

    private long? _userId;

    public Cart(long? userId = null)
    {
        _userId = userId;
    }

    public IEnumerable<CartItem> Items => _items.AsReadOnly();

    public void AddToCart(CartItem newItem)
    {
        _items.Add(newItem);
    }

    public void RemoveFromCart(CartItem itemToRemove)
    {
        _items.Remove(itemToRemove);
    }

    public void AlterCartItem(CartItem itemToChange)
    {
        if (!_items.Remove(itemToChange)) // Id 비교이므로 제거된다.
            throw new ArgumentException("없는 CartItem을 변경하려고 한다.");
        _items.Add(itemToChange);
    }

    public override bool Equals(object? obj)
    {
        // use null propagation (잘 이해 못하고 있습니다.)
        // see TC: CSharpTests#compare_with_null_instance_returns_false
        if (obj is not Cart other)
            return false;
        return _userId == other._userId;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return _userId.GetHashCode();
    }
}
