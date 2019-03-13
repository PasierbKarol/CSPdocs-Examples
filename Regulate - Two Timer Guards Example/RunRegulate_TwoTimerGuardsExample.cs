using System;
using CSPlang;
using PlugAndPlay;

namespace Regulate___Two_Timer_Guards_Example
{
    class RunRegulate_TwoTimerGuardsExample
    {
        static void Main(string[] args)
        {
            One2OneChannel a = Channel.one2one();
            One2OneChannel b = Channel.one2one();
            One2OneChannel reset = Channel.one2one();

            var process = new SupportingProcess(reset);

            new CSPParallel(
                new IamCSProcess[]
                {
                    new Variate(a.Out(), 5000, 10, 2),
                    new Regulate(a.In(), reset.In(), b.Out(), 500),
                    new Printer(b.In(), "RegulateTest ==> ", "\n"),
                    process
                }
            ).run();
        }
    }
}