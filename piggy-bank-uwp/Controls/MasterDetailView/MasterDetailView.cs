using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Controls.MasterDetailView
{
	public class MasterDetailView : ContentControl
	{
		private const string NARROW_STATE = "NarrowState";
		private const string WIDE_STATE = "WideState";

		private ContentPresenter _masterPresenter;
		private Frame _detailPresenter;
		private VisualStateGroup _stateGroup;

		public MasterDetailView()
		{
			DefaultStyleKey = typeof(MasterDetailView);
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
		}

		public void Navigate(Type pageTyep, object parameter = null)
		{
			_detailPresenter.Navigate(pageTyep, parameter);
			UpdateView();
		}

		protected override void OnApplyTemplate()
		{
			_masterPresenter = (ContentPresenter)GetTemplateChild("MasterPresenter");
			_detailPresenter = (Frame)GetTemplateChild("DetailPresenter");
			_stateGroup = (VisualStateGroup)GetTemplateChild("AdaptiveStates");

			_stateGroup.CurrentStateChanged += OnCurrentStateChanged;

			CurrentState = _stateGroup.CurrentState.Name == NARROW_STATE ?
				MasterDetailState.Narrow : MasterDetailState.Wide;
		}

		private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
		{
			if (e.NewState.Name == NARROW_STATE)
			{
				CurrentState = MasterDetailState.Narrow;
			}

			if (e.NewState.Name == WIDE_STATE)
			{
				CurrentState = MasterDetailState.Wide;
			}

			//Get a current state
			StateChanged?.Invoke(this, CurrentState);
		}

		private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
		}

		private void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{
			if (CurrentState == MasterDetailState.Wide)
			{
				if (_detailPresenter.CanGoBack)
				{
					_detailPresenter.GoBack();
				}
			}
			else
			{
				if (_detailPresenter.BackStack.Count > 1)
				{
					if (_detailPresenter.CanGoBack)
					{
						_detailPresenter.GoBack();
					}
				}
				else
				{
					_detailPresenter.BackStack.Clear();
					_detailPresenter.Content = null;
					UpdateView();
				}
			}
		}

		private void UpdateView()
		{
			if (CurrentState == MasterDetailState.Wide)
			{
				_masterPresenter.Visibility = Visibility.Visible;
				_detailPresenter.Visibility = Visibility.Visible;
			}

			if (CurrentState == MasterDetailState.Narrow && _detailPresenter.Content == null)
			{
				_masterPresenter.Visibility = Visibility.Visible;
				_detailPresenter.Visibility = Visibility.Collapsed;
			}

			if (CurrentState == MasterDetailState.Narrow && _detailPresenter.Content != null)
			{
				_masterPresenter.Visibility = Visibility.Collapsed;
				_detailPresenter.Visibility = Visibility.Visible;
			}
		}

		public MasterDetailState CurrentState { get; private set; }

		public event EventHandler<MasterDetailState> StateChanged;
	}
}
