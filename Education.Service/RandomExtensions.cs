using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class RandomExtensions
    {
        public static T Next<T>(this Random rnd, params T[] variants)
        {

            return variants[rnd.Next(0, variants.Length)];
        }
    }
}
