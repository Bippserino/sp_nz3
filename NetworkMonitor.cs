using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace nz3
{
    class NetworkMonitor : IMonitor
    {
        private List<string> instances;
        private List<float> sent;
        private List<float> received;
        private List<PerformanceCounter> pcsent;
        private List<PerformanceCounter> pcreceived;
        

        public NetworkMonitor()
        {
            PerformanceCounterCategory pcg = new PerformanceCounterCategory("Network Interface");

            instances = new List<string>();
            instances.AddRange(pcg.GetInstanceNames());

            pcsent = new List<PerformanceCounter>(new PerformanceCounter[instances.Count]);
            pcreceived = new List<PerformanceCounter>(new PerformanceCounter[instances.Count]);
            sent = new List<float>(new float[instances.Count]);
            received = new List<float>(new float[instances.Count]);

            for (int instanceIndex = 0; instanceIndex < instances.Count; instanceIndex++)
            {
                pcsent[instanceIndex] = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instances[instanceIndex]);
                pcreceived[instanceIndex] = new PerformanceCounter("Network Interface", "Bytes Received/sec", instances[instanceIndex]);
            }

            this.Update();
        }
        public string GetInfo()
        {
            this.Update();
            string info = "";

            for (int instanceIndex = 0; instanceIndex < instances.Count; instanceIndex++)
            {
                info += String.Format("\n\n{0}:\nSent: {1} MB\nReceived: {2} MB", instances[instanceIndex] , Math.Round(sent[instanceIndex] / (1024 * 1024), 2), Math.Round(received[instanceIndex] / (1024 * 1024), 2));

            }
            return info;
        }

        public void Update()
        {

            for (int instanceIndex = 0; instanceIndex < instances.Count; instanceIndex++)
            {
                sent[instanceIndex] = (pcsent[instanceIndex]).NextValue();
                received[instanceIndex] = (pcreceived[instanceIndex]).NextValue();
            }            
        }
    }
}
