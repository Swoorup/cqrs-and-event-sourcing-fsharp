﻿using PinetreeShop.CQRS.Infrastructure.Commands;
using PinetreeShop.CQRS.Infrastructure.Events;
using System.Collections.Generic;

namespace PinetreeShop.CQRS.Persistence
{
    public interface IEventStore
    {
        IEnumerable<IEvent> Events { get; }
        void CommitEvents(IEnumerable<IEvent> events);

        IEnumerable<ICommand> Commands { get; }
        void DispatchCommands(IEnumerable<ICommand> commands);
    }
}