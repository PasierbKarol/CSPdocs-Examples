//////////////////////////////////////////////////////////////////////
//                                                                  //
//  jcspDemos Demonstrations of the JCSP ("CSP for Java") Library   //
//  Copyright (C) 1996-2018 Peter Welch, Paul Austin and Neil Brown //
//                2001-2004 Quickstone Technologies Limited         //
//                2005-2018 Kevin Chalmers                          //
//                                                                  //
//  You may use this work under the terms of either                 //
//  You may use this work under the terms of either                 //
//  1. The Apache License, Version 2.0                              //
//  2. or (at your option), the GNU Lesser General Public License,  //
//       version 2.1 or greater.                                    //
//                                                                  //
//  Full licence texts are included in the LICENCE file with        //
//  this library.                                                   //
//                                                                  //
//  Author contacts: P.H.Welch@kent.ac.uk K.Chalmers@napier.ac.uk   //
//                                                                  //
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using CSPlang;


/**
 * @author P.H. Welch
 */
namespace Regulate___Two_Timer_Guards_Example
{
    public class Variate : IamCSProcess
    {
        private ChannelOutput Out;
        private int start, stop, n;

        public Variate(ChannelOutput Out, int start, int stop, int n)
        {
            this.Out = Out;
            this.start = start;
            this.stop = stop;
            this.n = n;
        }

        public void run()
        {
            int innerCycleTime = n * start;

            CSTimer tim = new CSTimer();
            long timeout = tim.read();

            while (true)
            {
                int interval = start;
                while (interval >= stop)
                {
                    int innerCycles = innerCycleTime / interval;
                    for (int i = 0; i < innerCycles; i++)
                    {
                        Out.write(interval);
                        timeout += (long) interval;
                        tim.after(timeout);
                    }

                    interval /= 2;
                }
            }
        }
    }
}