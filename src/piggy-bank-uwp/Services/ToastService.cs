using Microsoft.Toolkit.Uwp.Notifications;

namespace piggy_bank_uwp.Services
{
    public static class ToastService
    {
        public static ToastContent GenerateToastContent()
        {
            return new ToastContent
            {
                Scenario = ToastScenario.Reminder,
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Напоменание"
                            },
                            new AdaptiveText
                            {
                                Text = "Пожалуйста синхранизируйте совю учетную запись"
                            }
                        }
                    }
                }
            };
        } 
    }
}
