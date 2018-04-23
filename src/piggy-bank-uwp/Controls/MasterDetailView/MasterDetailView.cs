using piggy_bank_uwp.View.Default;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Controls.MasterDetailView
{
    public class MasterDetailView : ContentControl
    {
        private const string NARROW_STATE = "NarrowState";

        private ContentPresenter _masterPresenter;
        private Frame _detailPresenter;
        private VisualStateGroup _stateGroup;
        private DefaultPage _defaultPage;

        public MasterDetailView()
        {
            DefaultStyleKey = typeof(MasterDetailView);
            _defaultPage = new DefaultPage();

            SizeChanged += OnSizeChanged;
        }

        public void Navigate(Type pageType, object parameter = null)
        {
            _detailPresenter.Navigate(pageType, parameter);
            UpdateView();
        }

        public void Subscribe()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        public void Unsubscribe()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
        }

        protected override void OnApplyTemplate()
        {
            _masterPresenter = (ContentPresenter)GetTemplateChild("MasterPresenter");
            _detailPresenter = (Frame)GetTemplateChild("DetailPresenter");
            _stateGroup = (VisualStateGroup)GetTemplateChild("AdaptiveStates");
            //Set a default page
            _detailPresenter.Navigate(typeof(DefaultPage));

            _stateGroup.CurrentStateChanged += OnCurrentStateChanged;

            UpdateView();
        }

        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateView();
            //Get a current state
            StateChanged?.Invoke(this, CurrentState);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateView();
            //Get a current state
            StateChanged?.Invoke(this, CurrentState);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (CurrentState == MasterDetailState.Wide)
            {
                if (_detailPresenter.CanGoBack)
                {
                    _detailPresenter.GoBack();
                }
                else
                {
                    _detailPresenter.BackStack.Clear();
                    _detailPresenter.Content = _defaultPage;
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
                    _detailPresenter.Content = _defaultPage;
                    UpdateView();
                }
            }

            e.Handled = true;
        }

        private void UpdateView()
        {
            if (CurrentState == MasterDetailState.Wide)
            {
                _masterPresenter.Visibility = Visibility.Visible;
                _detailPresenter.Visibility = Visibility.Visible;
            }

            if (CurrentState == MasterDetailState.Narrow && _detailPresenter.Content is DefaultPage)
            {
                _masterPresenter.Visibility = Visibility.Visible;
                _detailPresenter.Visibility = Visibility.Collapsed;
            }

            if (CurrentState == MasterDetailState.Narrow && !(_detailPresenter.Content is DefaultPage))
            {
                _masterPresenter.Visibility = Visibility.Collapsed;
                _detailPresenter.Visibility = Visibility.Visible;
            }
        }

        public MasterDetailState CurrentState
        {
            get
            {
                return _stateGroup.CurrentState.Name == NARROW_STATE ? MasterDetailState.Narrow :
                    MasterDetailState.Wide;
            }
        }

        public bool CanGoBack
        {
            get
            {
                return !(_detailPresenter.Content is DefaultPage);
            }
        }

        public event EventHandler<MasterDetailState> StateChanged;
    }
}
