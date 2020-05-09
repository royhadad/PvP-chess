using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    static class Utilities
    {
        public static string ListToString<T>(List<T> list)
        {
            string str = "";
            foreach (T item in list)
                str += item.ToString()+"\n";
            return str;
        }
    }

    class ChessException : Exception { }
    class KingNotFoundException : ChessException { }

}
