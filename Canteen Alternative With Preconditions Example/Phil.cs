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
    /**
     * @author P.H. Welch
     */
    class Phil : IamCSProcess {

    //A Philosopher thinks for a while -- around 3 seconds -- and then goes to the
    //Canteen for food, consuming what he gets straight away.   This cycle continues
    //indefinitely.
    //
    //Except, that is, for Philosopher 0 ...  who refuses to think and just keeps
    //going to the Canteen.
    //
    //For this Canteen, when there's no chicken, the Philosphers are just kept
    //waiting in the service queue.  The greedy Philosopher no longer loses his
    //place through getting in before the food is cooked and doesn't starve.

    private int id;

    private ChannelOutput service;
    private ChannelInput deliver;

    public Phil(int id, ChannelOutput service, ChannelInput deliver)
    {
        this.id = id;
        this.service = service;
        this.deliver = deliver;
    }

    public void run()
    {
        CSTimer tim = new CSTimer();
        Console.WriteLine("      Phil " + id + "  : starting ... ");
        while (true)
        {
            // everyone, bar Philosopher 0, has a little think
            if (id > 0)
            {
                tim.after(tim.read() + 3000); // thinking
            }

                // want chicken
            Console.WriteLine("      Phil " + id + "  : gotta eat ... ");
            service.write(0);
            deliver.read();
            Console.WriteLine("      Phil " + id + "  : mmm ... that's good ... ");
        }
    }

    }
}