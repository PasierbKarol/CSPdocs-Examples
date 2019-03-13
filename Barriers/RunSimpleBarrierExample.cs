using System;
using CSPlang;

namespace Barriers
{
    class RunSimpleBarrierExample
    {
        static void Main(string[] args)
        {
            int nPlayers = 10;

            CSPBarrier barrier = new CSPBarrier(nPlayers);

            IamCSProcess[] players = new Player[nPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(i, nPlayers, barrier);
            }

            new CSPParallel(players).run();
        }
    }
}