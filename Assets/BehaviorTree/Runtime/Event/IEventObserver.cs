namespace BT.Runtime
{
    public interface IEventObserver
    {
        void OnNotify(string eventType);
    }
}