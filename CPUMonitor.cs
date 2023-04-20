using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nz3
{
    public class CPUMonitor : IMonitor
    {
        private PerformanceCounter cpuCounter;
        private float usage;

        public CPUMonitor()
        {
            cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            this.Update();
        }

        public string GetInfo()
        {
            this.Update();
            return "\nProcessor usage: " + Math.Round(cpuCounter.NextValue(), 2) + "%";
        }

        public void Update()
        {
            usage = cpuCounter.NextValue();
        }
    }
}
