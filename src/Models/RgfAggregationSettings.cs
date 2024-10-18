using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Recrovit.RecroGridFramework.Abstraction.Models;

public class RgfAggregationSettings
{
    public List<RgfAggregationColumn> Columns { get; set; } = new List<RgfAggregationColumn>();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Take { get; set; }
}

public class RgfAggregationColumn
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Aggregate { get; set; }

    public int PropertyId { get; set; } = 0;
}