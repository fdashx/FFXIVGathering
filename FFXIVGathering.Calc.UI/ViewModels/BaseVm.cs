using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGathering.Calc.UI.ViewModels
{
    public class BaseVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T oldVal, T newVal, string propertyName)
        {
            if (oldVal == null && newVal == null)
                return false;

            if (oldVal != null && oldVal.Equals(newVal))
                return false;

            oldVal = newVal;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
