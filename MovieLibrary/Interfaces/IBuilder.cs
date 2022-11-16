namespace MovieLibrary.Interfaces
{
    internal interface IBuilder<T>
    {
        T Build(params object[] args);
    }
}
