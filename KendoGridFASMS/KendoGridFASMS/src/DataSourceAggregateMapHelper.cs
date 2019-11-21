using KendoGridParameterParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridParameterParser
{
    public class DataSourceAggregateMapHelper
    {
        /// <summary>
        /// Build expression for Group By and Select statement for aggregate functions
        /// </summary>
        /// <param name="aggregates"></param>
        /// <returns></returns>
        public static AggregateResult AggregateExpressionBuilder(List<Aggregate> aggregates)
        {
            string expression = null, fields = null;
            int count = 0;

            aggregates.ForEach((aggregate) =>
            {
                expression = count == 0 ? (aggregate.aggregate + "(" + aggregate.Field + ") ") : expression + ", " + (aggregate.aggregate + "(" + aggregate.Field + ") ");
                fields = count == 0 ? (aggregate.Field) : fields + ", " + (aggregate.Field);

                count++;
            });

            return new AggregateResult
            {
                AggregateFields = fields,
                AggregateFunction = expression
            };
        }
    }
}
