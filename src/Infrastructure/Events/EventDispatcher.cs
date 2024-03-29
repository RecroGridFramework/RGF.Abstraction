﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace Recrovit.RecroGridFramework.Abstraction.Infrastructure.Events;

public class EventDispatcher<TValue>
{
    private event Action<TValue> _event;

    private event Func<TValue, Task> _eventAsync;

    public static EventDispatcher<TValue> operator +(EventDispatcher<TValue> eventHolder, Func<TValue, Task> eventHandler)
    {
        if (eventHandler != null)
        {
            eventHolder.Subscribe(eventHandler);
        }
        return eventHolder;
    }

    public static EventDispatcher<TValue> operator +(EventDispatcher<TValue> eventHolder, Action<TValue> eventHandler)
    {
        if (eventHandler != null)
        {
            eventHolder.Subscribe(eventHandler);
        }
        return eventHolder;
    }

    public static EventDispatcher<TValue> operator -(EventDispatcher<TValue> eventHolder, Func<TValue, Task> eventHandler)
    {
        if (eventHandler != null)
        {
            eventHolder.Unsubscribe(eventHandler);
        }
        return eventHolder;
    }

    public static EventDispatcher<TValue> operator -(EventDispatcher<TValue> eventHolder, Action<TValue> eventHandler)
    {
        if (eventHandler != null)
        {
            eventHolder.Unsubscribe(eventHandler);
        }
        return eventHolder;
    }

    public void Subscribe(Func<TValue, Task> eventHandler) => _eventAsync += eventHandler;

    public void Subscribe(Action<TValue> eventHandler) => _event += eventHandler;

    public void Unsubscribe(Func<TValue, Task> eventHandler) => _eventAsync -= eventHandler;

    public void Unsubscribe(Action<TValue> eventHandler) => _event -= eventHandler;

    public Task InvokeAsync(TValue value)
    {
        _event?.Invoke(value);

        var tasks = _eventAsync?.GetInvocationList()
            .OfType<Func<TValue, Task>>()
            .Select(callback => callback.Invoke(value));

        if (tasks != null)
        {
            return Task.WhenAll(tasks);
        }

        return Task.CompletedTask;
    }
}
