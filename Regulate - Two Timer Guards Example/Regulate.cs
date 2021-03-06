﻿//////////////////////////////////////////////////////////////////////
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

/**
 * @author P.H. Welch
 */
namespace Regulate___Two_Timer_Guards_Example
{
    public class Regulate : IamCSProcess
    {
        private AltingChannelInput In, reset;
        private ChannelOutput Out;
        private long initialInterval;

        public Regulate(AltingChannelInput In, AltingChannelInput reset, ChannelOutput Out, long initialInterval)
        {
            this.In = In;
            this.reset = reset;
            this.Out = Out;
            this.initialInterval = initialInterval;
        }

        public void run()
        {
            CSTimer tim = new CSTimer();

            Guard[] guards = { reset, tim, In }; // prioritised order
            const int RESET = 0; // index into guards
            const int TIM = 1; // index into guards
            const int IN = 2; // index into guards

            Alternative alt = new Alternative(guards);

            Object x = null; // holding object

            long interval = initialInterval;

            long timeout = tim.read() + interval;
            tim.setAlarm(timeout);

            while (true)
            {
                switch (alt.priSelect())
                {
                    case RESET:
                        interval = (long)reset.read();
                        timeout = tim.read();
                        break;
                    // fall through
                    case TIM:
                        interval = (long)reset.read();
                        timeout = tim.read();
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