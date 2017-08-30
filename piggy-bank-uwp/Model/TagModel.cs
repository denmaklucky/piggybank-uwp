using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace piggy_bank_uwp.Model
{
    public sealed class TagModel
    {
        public string HexColor { get; set; }

        public Color Color { get; set; }

        public string Title { get; set; }
    }
}
