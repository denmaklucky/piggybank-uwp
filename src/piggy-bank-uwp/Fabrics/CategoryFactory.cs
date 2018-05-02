using piggy_bank_uwp.Models;
using piggy_bank_uwp.Utilities;
using System.Collections.Generic;

namespace piggy_bank_uwp.Fabrics
{
    public static class CategoryFactory
    {
        public static IEnumerable<CategoryModel> GetCategories()
        {
            CategoryModel category = new CategoryModel { Title = "Foods", HexColor = "#ff107c10", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Extra costs", HexColor = "#ffe81123", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Home", HexColor = "#ffffb900", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Work", HexColor = "#ff0078d7", Id = SystemUtility.GetGuid() };
            yield return category;
        }
    }
}
