using System;
using CSPlang;
using CSPlang.Alting;
using PlugAndPlay;

namespace Alternative_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            One2OneChannel channel1 = Channel.one2one();
            One2OneChannel channel2 = Channel.one2one();

            AltingExample altingExample = new AltingExample(channel1.In(), channel2.In());
            Numbers num = new Numbers(channel1.Out());
            Numbers num2 = new Numbers(channel2.Out());

            //Alting input example
            IamCSProcess[] altingInputExample = { num, num2, altingExample };
            new CSPParallel(altingInputExample).run();


            //================ Alting Output Example =========================
            //AltingOutputExample altingOutput = new AltingOutputExample((AltingChannelOutput)channel1.Out(), (AltingChannelOutput)channel2.Out());
            //IamCSProcess[] altingOutputExample = { num, num2, altingOutput };

            //Alting Gadget example ============================
            //AltingBarrier altingBarrier = new AltingBarrier();

            //new CSPParallel(altingOutputExample).run();

            Console.ReadKey();

        }
    }
}
