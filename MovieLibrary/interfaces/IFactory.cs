namespace MovieLibrary.interfaces
{
    internal interface IFactory
    {
        public object Create(params object[] args);
    }
}
