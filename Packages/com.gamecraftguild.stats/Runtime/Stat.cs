using System;
using System.Collections.Generic;

/// <summary>
/// Object representing a stat. Contains a name, a value, a priority, a HashSet of string tags, and a bool for if the stat is multiplicative.
/// </summary>
/// <remarks>
/// The type for value (and any associated functions) should be changed from "float" to "INumerics" once Unity supports .NET 7 and generic math. This should be possible without any major refactoring of Stat or StatBlock, but I'm not 100% confident.
/// </remarks>
/// <typeparam name="T">Value type of the stat.</typeparam>
public class Stat {

    /// <summary>
    /// Name for the stat. This is automatically added to the tags for the stat and cannot be removed.
    /// </summary>
    private string name;

    /// <summary>
    /// Value for the stat.
    /// </summary>
    private float value;

    /// <summary>
    /// Order for applying this stat relative to other stats when totalling a list of stats. Lower stats are applied first.
    /// </summary>
    private float priority;

    /// <summary>
    /// Tags associated with the stat. Will always contain at least the stat's name.
    /// </summary>
    private HashSet<string> tags = new HashSet<string>();

    /// <summary>
    /// If the stat is multiplicative; if false, the stat is additive.
    /// </summary>
    private bool multiplicative = false;

    /// <summary>
    /// List of actions to be called when the value changes.
    /// </summary>
    private List<Action<float>> valueObserverFunctions = new List<Action<float>>();

    /// <summary>
    /// List of actions to be called when the tags change.
    /// </summary>
    private List<Action<HashSet<string>>> tagObserverFunctions = new List<Action<HashSet<string>>>();

    /// <summary>
    /// Create a stat.
    /// </summary>
    /// <param name="statName">Name for the stat.</param>
    /// <param name="initialValue">Initial value for the stat.</param>
    /// <param name="priority">Priority for applying the stat when totalling multiple stats. Lower stats are applied first.</param>
    /// <param name="initialTags">Initial tags for the stat.</param>
    /// <param name="multiplicative">If the stat is multiplicative; if false, the stat is additive.</param>
    public Stat (string statName, float initialValue, float priority, HashSet<string> initialTags, bool multiplicative = false) {
        name = statName;
        tags.Add(name);
        value = initialValue;
        this.priority = priority;
        tags.UnionWith(initialTags);
        this.multiplicative = multiplicative;
    }

    /// <summary>
    /// Get the name of the stat.
    /// </summary>
    /// <returns>The name of the stat.</returns>
    public string GetName () {
        return name;
    }

    /// <summary>
    /// Get the value of the stat.
    /// </summary>
    /// <returns>The value of the stat.</returns>
    public float GetValue () {
        return value;
    }

    /// <summary>
    /// Set the value of the stat.
    /// </summary>
    /// <param name="newValue">New value for the stat.</param>
    public void SetValue (float newValue) {
        value = newValue;
        valueObserverFunctions.ForEach(observer => observer(value));
    }

    /// <summary>
    /// Get the priority for applying the stat when totalling multiple stats. Lower stats are applied first.
    /// </summary>
    /// <returns>The priority for applying the stat when totalling multiple stats. Lower stats are applied first.</returns>
    public float GetPriority () {
        return priority;
    }

    /// <summary>
    /// Get the tags for the stat.
    /// </summary>
    /// <returns>Tags for the stat.</returns>
    public HashSet<string> GetTags () {
        return tags;
    }

    /// <summary>
    /// Add tags to the stat.
    /// </summary>
    /// <param name="tagsToAdd">Tags to add.</param>
    public void AddTags (HashSet<string> tagsToAdd) {
        tags.UnionWith(tagsToAdd);
        tagObserverFunctions.ForEach(observer => observer(tags));
    }

    /// <summary>
    /// Remove tags from the stat. The stat's name cannot be removed.
    /// </summary>
    /// <param name="tagsToRemove">Tags to remove.</param>
    public void RemoveTags (HashSet<string> tagsToRemove) {
        tagsToRemove.Remove(name);
        tags.ExceptWith(tagsToRemove);
        tagObserverFunctions.ForEach(observer => observer(tags));
    }

    /// <summary>
    /// Check if the stat is a multiplicative; if false, the stat is additive.
    /// </summary>
    /// <returns>If the stat is multiplicative.</returns>
    public bool IsMultiplicative () {
        return multiplicative;
    }

    /// <summary>
    /// Add an observer function to the stat's value.
    /// </summary>
    /// <param name="valueObserverFunctionToAdd">Observer function to be called when the value changes.</param>
    public void AddValueObserverFunction (Action<float> valueObserverFunctionToAdd) {
        valueObserverFunctions.Add(valueObserverFunctionToAdd);
    }

    /// <summary>
    /// Remove an observer function from the stat's value.
    /// </summary>
    /// <param name="valueObserverFunctionToRemove">Observer function to be removed.</param>
    /// <returns>If <paramref name="valueObserverFunctionToRemove" /> was successfully removed.</returns>
    public bool RemoveValueObserverFunction (Action<float> valueObserverFunctionToRemove) {
        return valueObserverFunctions.Remove(valueObserverFunctionToRemove);
    }

    /// <summary>
    /// Add an observer function to the stat's tags.
    /// </summary>
    /// <param name="tagObserverFunctionToAdd">Observer function to be called when the tags change.</param>
    public void AddTagObserverFunction (Action<HashSet<string>> tagObserverFunctionToAdd) {
        tagObserverFunctions.Add(tagObserverFunctionToAdd);
    }

    /// <summary>
    /// Remove an observer function from the stat's tags.
    /// </summary>
    /// <param name="tagObserverFunctionToRemove">Observer function to be removed.</param>
    /// <returns>If <paramref name="tagObserverFunctionToRemove" /> was successfully removed.</returns>
    public bool RemoveTagObserverFunction (Action<HashSet<string>> tagObserverFunctionToRemove) {
        return tagObserverFunctions.Remove(tagObserverFunctionToRemove);
    }

}
