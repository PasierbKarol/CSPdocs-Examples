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

namespace AltingBarrierTesting
{
    public class AltingBarrierExample
    {
        public static void Main(String[] argv)
        {
            int nUnits = 8;

            Console.WriteLine("Enter number of units: ");
            nUnits = (int)Console.Read(); //Ask.Int("\nnUnits = ", 3, 10);

            // make the buttons
            One2OneChannel[] _event = Channel.one2oneArray(nUnits);
            One2OneChannel[] configure = Channel.one2oneArray(nUnits);

            // construct an array of front-ends to a single alting barrier
            AltingBarrier[] group = AltingBarrier.create(nUnits);

            // make the gadgets
            AltingBarrierExampleProcess[] gadgets = new AltingBarrierExampleProcess[nUnits];
            for (int i = 0; i < gadgets.Length; i++)
            {
                gadgets[i] = new AltingBarrierExampleProcess(_event[i].In(), group[i], configure[i].Out());
            }

            //new CSPParallel(
            //    new IamCSProcess[]
            //    {
            //        buttons, new CSPParallel(gadgets)
            //    }
            //).run();

        }

    }
}