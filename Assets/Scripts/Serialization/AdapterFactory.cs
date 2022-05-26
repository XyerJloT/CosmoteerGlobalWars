namespace Assets.Scripts.Serialization
{
    public static class AdapterFactory<T, Adapter> where Adapter : ISerializeAdapter<T>, new()
    {
        public static Adapter Create(T value)
        {
            var adapter = new Adapter();
            adapter.SetOriginal(value);

            return adapter;
        }
    }
}
