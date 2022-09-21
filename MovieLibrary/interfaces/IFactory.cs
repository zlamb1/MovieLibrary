namespace MovieLibrary.interfaces
{
    internal interface IFactory<T>
    {
        public T Create(params object[] args);
    }
}
