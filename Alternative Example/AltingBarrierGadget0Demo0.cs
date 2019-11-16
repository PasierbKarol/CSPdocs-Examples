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
using PlugAndPlay;

namespace Alternative_Example
{
    public class AltingBarrierGadget0Demo0
    {
        public static void main(String[] argv)
        {
            int nUnits = 8;

            nUnits = (int)Console.Read(); //Ask.Int("\nnUnits = ", 3, 10);

            // make the buttons
            One2OneChannel[] _event = Channel.one2oneArray(nUnits);
            One2OneChannel[] configure = Channel.one2oneArray(nUnits);
            Boolean horizontal = true;

            FramedButtonArray buttons =
                new FramedButtonArray(
                    "AltingBarrier: Gadget 0, Demo 0", nUnits, 120, nUnits * 100,
                    horizontal, Channel.getInputArray(configure), Channel.getOutputArray(_event)
                );

            // construct an array of front-ends to a single alting barrier
            AltingBarrier[] group = AltingBarrier.create(nUnits);

            // make the gadgets
            AltingBarrierGadget0[] gadgets = new AltingBarrierGadget0[nUnits];
            for (int i = 0; i < gadgets.Length; i++)
            {
                gadgets[i] = new AltingBarrierGadget0(_event[i].In(), group[i], configure[i].Out());
            }

            // run everything

            new CSPParallel(
                new IamCSProcess[]
                {
                    buttons, new CSPParallel(gadgets)
                }
            ).run();
        }
    }
}