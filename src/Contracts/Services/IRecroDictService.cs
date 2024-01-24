﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recrovit.RecroGridFramework.Abstraction.Contracts.Services;

public interface IRecroDictService
{
    Task<ConcurrentDictionary<string, string>> GetDictionaryAsync(string scope, string language = null, bool authClient = true);

    Task InitializeAsync(string language = null);

    string GetRgfUiString(string stringId);

    Dictionary<string, string> Languages { get; }

    string DefaultLanguage { get; }

    bool IsInitialized { get; }
}

public static class IRecroDictServiceExtension
{
    public static async Task<string> GetItemAsync(this IRecroDictService recroDict, string scope, string stringId, string language = null, bool authClient = true)
    {
        var dictionary = await recroDict.GetDictionaryAsync(scope, language, authClient);
        return GetItem(dictionary, stringId, $"{scope}.{stringId}");
    }

    public static string GetItem(ConcurrentDictionary<string, string> dictionary, string stringId, string defaultValue = null) => dictionary.TryGetValue(stringId, out var value) ? value : defaultValue;

    public static string GetItem(Dictionary<string, string> dictionary, string stringId, string defaultValue = null) => dictionary.TryGetValue(stringId, out var value) ? value : defaultValue;
}
