using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonaRaceCreator
{
    class Helper
    {
        static public int IntParse(string x)
        {
            if (int.TryParse(x, out int result))
                return result;
            else
                return 0;
        }

        static public byte ByteParse(string x)
        {
            if (byte.TryParse(x, out byte result))
                return result;
            else
                return 0;
        }

        static public string ReturnNonZero(int v)
        {
            if (v > 0)
                return v.ToString();
            else
                return "";
        }
    }
}
