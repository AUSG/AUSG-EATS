﻿namespace AUSG.Eats.Order.Domain;

public class CartItem
{
    public long? Id { get; set; }
    public List<CartItemOption> Options { get; set; }
    public int Quantity { get; set; }

    // C#에서 지원하는 신기한 생성자 상속(?) 문법
    public CartItem(long? id, List<CartItemOption> options, int quantity) : this(options, quantity)
    {
        Id = id;
    }

    public CartItem(List<CartItemOption> options, int quantity)
    {
        Options = options;
        Quantity = quantity;
    }
    
    
    public override bool Equals(object? obj)
    {
        // use null propagation (잘 이해 못하고 있습니다.)
        // see TC: CSharpTests#compare_with_null_instance_returns_false
        if (obj is not CartItem other)
            return false;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        // Non-readonly property referenced in 'GetHashCode()' ??
        return Id.GetHashCode();
    }
}