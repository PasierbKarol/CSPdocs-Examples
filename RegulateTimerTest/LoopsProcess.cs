using System;
using System.Collections.Generic;
using System.Text;
using CSPlang;

namespace RegulateTimerTest
{
    public class LoopsProcess : IamCSProcess
    {
        ChannelInput c;
        ChannelOutput reset;

        public LoopsProcess(ChannelInput c, ChannelOutput reset)
        {
            this.c = c;
            this.reset = reset;
        }

        public void run()
        {
            long[] sample = {1000, 250, 100};
            int[] count = {10, 40, 100};
            while (true)
            {
                for (int cycle = 0; cycle < sample.Length; cycle++)
                {
                    reset.write(sample[cycle]);
                    Console.WriteLine("\nSampling every " + sample[cycle] +
                                      " ms ...\n");
                    for (int i = 0; i < count[cycle]; i++)
                    {
                        int n = (int) c.read();
                        Console.WriteLine("\t==> " + n);
                    }
                }
            }
        }
    }
}
