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
using  PlugAndPlay;

/**
 * @author P.H. Welch
 */

namespace Polling_Alternative_Skip_Guard_Example
{
    public class RunPolling
    {
        public static readonly String TITLE = "Polling Multiplexor";
        public static readonly String DESCR =
        "Shows a pri-Alt with a skip guard being used to poll the inputs. Five processes generate numbers " +
        "at 1s, 2s, 3s, 4s and 5s intervals. The number generated indicates the process generating it. If " +
        "no data is available on a polling cycle the polling process will wait for 400ms before polling " +
        "again. It could however be coded to perform some useful computation between polling cycles.\n" +
        "\n" +
        "The polling is unfair although this is not noticeable with these timings. If the interval at " +
        "which the numbers are generated is shortened then the higher numbered processes may become starved.";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Ask.app(TITLE, DESCR);
            //Ask.show();
            //Ask.blank();

            One2OneChannel[] a = Channel.one2oneArray(5);
            One2OneChannel b = Channel.one2one();

            new CSPParallel(
                new IamCSProcess[] {
                    new Regular (a[0].Out(), 1, 1000),
                    new Regular (a[1].Out(), 2, 2000),
                    new Regular (a[2].Out(), 3, 3000),
                    new Regular (a[3].Out(), 4, 4000),
                    new Regular (a[4].Out(), 5, 5000),
                    new Polling (a[0].In(), a[1].In(), a[2].In(), a[3].In(), a[4].In(), b.Out()),
                    new Printer (b.In(), "PollingTest ==> ", "\n")
                }
            ).run();


            Console.ReadKey();
        }
    }
}
