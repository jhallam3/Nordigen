using RestSharp;

namespace Nordigen.Utilities
{
    internal class RequestParameter
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public ParameterType Type { get; set; }
    }
}