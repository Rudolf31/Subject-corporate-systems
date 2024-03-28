using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS2_2project
{
    public static class Pointer
    {
        public static int Grade(int p)
        {
            if (90 <= p && p <= 100)
            {
                return 5;
            }

            if (80 <= p && p <= 90)
            {
                return 4;
            }

            if (70 <= p && p <= 80)
            {
                return 3;
            }
            if (60 <= p && p <= 70)
            {
                return 2;
            }
            return 1;
        }
    }
}

