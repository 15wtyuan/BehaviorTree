namespace BehaviorTree.Runtime
{
    public interface IEventObserver
    {
        void OnNotify(string eventType);
    }
}