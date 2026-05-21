namespace NetCoreTemplate.Services;

/// <summary>
/// An interface to be implemented when creating a class that inherently creates a route or a route group.
/// </summary>
public interface IRoute
{
    /// <summary>
    /// A method passing the router app instance allowing you to modify the base application from the class itself.
    /// </summary>
    /// <param name="app">The router app instance.</param>
    void MapRoutes(IEndpointRouteBuilder app);
}