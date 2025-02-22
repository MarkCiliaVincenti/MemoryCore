﻿namespace MemoryCore;

public partial class MemoryCoreManager : IMemoryCore
{
    /// <summary>
    /// Get all tags in the cache.
    /// </summary>
    public IEnumerable<string> GetTags()
    {
        return entries.Values
            .Where(x => x.Tags is not null)
            .SelectMany(x => x.Tags)
            .Distinct();
    }

    /// <summary>
    /// Check if a tag exists in the cache.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns>True if the tag exists, false otherwise.</returns>
    public bool ExistsTag(string tag)
    {
        return entries.Values
            .Any(x => x.IsTagged(tag, comparer));
    }

    /// <summary>
    /// Remove all items with a specific tag from the cache.
    /// </summary>
    public void RemoveTag(string tag)
    {
        var keys = entries.Values
            .Where(x => x.Tags.Any(x => x.Equals(tag, comparer)))
            .Select(x => x.Key);

        foreach (var key in keys)
            entries.TryRemove(key, out _);
    }
}
