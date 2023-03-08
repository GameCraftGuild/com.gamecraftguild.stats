using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Aggregation of stats.
/// </summary>
public class StatBlock {

    /// <summary>
    /// Dictionary of all stats in the statblock.
    /// </summary>
    private Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    /// <summary>
    /// Add a stat to the stat block if a stat with the same name is not already present.
    /// </summary>
    /// <param name="statToAdd">Stat to add.</param>
    /// <returns>True if <paramref name="statToAdd" /> was added successfully and false if a stat with the same name was already present or <paramref name="statToAdd" /> was null.</returns>
    public bool AddStat (Stat statToAdd) {
        if (statToAdd == null || stats.ContainsKey(statToAdd.GetName())) return false;

        stats.Add(statToAdd.GetName(), statToAdd);
        return true;
    }

    /// <summary>
    /// Remove a stat from the stat block.
    /// </summary>
    /// <param name="statToRemove">Stat to remove.</param>
    /// <returns>If <paramref name="statToRemove" /> was removed successfully.</returns>
    public bool RemoveStat (Stat statToRemove) {
        return stats.Remove(statToRemove.GetName());
    }

    /// <summary>
    /// Get a stat by name.
    /// </summary>
    /// <param name="name">Name of the stat to get.</param>
    /// <returns>The stat with <paramref name="name" /> or null if the stat block did not contain a stat with <paramref name="name" />.</returns>
    public Stat GetStatByName (string name) {
        if (!stats.ContainsKey(name)) return null;

        return stats[name];
    }

    /// <summary>
    /// Get stats with ANY of the provided tags.
    /// </summary>
    /// <param name="tags">Tags to look for.</param>
    /// <returns>List of stats containing ANY of the tags.</returns>
    public List<Stat> GetStatsWithAnyOfTags (HashSet<string> tags) {
        return stats.Select(kvp => kvp.Value).Where(stat => stat.GetTags().Overlaps(tags)).ToList();
    }

    /// <summary>
    /// Get stats with ALL of the provided tags.
    /// </summary>
    /// <param name="tags">Tags to look for</param>
    /// <returns>List of stats containing ALL of the tags.</returns>
    public List<Stat> GetStatsWithAllOfTags (HashSet<string> tags) {
        return stats.Select(kvp => kvp.Value).Where(stat => stat.GetTags().IsSupersetOf(tags)).ToList();
    }

    /// <summary>
    /// Total stats with ANY of the provided tags.
    /// </summary>
    /// <param name="tags">Tags to look for.</param>
    /// <returns>Total of stats containing ANY of the tags.</returns>
    public float TotalStatsWithAnyOfTags (HashSet<string> tags) {
        return TotalStats(GetStatsWithAnyOfTags(tags));
    }

    /// <summary>
    /// Total stats with ALL of the provided tags.
    /// </summary>
    /// <param name="tags">Tags to look for.</param>
    /// <returns>Total of stats containing ALL of the tags.</returns>
    public float TotalStatsWithAllOfTags (HashSet<string> tags) {
        return TotalStats(GetStatsWithAllOfTags(tags));
    }

    /// <summary>
    /// Total the provided stats. Sorts stats based on priority, from low to high, before totalling them.
    /// </summary>
    /// <param name="statsToTotal">List of stats to total.</param>
    /// <returns>Total of the provided stats.</returns>
    private float TotalStats (List<Stat> statsToTotal) {
        float runningTotal = 0;

        statsToTotal.Sort((a, b) => a.GetPriority().CompareTo(b.GetPriority())); // Sorts the stats by their priority from low to high.

        for (int currentStatIndex = 0; currentStatIndex < statsToTotal.Count; currentStatIndex++) {
            if (statsToTotal[currentStatIndex].IsMultiplicative()) {
                runningTotal *= statsToTotal[currentStatIndex].GetValue();
            } else {
                runningTotal += statsToTotal[currentStatIndex].GetValue();
            }
        }

        return runningTotal;
    }
}
