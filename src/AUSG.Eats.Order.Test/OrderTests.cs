using AUSG.Eats.Order.Domain;
using Xunit;

namespace AUSG.Eats.Order.Test;

public class OrderTests
{
    private CartItem MakeNewCartItem(long? id = null)
    {
        return new CartItem(id);
    }

    [Fact(DisplayName = "CartItem 객체는 Id로 식별할 수 있다.")]
    public void CartItem_Can_Be_Identified_By_Id()
    {
        // given
        const long pid = 1L;
        var cartItem1 = MakeNewCartItem(pid);
        var cartItem2 = MakeNewCartItem(pid);

        // then
        Assert.Equal(cartItem1, cartItem2);
        Assert.Equal(cartItem1.GetHashCode(), cartItem2.GetHashCode());
    }

    [Fact(DisplayName = "Cart 객체는 UserId로 식별할 수 있다.")]
    public void Cart_Shares_UserId_and_Can_be_Identified_Using_UserId()
    {
        // given
        var userId = 1L;
        var cart1 = new Cart(userId);
        var cart2 = new Cart(userId);

        // then
        Assert.Equal(cart1, cart2);
        Assert.Equal(cart1.GetHashCode(), cart2.GetHashCode());
    }

    [Fact(DisplayName = "Cart 객체는 CartItem을 추가할 수 있다.")]
    public void Cart_can_Add_CartItem()
    {
        // given
        var cart = new Cart();
        var cartItem1 = MakeNewCartItem();

        // when
        cart.AddToCart(cartItem1);

        // then
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
        var oldItem = MakeNewCartItem(cartItemId);
        cart.AddToCart(oldItem);

        // when
        var newItem = MakeNewCartItem(cartItemId);
        cart.AlterCartItem(newItem);

        // then
        var alteredItem = cart.Items.ElementAt(0);
        Assert.Equal(alteredItem, oldItem);
    }

    [Fact(DisplayName = "Cart 객체는 CartItem을 변경할 때 해당 Item이 이미 List에 없다면 예외를 발생시킨다.")]
    public void Cart_throw_ArgumentException_when_Alter_CartItem()
    {
        // given
        var cart = new Cart();
        const long cartItemIdWhichDoesNotExist = 1L;
        var oldItem = MakeNewCartItem(cartItemIdWhichDoesNotExist);

        // when
        void AlterItem()
        {
            cart.AlterCartItem(oldItem);
        }

        // then
        Assert.Throws<ArgumentException>((Action) AlterItem);
    }
}
