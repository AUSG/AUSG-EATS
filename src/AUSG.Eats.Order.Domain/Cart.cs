namespace AUSG.Eats.Order.Domain;

public class Cart
{
    public long UserId { get; set; }
    
    // IList에는 AsReadOnly();가 없다. (why?)
    private readonly List<CartItem> _items = new List<CartItem>();
    
    // IReadOnlyCollection에서 hint로 IEnumerable로 변경함
    // hint로 Expression Body로 변경함
    public IEnumerable<CartItem> Items => this._items.AsReadOnly();

    public void AddToCart(CartItem newItem)
    {
        this._items.Add(newItem);
    }

    public void RemoveFromCart(CartItem itemToRemove)
    {
        this._items.Remove(itemToRemove);
    }

    public void AlterCartItem(CartItem itemToChange)
    {
        if (!this._items.Remove(itemToChange)) // Id 비교이므로 제거된다.
            throw new ArgumentException("없는 CartItem을 변경하려고 한다.");
        this._items.Add(itemToChange);
    }
    
    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(this.GetType() == obj.GetType()))
            return false;
        var another = (Cart) obj;
        return this.UserId == another.UserId;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return this.UserId.GetHashCode();
    }
}