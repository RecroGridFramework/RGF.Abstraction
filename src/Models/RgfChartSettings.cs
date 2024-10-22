namespace Recrovit.RecroGridFramework.Abstraction.Models;

public enum RgfChartSeriesType
{
    Bar,
    Line,
    Pie,
    Donut
}

public class RgfChartSettings
{
    public RgfAggregationSettings AggregationSettings { get; set; } = new();

    public RgfChartSeriesType SeriesType { get; set; }

    public bool Legend { get; set; }

    public bool Stacked { get; set; }

    public bool Horizontal { get; set; }
}
