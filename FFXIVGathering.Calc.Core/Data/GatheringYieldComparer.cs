using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Data
{
    public class GatheringYieldComparer : IComparer<GatheringOutcome>
    {
        private readonly DoubleEpsilonComparer _doubleComparer;

        public GatheringYieldComparer()
        {
            _doubleComparer = new DoubleEpsilonComparer(1e-6);
        }

        public int Compare(GatheringOutcome? x, GatheringOutcome? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null && y != null)
            {
                return -1;
            }

            if (x != null && y == null)
            {
                return 1;
            }

            var result = _doubleComparer.Compare(x.Yield, y.Yield);

            if (result == 0)
            {
                return -x.UsedGP.CompareTo(y.UsedGP);
            }

            return result;
        }
    }
}
