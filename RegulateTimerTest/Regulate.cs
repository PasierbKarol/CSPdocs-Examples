using CSPlang;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegulateTimerTest
{
    public class Regulate : IamCSProcess
    {
        private readonly AltingChannelInput In, reset;
        private readonly ChannelOutput Out;
        private readonly long initialInterval;

        /**
          * Construct the process.
          * 
          * @param in the input channel
          * @param out the output channel
          * @param initialInterval the initial interval between outputs (in milliseconds)
          * @param reset send a <tt>Long</tt> down this to change the interval between outputs (in milliseconds)
          */
        public Regulate( AltingChannelInput In,  AltingChannelInput reset,
         ChannelOutput Out,  long initialInterval)
        {
            this.In = In;
            this.reset = reset;
            this.Out = Out;
            this.initialInterval = initialInterval;
        }

        /**
          * The main body of this process.
          */
        public void run()
        {

            CSTimer tim = new CSTimer();

            Guard[] guards = { reset, tim, In};              // prioritised order
           const int RESET = 0;                                  // index into guards
            const int TIM = 1;                                    // index into guards
            const int IN = 2;                                     // index into guards

            Alternative alt = new Alternative(guards);

            Object x = 1;                                      // holding object

            long interval = initialInterval;

            long timeout = tim.read() + interval;
            tim.setAlarm(timeout);

            while (true)
            {
                switch (alt.priSelect())
                {
                    case RESET:
                        //interval = ((long)reset.read()).longValue();
                        interval = (long)reset.read();
                        timeout = tim.read();                          // fall through
                        Out.write(x);
                        timeout += interval;
                        tim.setAlarm(timeout);
                        break;
                    case TIM:
                        Out.write(x);
                        timeout += interval;
                        tim.setAlarm(timeout);
                        break;
                    case IN:
                        x = In.read();
                        break;
                }
            }

        }

    }
}
