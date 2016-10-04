﻿using System;
using PinetreeShop.CQRS.Infrastructure.CommandsAndEvents;
using PinetreeShop.Domain.Baskets.Events;
using PinetreeShop.Domain.Types;
using Xunit;
using PinetreeShop.Domain.Baskets.Commands;
using System.Linq;
using PinetreeShop.Domain.Baskets.Exceptions;

namespace PinetreeShop.Domain.Tests.Basket
{
    public class CheckOutTests : TestBase
    {
        Guid id = Guid.NewGuid();
        Guid productId = Guid.NewGuid();
        Address shippingAddress = new Address { Country = "US", StateOrProvince = "CA", StreetAndNumber = "A2", ZipAndCity = "LA" };

        [Fact]
        public void When_CheckOut_CheckedOut()
        {
            Given(InitialEvents);
            When(new CheckOut(id, shippingAddress));
            Then(new CheckedOut(id, shippingAddress));
        }

        [Fact]
        public void When_CheckOutCancelled_ThrowsCheckoutException()
        {
            var initialEvents = InitialEvents.ToList();
            initialEvents.Add(new Cancelled(id));
            Given(initialEvents.ToArray());
            WhenThrows<CheckoutException>(new CheckOut(id, shippingAddress));
        }

        [Fact]
        public void When_RevertCheckOut_CheckOutReverted()
        {
            var initialEvents = InitialEvents.ToList();
            initialEvents.Add(new CheckedOut(id, shippingAddress));
            Given(initialEvents.ToArray());
            When(new RevertCheckOut(id, "reason"));
            Then(new CheckOutReverted(id, "reason"));
        }

        private IEvent[] InitialEvents
        {
            get
            {
                return new IEvent[]
                {
                    new BasketCreated(id),
                    new ProductAdded(id, productId, "Test Product", 2, 10)
                };
            }
        }
    }
}
