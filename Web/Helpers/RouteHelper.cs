namespace MediaItemsServer.Helpers
{
    public static class RouteHelper
    {
        public static string ConvertToRoute(string routeValue)
        {
            return $"/{routeValue}";
        }
    }
}
