using System;
using System.Threading.Tasks;
using CSPlang;
using CSPutil;
using PlugAndPlay;

namespace RegulateTimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! TEST REGULATE");

            One2OneChannel a = Channel.createOne2One();
            One2OneChannel b = Channel.createOne2One();
            One2OneChannel c = Channel.createOne2One();

            One2OneChannel reset = Channel.createOne2One(new OverWriteOldestBuffer(1));

            Numbers num = new Numbers(a.Out());
            FixedDelay fixedDelay = new FixedDelay(250, a.In(), b.Out());
            Regulate regulate = new Regulate(b.In(), reset.In(), c.Out(), 1000);
            LoopsProcess loops = new LoopsProcess(c.In(), reset.Out());

            IamCSProcess[] network = {num, fixedDelay, regulate, loops};

            new CSPParallel(network).run();
            Console.ReadKey();

        }
    }
}