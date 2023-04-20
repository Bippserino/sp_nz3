using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nz3
{
    public class DiskMonitor : IMonitor
    {
        private List<DriveInfo> drives;

        private List<String> names;
        private List<String> labels;
        private List<String> formats;
        private List<long> availableFreeSpace;
        private List<long> totalFreeSpace;
        private List<long> totalSize;

        public DiskMonitor()
        {
            drives = new List<DriveInfo>();
            drives.AddRange(DriveInfo.GetDrives());

            names = new List<string>(new string[drives.Count]);
            labels = new List<string>(new string[drives.Count]);
            formats = new List<string>(new string[drives.Count]);
            availableFreeSpace = new List<long>(new long[drives.Count]);
            totalFreeSpace = new List<long>(new long[drives.Count]);
            totalSize = new List<long>(new long[drives.Count]);
            this.Update();
        }

        public void Update()
        {

            for(int driveIndex = 0; driveIndex < drives.Count; driveIndex++)
            {
                names[driveIndex] = drives[driveIndex].Name;

                if (drives[driveIndex].IsReady == true)
                {
                    formats[driveIndex] = drives[driveIndex].DriveFormat;
                    labels[driveIndex] = drives[driveIndex].VolumeLabel;
                    availableFreeSpace[driveIndex] = drives[driveIndex].AvailableFreeSpace / (1024 * 1024);
                    totalFreeSpace[driveIndex] = drives[driveIndex].TotalSize / (1024 * 1024);
                    totalSize[driveIndex] = drives[driveIndex].TotalSize / (1024 * 1024);
                }
            }
        }

        public string GetInfo()
        {
            this.Update();

            string info = "";

            for (int driveIndex = 0; driveIndex < drives.Count; driveIndex++)
            {
                info += String.Format("\n\nDrive {0}", names[driveIndex]);
                info += String.Format("\nVolume label: {0}", labels[driveIndex]);
                info += String.Format("\nFile system: {0}", formats[driveIndex]);
                info += String.Format(
                    "\nAvailable space to current user:{0, 15} MB",
                    availableFreeSpace[driveIndex]);
                info += String.Format(
                    " \nTotal available space:          {0, 15}  MB",
                    totalFreeSpace[driveIndex]);

                info += String.Format(
                    "\nTotal size of drive:            {0, 15}  MB",
                    totalSize[driveIndex]);      
                

            }
            return info;
        }
    }
}
