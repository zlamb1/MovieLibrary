namespace MovieLibrary.Interfaces
{
    internal interface IUpdater<T>
    {
        public void Update(T item, int fieldNum, string val);
    }
}
