//////////////////////////////////////////////////////////////////////
//                                                                  //
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

using CSPlang;

namespace Alternative_FairPlex
{
    /**
     * @author P.H. Welch
     */
    public class FairPlex : IamCSProcess
    {
        private readonly AltingChannelInput[] In;
        private readonly ChannelOutput Out;

        public FairPlex(AltingChannelInput[] In, ChannelOutput Out)
        {
            this.In = In;
            this.Out = Out;
        }

        public void run()
        {
            Alternative alt = new Alternative(In);

            while (true)
            {
                int index = alt.fairSelect();
                Out.write(In[index].read());
            }
        }
    }
}