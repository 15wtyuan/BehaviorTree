using UnityEngine;

namespace BehaviorTree.Runtime
{
    public class TimeMonitor : ITimeMonitor
    {
        public float DeltaTime => Time.deltaTime;
    }
}