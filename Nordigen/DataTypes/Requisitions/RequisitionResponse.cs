using System.Collections.Generic;

namespace Nordigen.DataTypes.Requisitions
{
    public class RequisitionResponse
    {
        public string id { get; set; }
        public string redirect { get; set; }
        public string status { get; set; }
        public List<string> agreements { get; set; }
        public List<object> accounts { get; set; }
        public string reference { get; set; }
        public string enduser_id { get; set; }
        public string user_language { get; set; }
    }
}