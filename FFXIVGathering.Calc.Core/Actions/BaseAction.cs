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
        public abstract int ExecutionOrder { get; }

        public virtual bool CanExecute(GatheringContext context)
        {
            if (context.CharacterLevel < Level) 
            {
                return false;
            }

            if (context.AvailableGP >= GP)
            {
                return true;
            }
            else if (IsRepeatable)
            {
                return (context.Attempts - 1) * context.GPRegenPerAttempt + context.AvailableGP >= GP;
            }
            else
            {
                return false;
            }
        }

        public virtual void Execute(GatheringContext context)
        {
            context.AvailableGP -= GP;
        }
    }
}
