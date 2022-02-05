using Xunit;

namespace AUSG.Eats.Order.Test;

public class CSharpTests
{
    [Fact(DisplayName = "Integer fields are initialized to 0")]
    public void int_init_value_is_0_in_class()
    {
        var sut = new IntInit();

        Assert.Equal(0, sut.id);
    }

    [Fact(DisplayName = "Nullable integer fields are initialized to 0")]
    public void nullable_int_init_value_is_null_in_class()
    {
        var sut = new IntInit();

        // Assert.Null은 사용할 수 없다.
        // Boxing allocation: conversion from 'int?' to 'object' requires boxing of value type
        // Assert.Null(sut.nullableId);

        // Nullable<int?>의 null 여부를 체크할 수 있는 메소드가 없다.
        Assert.True(sut.nullableId == null);
    }

    [Fact(DisplayName = "Comparing with null instance returns false using null propagation")]
    public void compare_with_null_instance_returns_false()
    {
        var NonNullInstance = new EqualsImplEx(1L);
        var NullInstance = new EqualsImplEx();

        Assert.True(NullInstance.Id == null);
        Assert.NotEqual(NonNullInstance, NullInstance);
    }

    private class IntInit
    {
        public int id { get; set; }
        public int? nullableId { get; set; }
    }

    private class EqualsImplEx
    {
        public EqualsImplEx(long? id = null)
        {
            Id = id;
        }

        public long? Id { get; }

        public override bool Equals(object? obj)
        {
            // use null propagation
            if (obj is not EqualsImplEx other)
                return false;
            return Id == other.Id;
        }
    }
}
