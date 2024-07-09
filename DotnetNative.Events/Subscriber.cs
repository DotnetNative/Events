namespace DotnetNative.Events;
public record Subscriber<T>(Type Listener, Action<T> Action, short Priority);