namespace BehaviorTree.Runtime
{
    public abstract class SharedVariable
    {
        public abstract object GetValue();

        public abstract void SetValue(object value);
    }

    public abstract class SharedVariable<T> : SharedVariable
    {
        public T Value { get; set; }

        public override void SetValue(object value)
        {
            Value = (T)value;
        }

        public override object GetValue()
        {
            return Value;
        }
    }
}