﻿namespace CarDealer.DTOs.Import;

public class ImportCarDto
{
    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public long TraveledDistance { get; set; }

    public IEnumerable<int> PartsId { get; set; } = new HashSet<int>();
}