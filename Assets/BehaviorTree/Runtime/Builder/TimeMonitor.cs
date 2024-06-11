using UnityEngine;

namespace BT.Runtime
{
    public class TimeMonitor : ITimeMonitor
    {
        public int DeltaMillisecondsTime => Mathf.FloorToInt(Time.deltaTime * 1000);
    }
}