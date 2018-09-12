using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication
{
  public  class PCLock
    {
        public static readonly object Locker = new object();
    }
}
