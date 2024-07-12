﻿using Recrovit.RecroGridFramework.Abstraction.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Recrovit.RecroGridFramework.Abstraction.Contracts.API;

public class RgfGridRequest : RgfSessionParams
{
    public RgfGridRequest() { }

    public RgfGridRequest(RgfSessionParams param, string entityName = null, RgfListParam listParam = null) : base(param)
    {
        EntityName = entityName;
        ListParam = listParam;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string EntityName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Skeleton { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfListParam ListParam { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfDynamicDictionary Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfEntityKey EntityKey { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[] UserColumns { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfSelectParam SelectParam { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfGridSettings GridSettings { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RgfPredefinedFilter PredefinedFilter { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object> CustomParams { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string FunctionName { get; set; }
}
