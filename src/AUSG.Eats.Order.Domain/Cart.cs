namespace AUSG.Eats.Order.Domain;

public class Cart
{
    public long? UserId { get; set; }
    
    // IList에는 AsReadOnly();가 없다. (why?)
    private readonly List<CartItem> _items = new List<CartItem>();
    
    // IReadOnlyCollection에서 hint로 IEnumerable로 변경함
    // hint로 Expression Body로 변경함
    public IEnumerable<CartItem> Items => _items.AsReadOnly();

    public Cart(long? userId = null)
    {
        UserId = userId;
    }
    
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
        if ((obj == null) || !(GetType() == obj.GetType()))
            return false;
        var another = (Cart) obj;
        return UserId == another.UserId;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return UserId.GetHashCode();
    }
}