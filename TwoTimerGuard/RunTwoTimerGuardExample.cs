using CSPlang;
using PlugAndPlay;

namespace TwoTimerGuard
{
    class RunTwoTimerGuardExample
    {
        static void Main(string[] args)
        {
            One2OneChannel a = Channel.one2one();
            One2OneChannel b = Channel.one2one();
            One2OneChannel reset = Channel.one2one();

            new CSPParallel(
                new IamCSProcess[]
                {
                    new Numbers(outChannel: a.Out()),
                    new Regulator2Timers(a.In(), b.Out()),
                    new GPrint(inChannel: b.In(), heading: "Numbers", delay: 1000)
                }
            ).run();
        }
    }
}