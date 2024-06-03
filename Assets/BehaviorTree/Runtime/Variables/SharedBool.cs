namespace BehaviorTree.Runtime
{
    public class SharedBool : SharedVariable<bool>
    {
        public static implicit operator SharedBool(bool value)
        {
            return new SharedBool { Value = value };
        }
    }
}