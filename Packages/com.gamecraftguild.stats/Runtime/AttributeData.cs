using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCraftGuild.Stats {

    /// <summary>
    /// Container for multiple attributes.
    /// </summary>
    public class AttributeData {

        /// <summary>
        /// Combine attribute data.
        /// </summary>
        /// <param name="baseData">Base data.</param>
        /// <param name="combiningData">Data to combine.</param>
        /// <returns>Combined data.</returns>
        public static AttributeData CombineAttributeData (AttributeData baseData, AttributeData combiningData) {
            if (baseData == null || combiningData == null) return null;

            AttributeData combinedData = new AttributeData();
            List<string> attributesToCombine = baseData.attributes.Keys.Union(combinedData.attributes.Keys).ToList();

            foreach (string attributeName in attributesToCombine) {
                combinedData.AddAttribute(combiningData.attributes[attributeName].CombinationRule.CombineAttributes(baseData.attributes[attributeName], combiningData.attributes[attributeName]));
            }

            return combinedData;
        }

        /// <summary>
        /// Combine attribute data.
        /// </summary>
        /// <param name="baseData">Base data.</param>
        /// <param name="combiningData">List of data to combine.</param>
        /// <returns>Combined data.</returns>
        public static AttributeData CombineAttributeData (AttributeData baseData, List<AttributeData> combiningData) {
            if (baseData == null || combiningData == null || combiningData.Count == 0) return null;

            AttributeData combinedData = CombineAttributeData(baseData, combiningData[0]);

            for (int i = 0; i < combiningData.Count; i++) {
                combinedData = CombineAttributeData(combinedData, combiningData[i]);
            }

            return combinedData;
        }

        /// <summary>
        /// Dictionary of attributes where the key is the attribute name.
        /// </summary>
        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>();

        /// <summary>
        /// Add an attribute. Does not add if the attribute is already present.
        /// </summary>
        /// <param name="attributeToAdd">Attribute to add.</param>
        /// <returns>True if the attribute was added, false if <paramref name="attributeToAdd" /> was null or already present.</returns>
        public bool AddAttribute (Attribute attributeToAdd) {
            if (attributeToAdd == null) return false;
            if (attributes.ContainsKey(attributeToAdd.Name)) return false;

            attributes.Add(attributeToAdd.Name, attributeToAdd);
            return true;
        }

        /// <summary>
        /// Combine an attribute as long as the attribute is already present.
        /// </summary>
        /// <param name="attributeToCombine">Attribute to combine.</param>
        /// <returns>True if the attribute was combined, false if <paramref name="attributeToCombine" /> was null or not present.</returns>
        public bool CombineAttribute (Attribute attributeToCombine) {
            if (attributeToCombine == null) return false;
            if (!attributes.ContainsKey(attributeToCombine.Name)) return false;

            attributes[attributeToCombine.Name] = attributeToCombine.CombinationRule.CombineAttributes(attributes[attributeToCombine.Name], attributeToCombine);
            return true;
        }

        /// <summary>
        /// Remove an attribute.
        /// </summary>
        /// <param name="attributeName">Attribute to remove.</param>
        /// <returns>True if the attribute was removed, false otherwise.</returns>
        public bool RemoveAttribute (string attributeName) {
            if (attributeName == null) return false;

            return attributes.Remove(attributeName);
        }

        /// <summary>
        /// Get an attribute.
        /// </summary>
        /// <param name="attributeName">Attribute to get.</param>
        /// <returns>The attribute with <paramref name="attributeName" /> or null if it is null or not present.</returns>
        public Attribute GetAttribute (string attributeName) {
            if (attributeName == null || !attributes.ContainsKey(attributeName)) return null;

            return attributes[attributeName];
        }
    }
}
