using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPDLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //przykladowe wywolania funkcji
            XPDLManager xpdl = new XPDLManager("xpdl.xml");
            
            string []next = new string[3];
            next = xpdl.GetNext("ADDING_PRODUCTS_RESPONSE");
            Console.WriteLine(next[0]);

            xpdl.SetCurrentActivity("RESPONSE_TO_ARCHIVE");

            Console.WriteLine(xpdl.GetPreviousActivity());
            next = xpdl.GetNext();
            Console.WriteLine(next[0]);

            Console.WriteLine(xpdl.GetActivityName("CLIENT_OFFER_REJECTED"));



        }
    }
}
