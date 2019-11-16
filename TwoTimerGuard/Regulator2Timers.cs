using CSPlang;

namespace TwoTimerGuard
{
    public class Regulator2Timers : IamCSProcess
    {
        ChannelInput In;
        ChannelOutput Out;

        public Regulator2Timers(ChannelInput In, ChannelOutput Out)
        {
            this.In = In;
            this.Out = Out;
        }

        public void run()
        {
            CSTimer tim = new CSTimer();
            CSTimer reset = new CSTimer();

            Guard[] guards = { reset, tim, In as Guard }; // prioritised order
            const int RESET = 0; // index into guards
            const int TIM = 1; // index into guards
            const int IN = 2; // index into guards

            Alternative alt = new Alternative(guards);

            int x = 0;
            long timeout = tim.read() + 3000;
            tim.setAlarm(timeout);

            long resetting = reset.read() + 5000;
            reset.setAlarm(resetting);

            while (true)
            {
                switch (alt.priSelect())
                {
                    case RESET:
                        resetting = reset.read() + 5000;
                        reset.setAlarm(resetting);
                        Out.write("Numbers reset");
                        break;
                    // fall through
                    case TIM:
                        timeout = tim.read();
                        Out.write(x * 10);
                        timeout += 3000;
                        tim.setAlarm(timeout);
                        break;
                    case IN:
                        x = (int)In.read();
                        Out.write(x);
                        break;
                }
            }
        }
    }
}