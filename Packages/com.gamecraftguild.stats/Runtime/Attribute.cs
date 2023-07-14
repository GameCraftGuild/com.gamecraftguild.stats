using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameCraftGuild.Stats {

    /// <summary>
    /// Individual attribute.
    /// </summary>
    public struct Attribute {

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
        /// Attribute constructor.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        public Attribute (string name, float value) {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// String representation of the attribute.
        /// </summary>
        /// <returns>"Attribute(name: value)"</returns>
        public override string ToString () {
            return String.Format("Attribute({0}: {1})", name, value);
        }

        /// <summary>
        /// Add two attributes with the same name.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>A new attribute with the same name and the values of <paramref name="a"/> and <paramref name="b"/> added.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="a"/> and <paramref name="b"/> have different names.</exception>
        public static Attribute operator +(Attribute a, Attribute b) {
            if (b.name != a.name) throw new ArgumentException(string.Format("{0} has a different name than {1} so they cannot be added.", a, b));

            return new Attribute(a.name, a.value + b.value);
        }

        /// <summary>
        /// Subtract two attributes with the same name.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>A new attribute with the same name and the values of <paramref name="a"/> and <paramref name="b"/> subtracted.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="a"/> and <paramref name="b"/> have different names.</exception>
        public static Attribute operator -(Attribute a, Attribute b) {
            if (b.name != a.name) throw new ArgumentException(string.Format("{0} has a different name than {1} so they cannot be added.", a, b));

            return new Attribute(a.name, a.value - b.value);
        }

        /// <summary>
        /// Multiply two attributes with the same name.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>A new attribute with the same name and the values of <paramref name="a"/> and <paramref name="b"/> multiplied.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="a"/> and <paramref name="b"/> have different names.</exception>
        public static Attribute operator *(Attribute a, Attribute b) {
            if (b.name != a.name) throw new ArgumentException(string.Format("{0} has a different name than {1} so they cannot be added.", a, b));

            return new Attribute(a.name, a.value * b.value);
        }

        /// <summary>
        /// Divide two attributes with the same name.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>A new attribute with the same name and the value of <paramref name="a"/> divided by <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="a"/> and <paramref name="b"/> have different names.</exception>
        public static Attribute operator /(Attribute a, Attribute b) {
            if (b.name != a.name) throw new ArgumentException(string.Format("{0} has a different name than {1} so they cannot be added.", a, b));

            return new Attribute(a.name, a.value / b.value);
        }

        /// <summary>
        /// Check if the attribtues are equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>True if the attributes are equal, false if the attributes are not equal.</returns>
        public static bool operator ==(Attribute a, Attribute b) {
            if (a.name == b.name && a.value == b.value) return true;
            return false;
        }

        /// <summary>
        /// Check if the attribtues are not equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>True if the attributes are not equal and false if the attributes are equal.</returns>
        public static bool operator !=(Attribute a, Attribute b) {
            if (a.name != b.name || a.value != b.value) return true;
            return false;
        }

        /// <summary>
        /// Check if two attribute are equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="obj">Object to compare the attribute to.</param>
        /// <returns>True if the attributes are equal, false otherwise.</returns>
        public override bool Equals(object obj) {
            if (obj is Attribute) return this == (Attribute)obj;
            return false;
        }

        /// <summary>
        /// Get a hash code for the attribute.
        /// </summary>
        /// <returns>Hash code for the attribute.</returns>
        public override int GetHashCode () {
            return (name + value).GetHashCode();
        }

    }
}
