using test_backend.Services;

namespace test_backend.Routes;

public class CarsRoute : IRoute
{
    private static readonly Car[] Cars =
    [
        new Car("Dodge", "Ram"),
        new Car("Jeep", "Patriot")
    ];

    public void MapRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cars");

        group.MapGet("/", GetCars);
        group.MapGet("/{id:int}", GetCar);
    }

    private static IResult GetCars()
    {
        return Results.Ok(Cars);
    }

    private static IResult GetCar(int id)
    {
        var car = Cars.ElementAtOrDefault(id - 1);
        return car is not null ? Results.Ok(car.FullName) : Results.NotFound();
    }

    private record Car(string Brand, string Model)
    {
        public string FullName => $"{Brand} {Model}";
    };
}