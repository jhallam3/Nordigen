using System.Collections.Generic;

namespace Nordigen.DataTypes
{
    public class RequisitionRequest
    {
        public string redirect { get; set; }
        public string reference { get; set; }
        public string enduser_id { get; set; }
        public List<string> agreements { get; set; }
        public string user_language { get; set; }
    }
}