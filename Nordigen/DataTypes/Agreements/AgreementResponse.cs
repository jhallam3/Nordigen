using System;

namespace Nordigen.DataTypes.Agreements
{
    public class AgreementResponse
    {
        public string id { get; set; }
        public DateTime created { get; set; }
        public object accepted { get; set; }
        public int max_historical_days { get; set; }
        public int access_valid_for_days { get; set; }
        public string enduser_id { get; set; }
        public string aspsp_id { get; set; }
    }
}