using System;
using System.ComponentModel;
using System.Reflection;

namespace piggy_bank_uwp.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        internal virtual void RaisePropertyChanged(string propertyName)
        {
            App.RunUIAsync(() =>
            {
                if (String.IsNullOrEmpty(propertyName))
                    return;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        internal virtual void RaisePropertiesChanged()
        {
            var properties = GetType().GetRuntimeProperties();

            foreach (var property in properties)
            {
                RaisePropertyChanged(property.Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
