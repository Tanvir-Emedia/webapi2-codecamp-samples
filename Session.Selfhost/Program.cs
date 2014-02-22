using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Sessions.API;

namespace Session.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000";
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Listening on " + baseAddress);
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
