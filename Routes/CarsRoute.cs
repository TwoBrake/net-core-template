using Microsoft.AspNetCore.Mvc;
using NetCoreTemplate.Models;
using NetCoreTemplate.Services;

namespace NetCoreTemplate.Routes;

public class CarsRoute : IRoute
{
    private static readonly HashSet<Car> Cars = [];

    public void MapRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cars");

        group.MapGet("/", GetCars);
        group.MapGet("/{id:int}", GetCar);

        group.MapPost("/", CreateCar);
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

    private static IResult CreateCar([FromBody] CarModel car)
    {
        Cars.Add(new Car(car.Brand, car.Model));
        return Results.Ok();
    }

    private record Car(string Brand, string Model)
    {
        public string FullName => $"{Brand} {Model}";
    };
}