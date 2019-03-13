using System;
using System.Collections.Generic;
using System.Text;
using CSPlang;

namespace RegularTimerExample
{
    public class Regular : IamCSProcess
    {
        readonly private ChannelOutput Out;
        private int N;
        readonly private long interval;

        public Regular(ChannelOutput Out, int n, long interval)
        {
            this.Out = Out;
            this.N = n;
            this.interval = interval;
        }

        public void run()
        {
            CSTimer timer = new CSTimer();
            long timeout = timer.read(); // read the (absolute) time once only

            while (true)
            {
                N++;
                Out.write(N);
                timeout += interval; // set the next (absolute) timeOut
                timer.after(timeout); // wait until that (absolute) timeOut
            }
        }
    }
}