using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WPBasic.Interface
{
    public interface IViewModel : INotifyPropertyChanged
    {
        new event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string propertyName);
    }
}