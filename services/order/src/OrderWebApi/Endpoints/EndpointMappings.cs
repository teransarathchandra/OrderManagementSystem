using System.Reflection;

namespace OrderWebApi.Endpoints
{
    public static class EndpointMappings
    {
        public static void MapEndpoints(WebApplication app)
        {
            var endpointTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.IsSealed && t.IsAbstract && t.Name.EndsWith("Endpoint"));

            foreach (var endpointType in endpointTypes)
            {
                var mapMethod = endpointType.GetMethod("Map");
                if (mapMethod != null)
                {
                    mapMethod.Invoke(null, new[] { app });
                }
            }
        }
    }
}