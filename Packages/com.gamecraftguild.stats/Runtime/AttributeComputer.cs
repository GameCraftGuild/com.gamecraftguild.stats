using System.Collections.Generic;
using System;

namespace GameCraftGuild.Stats
{
    /// <summary>
    /// Compute attributes based on other attributes.
    /// </summary>
    public class AttributeComputer : IAttributeComputer {

        /// <summary>
        /// Mapping of names of attributes to computations.
        /// </summary>
        private Dictionary<string, IAttributeComputer.AttributeComputation> computations;

        /// <inheritdoc />
        public bool RegisterComputation(string computedAttribute, IAttributeComputer.AttributeComputation computation) {
            return computations.TryAdd(computedAttribute, computation);
        }

        /// <inheritdoc />
        public bool UnregisterComputation(string computedAttribute) {
            return computations.Remove(computedAttribute);
        }

        /// <inheritdoc />
        public float ComputeAttribute(string computedAttribute, AttributeData data) {
            if (!computations.ContainsKey(computedAttribute)) throw new ArgumentException($"{computedAttribute} does not have a computation registered for it.");
                
            return computations[computedAttribute](this, data);
        }

    }
}
