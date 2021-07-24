using System.Collections.Generic;

namespace Nordigen.DataTypes.Accounts
{
    public class AccountList
    {
        public string id { get; set; }
        public string status { get; set; }
        public List<string> agreements { get; set; }
        public List<string> accounts { get; set; }
        public string reference { get; set; }
        public string enduser_id { get; set; }
    }
}