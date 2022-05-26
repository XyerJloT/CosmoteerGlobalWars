namespace Assets.Scripts.Serialization
{
    public interface IDeserializeAdapter<T>
    {
        public T GetOriginal();
    }

    public interface IDeserializeAdapter<T, Context>
    {
        public T GetOriginal(Context context);
    }
}
