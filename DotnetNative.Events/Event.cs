namespace DotnetNative.Events;
public class Event
{
    // RAAAAAH 🦅🦅🦅 AMERICAN CANCELED, FUCK BRITISH CANCELLED, SLOWPOKE LANGUAGE
    public bool IsCanceled { get; private set; } = false;
    public void SetCanceled(bool cancel) => IsCanceled = cancel;
}