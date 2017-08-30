using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace piggy_bank_uwp.Utilities
{
    public static class ColorUtility
    {
        public static Color GetColorFromHexString(string hexString)
        {
            return new Color();
        } 

        public static string GetHexStringFromColor(Color color)
        {
            StringBuilder hexColor = new StringBuilder("#");

            hexColor.Append(color.R.ToString());
            hexColor.Append(color.G.ToString());
            hexColor.Append(color.B.ToString());

            return hexColor.ToString();
        }
    }
}
