namespace MovieLibrary.interfaces
{
    internal interface IFileDao
    {
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public object[] Read();
    }
}
