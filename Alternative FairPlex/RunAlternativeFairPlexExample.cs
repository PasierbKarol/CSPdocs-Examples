using System;
using System.Threading.Tasks;
using CSPlang;
using PlugAndPlay;


namespace Alternative_FairPlex
{
    class RunAlternativeFairPlexExample
    {
        public static readonly String TITLE = "Fair multiplexing (fair-Alt)";

        public static readonly String DESCR =
            "Shows a fair-Alt in action. Five processes are created which generate numbers at 5ms intervals. " +
            "A multiplexer will use 'fairSelect' to ensure that each of the channels gets served. The output " +
            "shows which data was accepted by the multiplexer. The fairness ensures that the higher numbered " +
            "channels do not get starved. A timeout guard is also used to stop the demonstration after 10 " +
            "seconds. Contrast this with the Pri-Alting demonstration.";

        static void Main(string[] args)
        {
            One2OneChannel[] a = Channel.one2oneArray(5);
            One2OneChannel b = Channel.one2one();

            new CSPParallel(
                new IamCSProcess[]
                {
                    new Regular(a[0].Out(), 0, 5),
                    new Regular(a[1].Out(), 1, 5),
                    new Regular(a[2].Out(), 2, 5),
                    new Regular(a[3].Out(), 3, 5),
                    new Regular(a[4].Out(), 4, 5),
                    new FairPlexTime(Channel.getInputArray(a), b.Out(), 10000),
                    new Printer(b.In(), "FairPlexTimeTest ==> ", "\n")
                }
            ).run();
        }
    }
}