using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCraftGuild.Stats {

    public interface AttributeCombinationRule {

        /// <summary>
        /// Combine two attributes into a new Attribute.
        /// </summary>
        /// <param name="baseAttribute">Base attribute.</param>
        /// <param name="combiningAttribute">Attribute being combined with <paramref name="baseAttribute" />.</param>
        /// <returns>New combined attribute.</returns>
        Attribute CombineAttributes (Attribute baseAttribute, Attribute combiningAttribute);

    }
}
