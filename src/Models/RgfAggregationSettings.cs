using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Recrovit.RecroGridFramework.Abstraction.Models;

public class RgfAggregationSettings
{
    public List<RgfAggregationColumn> Columns { get; set; } = new();

    public List<int> Groups { get; set; } = new ();

    public List<int> SubGroup { get; set; } = new();
}

public class RgfAggregationColumn
{
    public static readonly string[] AllowedAggregates = { "Sum", "Avg", "Min", "Max", "Count" };

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Aggregate { get; set; }

    public int PropertyId { get; set; } = 0;
}