namespace NetCoreTemplate.Models;

/// <summary>
/// The model for what is required to construct a car data set.
/// </summary>
public class CarModel
{
    /// <summary>
    /// The brand of the car.
    /// </summary>
    public required string Brand { get; set; }
    /// <summary>
    /// The model of the car.
    /// </summary>
    public required string Model { get; set; }
}