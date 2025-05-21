using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.Windows.Controls.Map;

namespace Routing
{
    internal class AzureRoutingHelper
    {
        private static HttpClient httpClient = new HttpClient();
        public static string AzureMapsSubscriptionKey { get; set; }

        internal static async Task<RouteInfo> GetRouteDirections(Location start, Location end)
        {
            var requestUrl = $"https://atlas.microsoft.com/route/directions/json?api-version=1.0&query={start}:{end}&instructionsType=text&subscription-key={AzureMapsSubscriptionKey}";

            var response = await httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            var routeData = JsonSerializer.Deserialize<RouteResponse>(jsonContent, options);

            var firstLeg = routeData.Routes[0].Legs[0];
            var routeInfo = new RouteInfo() { Points = firstLeg.Points.Select(p => new Location(p.Latitude, p.Longitude)).ToList(), 
                                              WayPointInfos = routeData.Routes[0].Guidance.Instructions.Select(i => new WayPointInfo() { Message = i.Message, Location = new Location(i.Point.Latitude, i.Point.Longitude) }).ToList() };

            return routeInfo;
        }
    }

    public class RouteInfo
    {
        public List<Location> Points { get; set; }

        public List<WayPointInfo> WayPointInfos { get; set; }
    }

    public class WayPointInfo
    {
        public string Message { get; set; }
        public Location Location { get; set; }
    }

    public class RouteResponse
    {
        public List<Route> Routes { get; set; }
    }

    public class Route
    {
        public List<RouteLeg> Legs { get; set; }

        public Guidance Guidance { get; set; }
    }

    public class Guidance
    {
        public List<Instruction> Instructions { get; set; }
    }

    public class RouteLeg
    {
        public List<RoutePoint> Points { get; set; }
    }

    public class RoutePoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Instruction
    {
        public RoutePoint Point { get; set; }

        public string Message { get; set; }
    }
}
