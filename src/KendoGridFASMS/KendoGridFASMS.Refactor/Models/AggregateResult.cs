using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor.Models
{

    /// <summary>
    /// Container for Aggregate fields and aggregate functions expression
    /// </summary>
    public class AggregateResult
    {
        /// <summary>
        /// Will return all the field to be aggregated (to be used in Group By clause)
        /// </summary>
        public string AggregateFields { get; set; }

        /// <summary>
        /// The aggregate function applied to the specified field
        /// </summary>
        public string AggregateFunction { get; set; }
    }
}
