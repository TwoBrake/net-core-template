using Microsoft.AspNetCore.Mvc;
using NetCoreTemplate.Models;
using NetCoreTemplate.Services;

namespace NetCoreTemplate.Routes;

/// <summary>
/// Routes allowing the retrieval and mutability of cars stored in the code's memory.
/// </summary>
public class CarsRoute : IRoute
{
    /// <summary>
    /// The cars stored in the application's memory.
    /// </summary>
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
        Cars.Add(new Car(car));
        return Results.Ok();
    }

    /// <summary>
    /// A record that adds useful data to a car's data.
    /// </summary>
    /// <param name="Data">The data set for the car.</param>
    private record Car(CarModel Data)
    {
        /// <summary>
        /// The full name of a car, combining the brand and model into a single string.
        /// </summary>
        public string FullName => $"{Data.Brand} {Data.Model}";
    };
}