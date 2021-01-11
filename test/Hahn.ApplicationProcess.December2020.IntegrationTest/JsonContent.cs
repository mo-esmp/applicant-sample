using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest
{
    internal sealed class JsonContent : StringContent
    {
        public JsonContent(object obj) : base(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json")
        { }
    }
}