using FFXIVGathering.Calc.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Interfaces
{
    public interface IRotationFactory
    {
        IRotation CreateRotation(GatheringContext context);

        IRotation Copy(IRotation rotation);
    }
}
