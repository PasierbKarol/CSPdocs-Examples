//////////////////////////////////////////////////////////////////////
//                                                                  //
//  jcspDemos Demonstrations of the JCSP ("CSP for Java") Library   //
//  Copyright (C) 1996-2018 Peter Welch, Paul Austin and Neil Brown //
//                2001-2004 Quickstone Technologies Limited         //
//                2005-2018 Kevin Chalmers                          //
//                                                                  //
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

namespace Canteen_Alternative_With_Preconditions_Example
{
    public class Canteen : IamCSProcess
    {
        private AltingChannelInput service;

        // shared from all Philosphers (any-1)
        private ChannelOutput deliver; // shared to all Philosphers (but only used 1-1)
        private AltingChannelInput supply; // from the Chef (1-1)

        public Canteen(AltingChannelInput service, AltingChannelInput supply, ChannelOutput deliver)
        {
            this.service = service;
            this.deliver = deliver;
            this.supply = supply;
        }

        public void run()
        {
            Alternative alt = new Alternative(new Guard[] { supply, service });
            Boolean[] precondition = { true, false };
            const int SUPPLY = 0;
            const int SERVICE = 1;

            CSTimer timer = new CSTimer();

            int nChickens = 0;

            Console.WriteLine("            Canteen : starting ... ");
            while (true)
            {
                precondition[SERVICE] = (nChickens > 0);
                switch (alt.fairSelect(precondition))
                {
                    case SUPPLY:
                        int value = (int)supply.read(); // new batch of chickens from the Chef
                        Console.WriteLine("            Canteen : ouch ... make room ... this dish is very hot ... ");
                        timer.after(timer.read() + 3000); // this takes 3 seconds to put down
                        nChickens += value;
                        Console.WriteLine("            Canteen : more chickens ... " +
                                          nChickens + " now available ... ");
                        supply.read(); // let the Chef get back to cooking
                        break;
                    case SERVICE:
                        service.read(); // Philosopher wants a chicken
                        Console.WriteLine("      Canteen : one chicken coming down ... " +
                                          (nChickens - 1) + " left ... ");
                        deliver.write(1); // serve one chicken
                        nChickens--;
                        break;
                }
            }
        }
    }
}