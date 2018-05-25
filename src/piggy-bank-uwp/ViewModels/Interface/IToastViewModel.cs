namespace piggy_bank_uwp.ViewModels.Interface
{
    public interface IToastViewModel
    {
        void ShowToast();

        void SaveLastTimeShow();

        bool CanShowToast { get; }
    }
}
