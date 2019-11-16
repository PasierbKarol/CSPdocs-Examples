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

using System.Diagnostics;
using System.Threading;
using CSPlang;

namespace Polling_Alternative_Skip_Guard_Example
{
    /**
     * @author P.H. Welch
     */
    public class Polling : IamCSProcess
    {
        private readonly AltingChannelInput In0;
        private readonly AltingChannelInput In1;
        private readonly AltingChannelInput In2;
        private readonly AltingChannelInput In3;
        private readonly AltingChannelInput In4;
        private readonly ChannelOutput Out;

        public Polling(
            AltingChannelInput in0, 
            AltingChannelInput in1,
            AltingChannelInput in2, 
            AltingChannelInput in3,
            AltingChannelInput in4, 
            ChannelOutput Out)
        {
            this.In0 = in0;
            this.In1 = in1;
            this.In2 = in2;
            this.In3 = in3;
            this.In4 = in4;
            this.Out = Out;
        }

        public void run()
        {
            Skip skip = new Skip();
            Guard[] guards = { In0, In1, In2, In3, In4, skip };
            Alternative alt = new Alternative(guards);

            while (true)
            {
                switch (alt.priSelect())
                {
                    case 0:
                        // ...  process data pending on channel In0
                        Out.write(In0.read());
                        Debug.WriteLine(" 0 ready");
                        break;
                    case 1:
                        // ...  process data pending on channel In1
                        Out.write(In1.read());
                        Debug.WriteLine(" 1 ready");

                        break;
                    case 2:
                        // ...  process data pending on channel In2
                        Out.write(In2.read());
                        Debug.WriteLine(" 2 ready");

                        break;
                    case 3:
                        // ...  process data pending on channel In2
                        Out.write(In3.read());
                        Debug.WriteLine(" 3 ready");

                        break;
                    case 4:
                        // ...  process data pending on channel In2
                        Out.write(In4.read());
                        Debug.WriteLine(" 4 ready");

                        break;
                    case 5:
                        // ...  nothing available for the above ...
                        // ...  so get on with something else for a while ...
                        // ...  then loop around and poll again ...
                        try
                        {
                            Debug.WriteLine(" Skip ready");
                            Thread.Sleep(400);
                        }
                        catch (ThreadInterruptedException e)
                        {
                        }

                        Out.write("...  so getting on with something else for a while ...");
                        break;
                }
            }
        }
    }
}