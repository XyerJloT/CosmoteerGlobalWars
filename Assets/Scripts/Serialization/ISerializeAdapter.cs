namespace Assets.Scripts.Serialization
{
    public interface ISerializeAdapter<T>
    {
        public void SetOriginal(T value);
    }
}
