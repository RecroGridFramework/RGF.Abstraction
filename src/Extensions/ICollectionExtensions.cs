﻿using System.Collections.Generic;

namespace Recrovit.RecroGridFramework.Abstraction.Extensions;

public static class ICollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> source)
    {
        if (source != null)
        {
            foreach (var element in source)
            {
                self.Add(element);
            }
        }
    }
}