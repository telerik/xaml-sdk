using System;

namespace HtmlGenerator
{
    class Program
    {
        static void Main()
        {
            DocumentGenerator generator = new DocumentGenerator();
            generator.Generate();

            Console.Read();
        }
    }
}
