using System;
using System.Collections.Generic;
using System.Text;
using CSPlang;

namespace Regulate___Two_Timer_Guards_Example
{
    public class SupportingProcess : IamCSProcess
    {
        One2OneChannel reset;

        public SupportingProcess(One2OneChannel reset)
        {
            this.reset = reset;

        }

        public void run()
        {
            long halfSecond = 500;
            long second = 1000;
            CSTimer tim = new CSTimer();
            long timeout = tim.read();
            while (true)
            {
                timeout += 5000;
                tim.after(timeout);
                Console.WriteLine("                    <== now every second");
                reset.Out().write(second);
                timeout += 5000;
                tim.after(timeout);
                Console.WriteLine("                    <== now every half second");
                reset.Out().write(halfSecond);
            }
        }
    }
}
