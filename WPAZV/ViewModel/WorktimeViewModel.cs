using System.ComponentModel;
using WPAZV.Model;

namespace WPAZV.ViewModel
{
    public class WorktimeViewModel: Worktime, INotifyPropertyChanged
    {
        #pragma warning disable CS8612, CS8618 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS8612, CS8618 // Nullability of reference types in type doesn't match implicitly implemented member.

        public void NotifyPropertyChanged(string propertyName){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}