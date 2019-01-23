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

    public class Canteen : IamCSProcess {

    private readonly AltingChannelInput supply;

    // from the cook
    private readonly AltingChannelInput request; // from a philosopher
    private readonly ChannelOutput deliver; // to a philosopher

    public Canteen(AltingChannelInput supply, AltingChannelInput request, ChannelOutput deliver) {
        this.supply = supply;
        this.request = request;
        this.deliver = deliver;
    }

    public void run()
    {

        Guard[] guard = {supply, request};
        Boolean[] preCondition = new Boolean[guard.Length];
        const int SUPPLY = 0;
        const int REQUEST = 1;

        Alternative alt = new Alternative(guard);

        int maxChickens = 20;
        int maxSupply = 4;
        int limitChickens = maxChickens - maxSupply;

        int oneChicken = 1; // ready to go!

        int nChickens = 0; // invariant : 0 <= nChickens <= maxChickens

        while (true)
        {
            preCondition[SUPPLY] = (nChickens <= limitChickens);
            preCondition[REQUEST] = (nChickens > 0);
            switch (alt.priSelect(preCondition))
            {
                case SUPPLY:
                    nChickens += (int) supply.read(); // <= maxSupply
                    break;
                case REQUEST:
                    Object dummy = request.read(); // we have to still input the signal
                    deliver.write(oneChicken); // preCondition ==> (nChickens > 0)
                    nChickens--;
                    break;
            }
        }

    }

    }
}