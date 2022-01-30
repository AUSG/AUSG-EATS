using System.Collections.ObjectModel;
using AUSG.Eats.Order.Domain;
using Xunit;

namespace AUSG.Eats.Order.Test;

public class OrderTests
{
    
    private CartItem MakeNewCartItem()
    {
        var options = new List<CartItemOption>();
        const int quantity = 1;
        return new CartItem(options, quantity);
    }
    
    [Fact(DisplayName = "CartItemOption 객체는 Name과 Price 필드를 노출한다.")]
    public void CartItemOption_Shares_Name_and_Price()
    {
        const string name = "name";
        const decimal price = 1000m;
        var cartItemOption = new CartItemOption(name, price);
        
        Assert.Equal(cartItemOption.Name, name);
        Assert.Equal(cartItemOption.Price, price);
    }
    
    [Fact(DisplayName = "CartItem 객체는 Id, List<CarItemOption>, Quantity 필드를 노출한다.")]
    public void CartItem_Shares_Id_and_ListOfCartItemOption_and_Quantity()
    {
        const long pid = 1L;
        var options = new List<CartItemOption>();
        const int quantity = 0;
        var cartItem = new CartItem(options, quantity);
        cartItem.Id = pid;
        
        Assert.Equal(cartItem.Options, options); // IEnumerable ?
        Assert.Equal(cartItem.Quantity, quantity);
        Assert.Equal(cartItem.Id, pid);
    }

    [Fact(DisplayName = "Cart 객체는 List<CartItem> 필드를 노출한다.")]
    public void Cart_Shares_ListOfCartItem()
    {
        var cart = new Cart();
        Assert.Empty(cart.Items); // Collection && size=0
    }

    [Fact(DisplayName = "Cart 객체가 노출하는 List<CartItem>은 외부에서 그 내용을 수정할 수 없어야 한다.")]
    public void ListOfCartItem_of_Cart_Must_Not_Be_Mutable()
    {
        var cart = new Cart();
        
        Assert.True(cart.Items is ReadOnlyCollection<CartItem>);
    }

    [Fact(DisplayName = "Cart 객체는 UserId 필드를 노출하며 UserId로 식별할 수 있다.")]
    public void Cart_Shares_UserId_and_Can_be_Identified_Using_UserId()
    {
        var cart1 = new Cart();
        var cart2 = new Cart();
        var userId = 1L;
        cart1.UserId = userId;
        cart2.UserId = userId;
        
        // Shares Id
        Assert.Equal(cart1.UserId, userId);
        
        // Identified by UserId
        Assert.Equal(cart1, cart2);
        
        // Compare HashCode
        Assert.Equal(cart1.GetHashCode(), cart2.GetHashCode());
    }

    [Fact(DisplayName = "Cart 객체는 CartItem을 추가할 수 있다.")]
    public void Cart_can_Add_CartItem()
    {
        var cart = new Cart();
        var cartItem1 = MakeNewCartItem();
        cart.AddToCart(cartItem1);
        
        Assert.Equal(cart.Items.ElementAt(0), cartItem1);
    }
    
    [Fact(DisplayName = "Cart 객체는 CartItem을 제거할 수 있다.")]
    public void Cart_can_Remove_CartItem()
    {
        // given
        var cart = new Cart();
        var cartItem1 = MakeNewCartItem();
        cart.AddToCart(cartItem1);
        Assert.Equal(cart.Items.ElementAt(0), cartItem1);
        
        // when
        cart.RemoveFromCart(cartItem1);

        // then
        Assert.Empty(cart.Items);
    }
        
    [Fact(DisplayName = "Cart 객체는 CartItem을 변경할 수 있다.")]
    public void Cart_can_Alter_CartItem()
    {
        // given
        const long cartItemId = 1L; 
        var cart = new Cart();
        var oldItem = MakeNewCartItem();
        oldItem.Id = cartItemId;
        cart.AddToCart(oldItem);
        
        // when (Item의 상태를 변경함)
        // 가능한 모든 필드가 변경됨을 확인하는 게 적절할 것으로 보임
        var newOptions = new List<CartItemOption>();
        const int newQuantity = 2;
        oldItem.Options = newOptions;
        oldItem.Quantity = newQuantity;
        cart.AlterCartItem(oldItem);

        // then
        var alteredItem = cart.Items.ElementAt(0);
        Assert.Equal(alteredItem.Id, oldItem.Id);
        Assert.Equal(alteredItem.Options, newOptions); // Collection의 새 레퍼런스의 일치 확인해도 충분하다.
        Assert.Equal(alteredItem.Quantity, newQuantity);
    }
    
    // 추가 TC
    [Fact(DisplayName = "Cart 객체는 CartItem을 변경할 때 해당 Item이 이미 List에 없다면 예외를 발생시킨다.")]
    public void Cart_throw_ArgumentException_when_Alter_CartItem()
    {
        // given
        var cart = new Cart();
        var oldItem = MakeNewCartItem();
        oldItem.Id = 1L;
        
        // when (Item의 상태를 변경함)
        // 가능한 모든 필드가 변경됨을 확인하는 게 적절할 것으로 보임
        Action alterItem = () => cart.AlterCartItem(oldItem);

        // then
        ArgumentException ex = Assert.Throws<ArgumentException>(alterItem);
    }
}
