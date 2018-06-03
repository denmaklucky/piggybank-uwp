using piggy_bank_uwp.Models;
using piggy_bank_uwp.Utilities;
using System.Collections.Generic;

namespace piggy_bank_uwp.Fabrics
{
    public static class CategoryFactory
    {
        public static IEnumerable<CategoryModel> GetCategories()
        {
            CategoryModel category = new CategoryModel { Title = "Foods", HexColor = "#FF107C10", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Extra costs", HexColor = "#FFE81123", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Home", HexColor = "#FFFFB900", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Work", HexColor = "#FF0078D7", Id = SystemUtility.GetGuid() };
            yield return category;
        }
    }
}
