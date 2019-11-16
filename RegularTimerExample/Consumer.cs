using System;
using CSPlang;

namespace RegularTimerExample
{
    public class Consumer : IamCSProcess
    {
        ChannelInput inChannel;

        public Consumer(ChannelInput inChannel)
        {
            this.inChannel = inChannel;
        }

        public void run()
        {
            int i = 1;
            while (i > 0)
            {
                i = (int)inChannel.read();
                Console.WriteLine("\nThe input was " + i);
            }

            Console.WriteLine("Finished!");
        }
    }
}