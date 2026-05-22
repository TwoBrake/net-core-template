using System.ComponentModel.DataAnnotations;

namespace NetCoreTemplate.Models;

/// <summary>
/// The model for what is required to construct a car data set.
/// </summary>
public class CarModel
{
    [Key] public int Id { get; init; }

    /// <summary>
    /// The brand of the car.
    /// </summary>
    [MaxLength(100)]
    public required string Brand { get; init; }

    /// <summary>
    /// The model of the car.
    /// </summary>
    [MaxLength(100)]
    public required string Model { get; init; }
}