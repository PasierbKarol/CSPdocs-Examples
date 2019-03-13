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
using CSPlang;

namespace Alternative_FairPlex
{
    /**
     * @author P.H. Welch
     */
    public class FairPlexTime : IamCSProcess
    {
        private readonly AltingChannelInput[] In;
        private readonly ChannelOutput Out;
        private readonly long timeout;

        public FairPlexTime(AltingChannelInput[] In, ChannelOutput Out, long timeout)
        {
            this.In = In;
            this.Out = Out;
            this.timeout = timeout;
        }

        public void run()
        {
            Guard[] guards = new Guard[In.Length + 1];
            Array.Copy(In, 0, guards, 0, In.Length);

            CSTimer tim = new CSTimer();
            int timerIndex = In.Length;
            guards[timerIndex] = tim;

            Alternative alt = new Alternative(guards);

            Boolean running = true;
            tim.setAlarm(tim.read() + timeout);
            while (running)
            {
                int index = alt.fairSelect();
                if (index == timerIndex)
                {
                    running = false;
                }
                else
                {
                    Out.write(In[index].read());
                }
            }

            Console.WriteLine("Goodbye from FairPlexTime ...");
            //System.exit(0);
        }
    }
}