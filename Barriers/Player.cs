using System;
using CSPlang;

namespace Barriers
{
    public class Player : IamCSProcess
    {
        private int id, nPlayers;
        private CSPBarrier barrier;

        public Player(int id, int nPlayers, CSPBarrier barrier)
        {
            this.id = id;
            this.nPlayers = nPlayers;
            this.barrier = barrier;
        }

        public void run()
        {
            CSTimer timer = new CSTimer();
            long second = 1000; // JCSP timer units are milliseconds
            int busy = id + 1;
            while (true)
            {
                timer.sleep(busy * second); // application specific work
                Console.WriteLine("Player " + id + " at the barrier ...");
                barrier.sync();
                Console.WriteLine("\t\t\t... Player " + id + " over the barrier");
                busy = (nPlayers + 1) - busy; // just to make it more interesting
            }
        }
    }
}