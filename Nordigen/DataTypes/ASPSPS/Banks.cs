using System.Collections.Generic;

namespace Nordigen.DataTypes.ASPSPS
{
    public class Banks
    {
        public string id { get; set; }
        public string name { get; set; }
        public string bic { get; set; }
        public string transaction_total_days { get; set; }
        public List<string> countries { get; set; }
    }
}