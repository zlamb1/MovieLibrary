namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Startup().ConfigureServices();
        }
    }
}


