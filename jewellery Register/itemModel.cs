using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jewellery_Register
{
    public class  Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string total_weight { get; set; }

        public string ratti { get; set; }

        public string pure_gold { get; set; }

        public string gold_rate { get; set; }
        public string labor { get; set; }
        

        public string total { get; set; }
    }
    public class DisplayItem {
        public string Name { get; set; }

        public string PureGold { get; set; }
        public string Ratti { get; set; }

        public string Gold_Weight { get; set; }
        

    }
    public class Cash
    {
        public string Name { get; set; }

        public string Labor { get; set; }
        public string Total { get; set; }


    }
}
