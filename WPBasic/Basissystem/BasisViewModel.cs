using System.ComponentModel;
using WPBasic.Interface;

namespace WPBasic;

public abstract class BasisViewModel : IViewModel
{
    #pragma warning disable CS8612, CS8618 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangedEventHandler? PropertyChanged;
        #pragma warning restore CS8612, CS8618 // Nullability of reference types in type doesn't match implicitly implemented member.

        public void NotifyPropertyChanged(string propertyName){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
}
public abstract class BasisViewModel<T> : BasisViewModel, IViewModel<T>{
    public T Value { get; set; } = default;
    public List<T> Values { get; set; } = default;
}
