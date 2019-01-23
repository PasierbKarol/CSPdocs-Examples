using System;
using System.Threading.Tasks;
using CSPlang;
using CSPlang.Any2;

namespace Canteen_Alternative_With_Preconditions_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int n_philosophers = 5;

            Any2OneChannel service = Channel.any2one();
            One2OneChannel deliver = Channel.one2one();
            One2OneChannel supply = Channel.one2one();

            Phil[] phil = new Phil[n_philosophers];
            for (int i = 0; i < n_philosophers; i++)
            {
                phil[i] = new Phil(i, service.Out(), deliver.In());
            }

            IamCSProcess[] network =
            {
                new Clock(),
                new Canteen(service.In(),  supply.In(), deliver.Out()),
                new Chef(supply.Out()),
                new CSPParallel(phil)
            };

            new CSPParallel(network).run();
        }
    }
}