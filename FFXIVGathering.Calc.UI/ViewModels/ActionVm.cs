using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.UI.ViewModels
{
    public class ActionVm : BaseVm
    {
        private readonly IGatheringAction _action;
        private bool _isMiner;
        private string _name;
        private int _usedTimes;

        public ActionVm(IGatheringAction action)
        {
            _action = action;
            _isMiner = false;
            _name = _action.NameBotanist;
            _usedTimes = 1;
        }

        public bool IsMiner 
        { 
            get => _isMiner;
            set
            {
                if (!SetProperty(ref _isMiner, value, nameof(IsMiner)))
                {
                    return;
                }

                Name = IsMiner ? _action.NameMiner : _action.NameBotanist;
            }
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

        public int UsedTimes
        {
            get => _usedTimes;
            set => SetProperty(ref _usedTimes, value, nameof(UsedTimes));
        }
    }
}
