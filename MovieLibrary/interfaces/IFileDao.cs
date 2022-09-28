namespace MovieLibrary.interfaces
{
    internal interface IFileDao
    {
        public object[] Args { get; set; }
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public object[] Read();
    }
}
