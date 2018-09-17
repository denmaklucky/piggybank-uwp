using Windows.ApplicationModel.Resources;

namespace piggy_bank_uwp
{
    public static class Localize
    {
        public const string Accounts = "Accounts";
        public const string Costs = "Costs";
        public const string Categories = "Categories";
        public const string Diagrama = "Diagrama";
        public const string Synchronization = "Synchronization";
        public const string Donate = "Donate";
        public const string Settings = "Settings";
        public const string HeaderReminderNotifi = "HeaderReminderNotifi";
        public const string DescriptionRemiderNotifi = "DescriptionRemiderNotifi";
        public const string WarringCategoriesContent = "WarringCategoriesContent";
        public const string WarringCostContent = "WarringCostContent";
        public const string WarringCategoryContent = "WarringCategoryContent";
        public const string WarringBalanceCostContent = "WarringBalanceCostContent";
        public const string Ok = "Ok";
        public const string PurchaseStatusOk = "PurchaseStatusOk";
        public const string PurchaseStatusBad = "PurchaseStatusBad";

        public static string GetTranslateByKey(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return string.Empty;

                string translate = ResourceLoader.GetForViewIndependentUse().GetString(key);

                if (string.IsNullOrEmpty(translate))
                {
                    return key;
                }

                return translate;
            }
            catch
            {
                return key;
            }
        }
    }
}
