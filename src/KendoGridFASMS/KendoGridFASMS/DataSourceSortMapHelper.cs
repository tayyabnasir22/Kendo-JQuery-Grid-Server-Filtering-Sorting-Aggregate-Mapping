using KendoGridParameterParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridParameterParser
{
    public class DataSourceSortMapHelper
    {
        /// <summary>
        /// Build Expression for Order By clause
        /// </summary>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static string SortExpressionBuilder(List<Sort> sorts)
        {
            string expression = null;
            int count = 0;

            sorts.ForEach((sort) =>
            {
                expression = count == 0 ? (sort.Field + " " + sort.Dir) : expression + ", " + sort.Field + " " + sort.Dir;
                count++;
            });

            return expression;
        }
    }
}
