using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core.Models
{
    public class Rotation : IRotation
    {
        public Rotation(GatheringContext context)
        {
            Context = context;
            Actions = new List<IGatheringAction>();
        }

        public IList<IGatheringAction> Actions { get; }

        public GatheringContext Context { get; }

        public void AddAction(IGatheringAction action)
        {
            Actions.Add(action);
            action.Execute(Context);
        }
    }
}
