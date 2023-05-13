using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCraftGuild.Stats {

    /// <summary>
    /// Individual attribute.
    /// </summary>
    public class Attribute {

        /// <summary>
        /// Name of the attribute.
        /// </summary>
        private string name;

        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string Name {
            get {
                return name;
            }
        }

        /// <summary>
        /// Value of the attribute.
        /// </summary>
        private float value;

        /// <summary>
        /// Value of the attribute.
        /// </summary>
        public float Value {
            get {
                return value;
            }
        }

        /// <summary>
        /// Rule for combining this attribute.
        /// </summary>
        private AttributeCombinationRule combinationRule;

        /// <summary>
        /// Rule for combining this attribute.
        /// </summary>
        public AttributeCombinationRule CombinationRule {
            get {
                return combinationRule;
            }
        }

        /// <summary>
        /// Attribute constructor.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        /// <param name="combinationRule">Rule for combining this attribute.</param>
        public Attribute (string name, float value, AttributeCombinationRule combinationRule) {
            this.name = name;
            this.value = value;
            this.combinationRule = combinationRule;
        }

    }
}
