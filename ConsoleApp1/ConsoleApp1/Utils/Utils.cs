using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class Utils
    {
        public static int Sign(double x) {
            return x > 0 ? 1 : (x < 0 ? -1 : 0);
        }
    }
}
