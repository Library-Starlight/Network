using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Extension
{
    public static class ListExtensions
    {
        public static T NextElement<T>(this List<T> ls, T current)
        {
            var index = ls.IndexOf(current);
            return ls[index + 1];
        }
    }
}
