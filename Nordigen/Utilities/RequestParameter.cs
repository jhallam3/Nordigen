using RestSharp;

namespace Nordigen.Utilities
{
    public class RequestParameter
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public ParameterType Type { get; set; }
    }
}