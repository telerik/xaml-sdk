using System;

namespace ContentControls
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exporting file...");

            DocumentGenerator.Generate();

            Console.ReadKey();
        }
    }
}
