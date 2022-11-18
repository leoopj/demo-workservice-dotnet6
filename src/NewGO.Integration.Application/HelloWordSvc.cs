using NewGO.Integration.Model;

namespace NewGO.Integration.Application
{
    public class HelloWordSvc : IHelloWordSvc
    {
        private string? _helloWord;

        public void HelloWordSync()
        {
            Task.Run(() => WriteHelloWord());
            Console.WriteLine(_helloWord);
        }

        internal async Task WriteHelloWord()
        {
            _helloWord = "Hello Word";
            await Task.CompletedTask;
        }

    }
}