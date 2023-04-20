using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace nz3
{
    class ResourceMonitor
    {
        private List<IMonitor> monitors;
        private string[] output;

        public ResourceMonitor()
        {
            monitors = new List<IMonitor>();
            monitors.Add(new CPUMonitor());
            monitors.Add(new RamMonitor());
            monitors.Add(new DiskMonitor());
            monitors.Add(new NetworkMonitor());
            output = new string[4];
        }

        public void Update()
        {
            foreach(var monitor in monitors)
            {
                monitor.Update();
            }
        }

        public string GetInfo()
        {
            string output = "";
            foreach (var monitor in monitors)
            {
                output += monitor.GetInfo();
            }
            return output;
        }

        public void LogToXml()
        {
            string fileName = "log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".xml";

            if (!File.Exists(fileName))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    writer.WriteStartElement("logEntries");
                    writer.WriteEndElement();
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNode logEntry = doc.CreateElement("logEntry");

            XmlNode time = doc.CreateElement("time");
            time.InnerText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            XmlNode cpu = doc.CreateElement("cpu");
            cpu.InnerText = monitors[0].GetInfo();

            XmlNode ram = doc.CreateElement("ram");
            ram.InnerText = monitors[1].GetInfo();

            XmlNode disk = doc.CreateElement("disk");
            disk.InnerText = monitors[2].GetInfo();

            XmlNode network = doc.CreateElement("network");
            network.InnerText = monitors[3].GetInfo();

            logEntry.AppendChild(time);
            logEntry.AppendChild(cpu);
            logEntry.AppendChild(ram);
            logEntry.AppendChild(disk);
            logEntry.AppendChild(network);

            XmlNode root = doc.SelectSingleNode("logEntries");
            root.AppendChild(logEntry);

            doc.Save(fileName);
        }
    }
}
