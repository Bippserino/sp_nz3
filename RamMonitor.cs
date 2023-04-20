using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace nz3
{
    class RamMonitor : IMonitor
    {
        private PerformanceCounter ramCounter;
        private double available;
        private double total;
        public RamMonitor()
        {
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            this.Update();
        }

        public string GetInfo()
        {
            this.Update();
            return "\nUsed RAM:" + (this.total - this.available) + " MB\nAvailable RAM: " + this.available + "MB";
        }


        public void Update()
        {
            available = Math.Round(ramCounter.NextValue(), 2);
            total = GetTotalMemory();
        }

        public long GetTotalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
            ManagementObjectCollection objColl = searcher.Get();

            foreach (ManagementObject obj in objColl)
            {
                return Convert.ToInt64(obj["TotalVisibleMemorySize"]) / 1024;
               
            }
            return 0;
        }
    }
}
