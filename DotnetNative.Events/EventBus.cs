namespace DotnetNative.Events;
public class EventBus<T> where T : Event
{
    public List<Subscriber<T>> Subscribers = [];

    public bool Invoke(T args)
    {
        foreach (Subscriber<T> subscriber in Subscribers)
            subscriber.Action(args);
        return !args.IsCanceled;
    }

    public void RegisterType(Type listener, Action<T> action, short priority = 0)
    {
        Subscribers.Add(new Subscriber<T>(listener, action, priority));
        Subscribers = Subscribers.OrderBy(z => z.Priority).ToList();
    }

    public void Register(object listener, Action<T> action, short priority = 0) => RegisterType(listener.GetType(), action, priority);

    public void Register<T2>(Action<T> action, short priority = 0) => RegisterType(typeof(T2), action, priority);

    public bool UnregisterType(Type listener, Action<T> action)
    {
        int index = Subscribers.FindIndex(z => z.Listener.Equals(listener) && z.Action.Equals(action));
        Subscribers.RemoveAt(index);
        return index != -1;
    }

    public bool Unregister(object listener, Action<T> action) => UnregisterType(listener.GetType(), action);

    public bool Unegister<T2>(Action<T> action) => UnregisterType(typeof(T2), action);

    public void UnregisterTypeAll(Type listener)
    {
        foreach (Subscriber<T> subscriber in Subscribers)
            if (subscriber.Listener.Equals(listener))
                Subscribers.Remove(subscriber);
    }

    public void UnregisterAll(object listener) => UnregisterTypeAll(listener.GetType());
}