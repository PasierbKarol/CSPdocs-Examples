using System;
using CSPlang;

namespace RegularTimerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            One2OneChannel regularTimer = Channel.one2one();
            Regular regular = new Regular(regularTimer.Out(), 1, 1000);
            Consumer consumer = new Consumer(regularTimer.In());

            IamCSProcess[] network = { regular, consumer };

            new CSPParallel(network).run();

            Console.ReadKey();
        }
    }
}
