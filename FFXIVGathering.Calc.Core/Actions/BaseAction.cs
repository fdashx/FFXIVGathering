using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Actions
{
    public abstract class BaseAction : IGatheringAction
    {
        public abstract int Level { get; }
        public abstract string NameBotanist { get; }
        public abstract string NameMiner { get; }
        public abstract bool IsRepeatable { get; }
        public abstract int GP { get; }

        public virtual bool CanExecute(GatheringContext context)
        {
            return context.CharacterLevel >= Level && context.AvailableGP >= GP;
        }

        public virtual void Execute(GatheringContext context)
        {
            context.AvailableGP -= GP;
        }
    }
}
