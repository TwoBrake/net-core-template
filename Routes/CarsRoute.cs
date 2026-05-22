using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Data;
using NetCoreTemplate.Models;
using NetCoreTemplate.Services;

namespace NetCoreTemplate.Routes;

/// <summary>
/// Routes allowing the retrieval and mutability of cars stored in the code's memory.
/// </summary>
public class CarsRoute : IRoute
{
    public void MapRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cars");

        group.MapGet("/", GetCars);
        group.MapGet("/{id:int}", GetCar);

        group.MapPost("/", CreateCar);
    }

    private static async Task<IResult> GetCars(DatabaseContext db)
    {
        var cars = await db.Cars.ToArrayAsync();
        return Results.Ok(cars);
    }

    private static async Task<IResult> GetCar(int id, DatabaseContext db)
    {
        var car = await db.Cars.FindAsync(id);
        return car is not null ? Results.Ok(new Car(car).FullName) : Results.NotFound();
    }

    private static async Task<IResult> CreateCar([FromBody] CarModel car, DatabaseContext db)
    {
        await db.Cars.AddAsync(car);
        await db.SaveChangesAsync();

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