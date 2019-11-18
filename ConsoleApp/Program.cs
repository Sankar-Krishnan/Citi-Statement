using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StatemeentProcessor.Classes;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StatementGenerator gen = new StatementGenerator();
            gen.StartProcessing();
            
            Console.WriteLine("Hello World!");
        }
    }
}
