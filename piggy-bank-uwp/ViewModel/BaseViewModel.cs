using System;
using System.ComponentModel;
using System.Reflection;

namespace piggy_bank_uwp.ViewModel
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		internal virtual void RaisePropertyChanged(string propertyName)
		{
			if (String.IsNullOrEmpty(propertyName))
				return;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		internal virtual void RaisePropertyChanged(object obj)
		{
			if (obj == null)
				return;

			var properties = obj.GetType().GetRuntimeProperties();

			foreach (var property in properties)
			{
				RaisePropertyChanged(property.Name);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
