using System.Collections.Generic;
using System;

namespace GameCraftGuild.Stats
{
    /// <summary>
    /// Compute attributes based on other attributes.
    /// </summary>
    public class AttributeComputer {

        public delegate float AttributeComputation(AttributeComputer attributeComputer, AttributeData data);

        /// <summary>
        /// Mapping of names of attributes to computations.
        /// </summary>
        private Dictionary<string, AttributeComputation> computations;

        /// <summary>
        /// Register a computation for the given attribute. There can only be one computation for a given attribute.
        /// </summary>
        /// <param name="computedAttribute">Name of the attribute the computation is for.</param>
        /// <param name="computation">Computation for the attribute.</param>
        /// <returns>True if <paramref name="computation"/> was registerd, false otherwise.</returns>
        public bool RegisterComputation(string computedAttribute, AttributeComputation computation) {
            if (computations.ContainsKey(computedAttribute)) return false;

            computations[computedAttribute] = computation;
            return true;
        }

        /// <summary>
        /// Unregister a computaiton. <paramref name="computation"/> must match the registered computation for <paramref name="computedAttribute"/> for it to be unregistered. 
        /// </summary>
        /// <param name="computedAttribute">Name of the attribute the computation should be unregistered for.</param>
        /// <param name="computation">Computation to be unregistered.</param>
        /// <returns>True if <paramref name="computation"/> was unregistered, false otherwise.</returns>
        public bool UnregisterComputation(string computedAttribute, AttributeComputation computation) {
            if (!computations.ContainsKey(computedAttribute)) return false;
            if (computations[computedAttribute] != computation) return false;

            return computations.Remove(computedAttribute);
        }

        /// <summary>
        /// Compute the <paramref name="computedAttribute"/> using the registered computation and the <paramref name="data"/>.
        /// </summary>
        /// <param name="computedAttribute">Attribute to compute.</param>
        /// <param name="data">Data to use in the computation.</param>
        /// <returns>Value for the <paramref name="computedAttribute"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if there is no registered computation for <paramref name="computedAttribute"/>.</exception>
        public float ComputeAttribute (string computedAttribute, AttributeData data) {
            if (!computations.ContainsKey(computedAttribute)) throw new ArgumentException(String.Format("{0} does not have a computation registered for it.", computedAttribute));
                
            return computations[computedAttribute](this, data);
        }

    }
}
