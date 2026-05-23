using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Contracts;
using NetCoreTemplate.Data;
using NetCoreTemplate.Models;
using NetCoreTemplate.Services;

namespace NetCoreTemplate.Routes;

/// <summary>
/// Routes allowing the retrieval and mutability of cars stored in the code's memory.
/// </summary>
public class CarsRoute : IRoute
{
    /// <summary>
    /// Register the routes to the app.
    /// </summary>
    public void MapRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cars");

        group.MapGet("/", GetCars);
        group.MapGet("/{id:int}", GetCar);

        group.MapPost("/", AddCar);

        group.MapPatch("/{id:int}", UpdateCar);

        group.MapDelete("/{id:int}", DeleteCar);
    }

    /// <summary>
    /// Retrieves all the cars in the database.
    /// </summary>
    private static async Task<IResult> GetCars(DatabaseContext db)
    {
        var cars = await db.Cars.ToArrayAsync();
        return Results.Ok(cars);
    }

    /// <summary>
    /// Retrieves a single car by its ID from the database.
    /// </summary>
    private static async Task<IResult> GetCar(int id, DatabaseContext db)
    {
        var car = await db.Cars.FindAsync(id);
        return car is not null ? Results.Ok(new Car(car).FullName) : Results.NotFound();
    }

    /// <summary>
    /// Adds a new car to the database.
    /// </summary>
    private static async Task<IResult> AddCar([FromBody] CarCreateRequest car, DatabaseContext db)
    {
        await db.Cars.AddAsync(ConvertToCar(car));
        await db.SaveChangesAsync();

        return Results.Ok();
    }

    /// <summary>
    /// Updates an existing car from the database.
    /// </summary>
    private static async Task<IResult> UpdateCar(int id, [FromBody] CarUpdateRequest dto, DatabaseContext db)
    {
        var car = await db.Cars.FindAsync(id);
        if (car is null)
        {
            return Results.NotFound();
        }

        dto.Adapt(car); // Replace entry's values with any values from the body that are no null.

        await db.SaveChangesAsync();
        return Results.Ok();
    }

    /// <summary>
    /// Deletes an existing car from the database.
    /// </summary>
    private static async Task<IResult> DeleteCar(int id, DatabaseContext db)
    {
        var car = await db.Cars.FindAsync(id);
        if (car is null)
        {
            return Results.NotFound();
        }

        db.Remove(car);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    /// <summary>
    /// Converts the body of a car create request to a car model to be directly sent to the database.
    /// </summary>
    /// <param name="car">The car create request.</param>
    /// <returns>The car model.</returns>
    private static CarModel ConvertToCar(CarCreateRequest car)
    {
        return new CarModel { Brand = car.Brand, Model = car.Model };
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