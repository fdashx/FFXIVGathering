using FFXIVGathering.Calc.Core.Data;
using FFXIVGathering.Calc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FFXIVGathering.Calc.UI.ViewModels
{
    public class MainVm : BaseVm, IMainVm
    {
        private readonly IRotationGenerator _rotationGenerator;
        private readonly IGatheringCalculator _calculator;
        private readonly IRotationFactory _rotationFactory;
        private int _characterLevel;
        private int _availableGP;
        private int _attempts;
        private int _baseYield;
        private double _chance;
        private double _boonChance;
        private int _boonBonus;
        private int _bountifulBonus;
        private double _highestYield;
        private int _highestGP;
        private double _highestYieldPer100GP;
        private double _efficientYield;
        private int _efficientGP;
        private double _efficientYieldPer100GP;
        private bool _isMiner;
        private double _baseTotalYield;

        public MainVm(IRotationGenerator rotationGenerator, IGatheringCalculator calculator, IRotationFactory rotationFactory)
        {
            _rotationGenerator = rotationGenerator;
            _calculator = calculator;
            _rotationFactory = rotationFactory;
            CalculateCmd = new RelayCommand(GetBestRotations);
            HighestRotation = new ObservableCollection<ActionVm>();
            EfficientRotation = new ObservableCollection<ActionVm>();
        }
        public int CharacterLevel
        {
            get => _characterLevel;
            set => SetProperty(ref _characterLevel, value, nameof(CharacterLevel));
        }

        public int AvailableGP
        {
            get => _availableGP;
            set => SetProperty(ref _availableGP, value, nameof(AvailableGP));
        }

        public int Attempts
        {
            get => _attempts;
            set => SetProperty(ref _attempts, value, nameof(Attempts));
        }

        public int BaseYield
        {
            get => _baseYield;
            set => SetProperty(ref _baseYield, value, nameof(BaseYield));
        }

        public double Chance
        {
            get => _chance;
            set => SetProperty(ref _chance, value, nameof(Chance));
        }

        public double BoonChance
        {
            get => _boonChance;
            set => SetProperty(ref _boonChance, value, nameof(BoonChance));
        }

        public int BountifulBonus
        {
            get => _bountifulBonus;
            set => SetProperty(ref _bountifulBonus, value, nameof(BountifulBonus));
        }

        public ObservableCollection<ActionVm> HighestRotation { get; }

        public double HighestYield
        {
            get => _highestYield;
            set => SetProperty(ref _highestYield, value, nameof(HighestYield));
        }

        public int HighestGP
        {
            get => _highestGP;
            set => SetProperty(ref _highestGP, value, nameof(HighestGP));
        }

        public double HighestYieldPer100GP
        {
            get => _highestYieldPer100GP;
            set => SetProperty(ref _highestYieldPer100GP, value, nameof(HighestYieldPer100GP));
        }

        public ObservableCollection<ActionVm> EfficientRotation { get; }

        public double EfficientYield
        {
            get => _efficientYield;
            set => SetProperty(ref _efficientYield, value, nameof(EfficientYield));
        }

        public int EfficientGP
        {
            get => _efficientGP;
            set => SetProperty(ref _efficientGP, value, nameof(EfficientGP));
        }

        public double EfficientYieldPer100GP
        {
            get => _efficientYieldPer100GP;
            set => SetProperty(ref _efficientYieldPer100GP, value, nameof(EfficientYieldPer100GP));
        }

        public double BaseTotalYield
        {
            get => _baseTotalYield;
            set => SetProperty(ref _baseTotalYield, value, nameof(BaseTotalYield));
        }

        public ICommand CalculateCmd { get; }

        public bool IsMiner
        {
            get => _isMiner;
            set
            {
                if (!SetProperty(ref _isMiner, value, nameof(IsMiner)))
                {
                    return;
                }

                foreach (var action in HighestRotation)
                {
                    action.IsMiner = value;
                }

                foreach (var action in EfficientRotation)
                {
                    action.IsMiner = value;
                }
            }
        }

        public void LoadSettings(IMainSettings settings)
        {
            CharacterLevel = settings.CharacterLevel;
            AvailableGP = settings.AvailableGP;
            Attempts = settings.GatheringAttempts;
            BaseYield = settings.BaseYieldPerAttempt;
            Chance = settings.GatheringChance;
            BoonChance = settings.BoonChance;
            BountifulBonus = settings.BountifulBonusYield;
            IsMiner = settings.IsMiner;
        }

        public void SaveSettings(IMainSettings settings)
        {
            settings.CharacterLevel = CharacterLevel;
            settings.AvailableGP = AvailableGP;
            settings.GatheringAttempts = Attempts;
            settings.BaseYieldPerAttempt = BaseYield;
            settings.GatheringChance = Chance;
            settings.BoonChance = BoonChance;
            settings.BountifulBonusYield = BountifulBonus;
            settings.IsMiner = IsMiner;
        }

        private void GetBestRotations()
        {
            var initialContext = new GatheringContext
            {
                Attempts = Attempts,
                AvailableGP = AvailableGP,
                BaseAmount = BaseYield,
                Boon = BoonChance,
                BoonBonus = 1,
                BountifulBonus = BountifulBonus,
                Chance = Chance,
                CharacterLevel = CharacterLevel
            };
            var rotations = _rotationGenerator.GetRotations(initialContext);
            var baseOutcome = _calculator.CalculateOutcome(rotations[0]);
            var rotationOutcomes = new Dictionary<IRotation, GatheringOutcome>();
            rotations.ForEach(r =>
            {
                var outcome = _calculator.CalculateOutcome(r, baseOutcome);
                rotationOutcomes[r] = outcome;
            });
            var highestYield = rotationOutcomes.MaxBy((KeyValuePair<IRotation, GatheringOutcome> kv) => kv.Value, new GatheringYieldComparer());
            var gpEfficient = rotationOutcomes.MaxBy((KeyValuePair<IRotation, GatheringOutcome> kv) => kv.Value, new GatheringEfficiencyComparer());
            PopulateActions(HighestRotation, highestYield.Key);
            HighestYield = highestYield.Value.Yield;
            HighestGP = highestYield.Value.UsedGP;
            HighestYieldPer100GP = highestYield.Value.AddYieldPerGP * 100;
            PopulateActions(EfficientRotation, gpEfficient.Key);
            EfficientYield = gpEfficient.Value.Yield;
            EfficientGP = gpEfficient.Value.UsedGP;
            EfficientYieldPer100GP = gpEfficient.Value.AddYieldPerGP * 100;
            BaseTotalYield = baseOutcome.Yield;
        }

        private void PopulateActions(ObservableCollection<ActionVm> actions, IRotation rotation)
        {
            actions.Clear();

            foreach (var action in rotation.Actions.GroupBy(a => a).OrderBy(a => a.Key.Level))
            {
                var actionVm = new ActionVm(action.Key) { UsedTimes = action.Count(), IsMiner = IsMiner };
                actions.Add(actionVm);
            }
        }
    }
}
