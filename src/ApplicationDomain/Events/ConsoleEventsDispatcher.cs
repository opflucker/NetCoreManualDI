﻿using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain.Events
{
    internal class ConsoleEventsDispatcher : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            Console.WriteLine($"Dispatched event: {@event}");
            return Task.CompletedTask;
        }
    }
}