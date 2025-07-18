﻿using Recrovit.RecroGridFramework.Abstraction.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Recrovit.RecroGridFramework.Abstraction.Models;

public enum PropertyFormType
{
    [EnumMember(Value = "invalid")]
    Invalid = 0,
    [EnumMember(Value = "textbox")]
    TextBox = 1,
    [EnumMember(Value = "textboxmultiline")]
    TextBoxMultiLine = 2,
    [EnumMember(Value = "checkbox")]
    CheckBox = 3,
    [EnumMember(Value = "dropdown")]
    DropDown = 4,
    [EnumMember(Value = "date")]
    Date = 5,
    [EnumMember(Value = "datetime")]
    DateTime = 6,
    [EnumMember(Value = "recrogrid")]
    RecroGrid = 7,
    [EnumMember(Value = "entity")]
    Entity = 8,
    [EnumMember(Value = "statictext")]
    StaticText = 10,
    [EnumMember(Value = "imageindb")]
    ImageInDB = 11,
    [EnumMember(Value = "recrodict")]
    RecroDict = 13,
    [EnumMember(Value = "htmleditor")]
    HtmlEditor = 14,
    [EnumMember(Value = "listbox")]
    ListBox = 15,
    [EnumMember(Value = "custom")]
    Custom = 16,
    //[Obsolete("Use RGO_AggregationRequired instead", true)]
    //[EnumMember(Value = "chartitem")]
    //ChartOnlyData = 17,
}
public enum PropertyListType
{
    [EnumMember(Value = "string")]
    String = 0,
    [EnumMember(Value = "numeric")]
    Numeric = 1,
    [EnumMember(Value = "date")]
    Date = 2,
    [EnumMember(Value = "html")]
    Html = 3,
    [EnumMember(Value = "image")]
    Image = 4,
    [EnumMember(Value = "recrogrid")]
    RecroGrid = 5
}

public enum ClientDataType
{
    Undefined = 0,
    String = 1,
    Integer = 2,
    Decimal = 3,
    Double = 4,
    DateTime = 5,
    Boolean = 7,
}

public static class ClientDataTypeExtension
{
    public static bool IsNumeric(this ClientDataType data)
    {
        switch (data)
        {
            case ClientDataType.Integer:
            case ClientDataType.Decimal:
            case ClientDataType.Double:
                return true;
        }
        return false;
    }
}
public interface IRgfProperty
{
    string Alias { get; set; }

    string ClientName { get; set; }

    string BaseEntityNameVersion { get; set; }

    string BaseEntityPropertyName { get; set; }

    int ColPos { get; set; }

    string ColTitle { get; set; }

    int ColWidth { get; set; }

    bool Editable { get; set; }

    string Ex { get; set; }

    ClientDataType ClientDataType { get; }

    PropertyFormType FormType { get; set; }

    int Id { get; set; }

    bool IsKey { get; set; }

    PropertyListType ListType { get; set; }

    Dictionary<string, object> Options { get; set; }

    bool Orderable { get; set; }

    bool Readable { get; set; }

    int Sort { get; set; }
}

public class RgfIdAliasPair : ICloneable
{
    public RgfIdAliasPair() { }

    public RgfIdAliasPair(int id, string alias)
    {
        Id = id;
        Alias = alias;
    }

    public RgfIdAliasPair(RgfIdAliasPair rgfIdAliasPair)
    {
        if (rgfIdAliasPair != null)
        {
            Id = rgfIdAliasPair.Id;
            Alias = rgfIdAliasPair.Alias;
        }
    }

    public int Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Alias { get; set; }

    public virtual object Clone() => DeepCopy(this);

    public static RgfIdAliasPair DeepCopy(RgfIdAliasPair source) => source == null ? null : new RgfIdAliasPair(source);
}

public class RgfProperty : RgfIdAliasPair, IRgfProperty
{
    public string ClientName { get; set; }

    public string BaseEntityNameVersion { get; set; }

    public string BaseEntityPropertyName { get; set; }

    public string ColTitle { get; set; }

    public PropertyListType ListType { get; set; }

    public int ColPos { get; set; }

    public int ColWidth { get; set; }

    public PropertyFormType FormType { get; set; }

    public int FormTab { get; set; }

    public int FormGroup { get; set; }

    public int FormPos { get; set; }

    public int Sort { get; set; }

    public bool IsKey { get; set; }

    public bool Readable { get; set; }

    public bool Editable { get; set; }

    public bool Orderable { get; set; }

    public string Ex { get; set; }

    public Dictionary<string, object> Options { get; set; }

    [JsonIgnore]
    public int? MaxLength => this.GetMaxLength();

    [JsonIgnore]
    public bool PasswordType => this.IsPasswordType();

    [JsonIgnore]
    public bool Nullable => this.IsNullable();

    [JsonIgnore]
    public bool IsDynamic => this.IsDynamic();

    [JsonIgnore]
    public bool Required => !Nullable;

    [JsonIgnore]
    public ClientDataType ClientDataType
    {
        get
        {
            switch (FormType)
            {
                case PropertyFormType.Invalid:
                case PropertyFormType.RecroGrid:
                case PropertyFormType.Entity:
                case PropertyFormType.Custom:
                    return ClientDataType.Undefined;

                case PropertyFormType.Date:
                case PropertyFormType.DateTime:
                    return ClientDataType.DateTime;

                case PropertyFormType.DropDown:
                case PropertyFormType.ListBox:
                    return ClientDataType.String;//Always a string on the client side

                case PropertyFormType.CheckBox:
                    if (Options?.GetBoolValue("RGO_Nullable") != true)
                    {
                        return ClientDataType.Boolean;
                    }
                    return ClientDataType.String;

                default:
                    if (ListType == PropertyListType.Numeric)
                    {
                        if (!IsKey)
                        {
                            /*TODO: ClientDataType => Integer, Decimal, Double ?
                            if (this.Options.GetBoolValue(?))
                            {
                                return ClientDataType.Decimal;
                            }
                            if (this.Options.GetBoolValue(?))*/
                            {
                                return ClientDataType.Double;
                            }
                        }
                        return ClientDataType.Integer;
                    }
                    return ClientDataType.String;
            }
        }
    }
}

public static class IRgfPropertyExtension
{
    public static int? GetMaxLength(this IRgfProperty property) => property.Options?.TryGetIntValue("RGO_MaxLength");

    public static bool IsPasswordType(this IRgfProperty property) => property.Options?.GetBoolValue("RGO_Password") ?? false;

    public static bool IsNullable(this IRgfProperty property) => property.Options?.GetBoolValue("RGO_Nullable") ?? false;

    public static bool IsDynamic(this IRgfProperty property) => property.Ex?.Contains("D") == true;

    public static int? GetAutoExternalId(this IRgfProperty property)
    {
        var ext = property.Options?.GetStringValue("RGO_AutoExternal");
        if (ext != null && int.TryParse(ext.ToString().Split('/').Last(), out int externalId))
        {
            return externalId;
        }
        return null;
    }
}