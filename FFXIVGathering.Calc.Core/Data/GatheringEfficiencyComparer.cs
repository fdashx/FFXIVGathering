using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Data
{
    public class GatheringEfficiencyComparer : IComparer<GatheringOutcome>
    {
        private readonly DoubleEpsilonComparer _doubleComparer;

        public GatheringEfficiencyComparer()
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

            var result = _doubleComparer.Compare(x.AddYieldPerGP, y.AddYieldPerGP);

            if (result == 0)
            {
                return _doubleComparer.Compare(x.Yield, y.Yield);
            }

            return result;
        }
    }
}
