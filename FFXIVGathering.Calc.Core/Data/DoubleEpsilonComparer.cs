using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Data
{
    internal class DoubleEpsilonComparer : IComparer<double>
    {
        private readonly double _epsilon;

        internal DoubleEpsilonComparer(double epsilon)
        {
            _epsilon = epsilon;
        }

        public int Compare(double x, double y)
        {
            var diff = Math.Abs(x - y);

            if (diff < _epsilon)
            {
                return 0;
            }

            return x.CompareTo(y);
        }
    }
}
