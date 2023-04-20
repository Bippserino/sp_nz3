using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nz3
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceMonitor rm = new ResourceMonitor();
            
            while (true)
               
            {
                Console.WriteLine(rm.GetInfo());
                rm.LogToXml();
                Thread.Sleep(300000);
                rm.Update();
            }
        }
    }
}
