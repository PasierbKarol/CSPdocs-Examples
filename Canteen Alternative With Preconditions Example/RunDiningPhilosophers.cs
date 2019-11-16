using CSPlang;
using CSPlang.Any2;

namespace Canteen_Alternative_With_Preconditions_Example
{
    class RunDiningPhilosophers
    {
        static void Main(string[] args)
        {
            int n_philosophers = 5;

            Any2OneChannel service = Channel.any2one();
            One2OneChannel deliver = Channel.one2one();
            One2OneChannel supply = Channel.one2one();

            Philosopher[] philosopher = new Philosopher[n_philosophers];
            for (int i = 0; i < n_philosophers; i++)
            {
                philosopher[i] = new Philosopher(i, service.Out(), deliver.In());
            }

            IamCSProcess[] network =
            {
                new Clock(),
                new Canteen(service.In(), supply.In(), deliver.Out()),
                new Chef(supply.Out()),
                new CSPParallel(philosopher)
            };

            new CSPParallel(network).run();
        }
    }
}