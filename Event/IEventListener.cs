public interface IEventListener
{
    void HandleEvent(EventType type, params object[] data);
    int EventPriority();
    bool BlockEvent();
}