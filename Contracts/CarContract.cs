namespace NetCoreTemplate.Contracts;

/// <summary>
/// The body of a request to create a new car.
/// </summary>
public record CarCreateRequest(string Brand, string Model);

/// <summary>
/// The body of a request to update an existing car.
/// </summary>
public record CarUpdateRequest(string? Brand, string? Model);