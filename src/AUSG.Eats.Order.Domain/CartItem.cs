namespace AUSG.Eats.Order.Domain;

public class CartItem
{
    public long Id { get; set; }
    public List<CartItemOption> Options { get; set; }
    public int Quantity { get; set;  }

    public CartItem(List<CartItemOption> options, int quantity)
    {
        this.Options = options;
        this.Quantity = quantity;
    }
    
    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(this.GetType() == obj.GetType()))
            return false;
        var another = (CartItem) obj;
        return this.Id == another.Id;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return this.Id.GetHashCode();
    }
}