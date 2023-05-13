using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCraftGuild.Stats {
    public interface AttributeComputation {

        /// <summary>
        /// Perform a computation using two sets of AttributeData.
        /// </summary>
        /// <param name="data1">1st set of attribute data.</param>
        /// <param name="data2">2nd set of attribute data.</param>
        /// <returns>Resulting value.</returns>
        float PerformComputation (AttributeData data1, AttributeData data2);

    }
}
