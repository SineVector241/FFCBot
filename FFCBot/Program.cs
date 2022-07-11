namespace FFCBot
{
    class Program
    {
        static void Main(string[] args) => new Bot().MainAsync().GetAwaiter().GetResult();
    }
}