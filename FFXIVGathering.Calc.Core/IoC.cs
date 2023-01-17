using Autofac;
using FFXIVGathering.Calc.Core.Actions;
using FFXIVGathering.Calc.Core.Interfaces;
using FFXIVGathering.Calc.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.Core
{
    public static class IoC
    {
        public static void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<GatheringCalculator>().As<IGatheringCalculator>();
            builder.RegisterType<RotationFactory>().As<IRotationFactory>();
            builder.RegisterType<RotationGenerator>().As<IRotationGenerator>();
            builder.RegisterInstance(new List<IGatheringAction>
            {
                new Chance1(),
                new Chance2(),
                new Chance3(),
                new Gift1(),
                new Bountiful1(),
                new Attempt(),
                new Yield1(),
                new Yield2(),
                new Gift2(),
                new Bountiful2(),
                new Tidings(),
                new AttemptEnhanced()
            }).As<IEnumerable<IGatheringAction>>();
        }
    }
}
