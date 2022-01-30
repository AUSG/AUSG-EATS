namespace AUSG.Eats.Order.Domain;

public class CartItemOption
{
    public string Name { get; }
    public decimal Price { get; }

    public CartItemOption(string name, decimal price)
    {
        this.Name = name;
        this.Price = price;
    }
}
