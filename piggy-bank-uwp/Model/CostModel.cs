using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Model
{
    public sealed class CostModel
    {
        public DateTime Date { get; set; }

        //Затрата
        public ulong Cost { get; set; }

        public string Comment { get; set; }

        public TagModel Tag { get; set; }
    }
}
