namespace BehaviorTree.Runtime
{
    public class SharedFloat : SharedVariable<float>
    {
        public static implicit operator SharedFloat(float value)
        {
            return new SharedFloat { Value = value };
        }
    }
}