using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nz3
{
    interface IMonitor
    {
        string GetInfo();
        void Update();
    }
}
