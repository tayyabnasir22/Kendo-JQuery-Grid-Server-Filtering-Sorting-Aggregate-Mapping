using KendoGridParameterParser;
using KendoGridParameterParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        private AggregateResult ParseAggregateExpression(DataSourceRequest request)
        {
            if (request.Aggregate == null)
            {
                return null;
            }
            else
            {
                var sorter = DataSourceAggregateMapHelper.AggregateExpressionBuilder(request.Aggregate);
                return (sorter == null || sorter.Equals("")) ? null : sorter;
            }
        }

        /// <summary>
        /// Get fields to with aggregate functions
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns>
        /// Output Examples:
        /// count(projectName) , sum(projectAmount) , sum(projectFee)
        /// </returns>
        private string ParseForAggregateSelect(AggregateResult aggregate)
        {
            return aggregate.AggregateFunction;
        }

        /// <summary>
        /// Example of dynamic Group By Clause
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns>
        /// Output Examples:
        /// Group By projectName, projectAmount, projectFee
        /// </returns>
        private string ParseForGroupBy(AggregateResult aggregate)
        {
            if (aggregate == null)
            {
                return "";
            }
            else
            {
                return (aggregate.AggregateFields == null || aggregate.AggregateFields.Equals("")) ? "" : string.Format("Group By {0}", aggregate.AggregateFields);
            }
        }

        /// <summary>
        /// Example of Dynamic Order by Clause
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// Order By projectName desc, projectAmount asc
        /// </returns>
        private string ParseSortExpression(DataSourceRequest request)
        {
            if (request.Sort == null)
            {
                return "";
            }
            else
            {
                var sorter = DataSourceSortMapHelper.SortExpressionBuilder(request.Sort);
                return (sorter == null || sorter.Equals("")) ? "" : string.Format("ORDER BY {0}", sorter);
            }
        }

        /// <summary>
        /// Example for generating Dynamic Where Clause
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// Output Examples:
        /// WHERE (( projectNumber > 4888) and ( projectNumber <> 8000) and (( projectName like 'a%') or ( projectName like 'k%')))
        /// WHERE (( projectType = 'Combo') or ( projectType = '504'))
        /// </returns>
        private string ParseFilterExpression(DataSourceRequest request)
        {
            if (request.Filter == null)
            {
                return "";
            }
            else
            {
                var filter = new Filter
                {
                    filters = request.Filter.filters,
                    Logic = request.Filter.Logic
                };
                var expression = DataSourceFilterMapHelper.RecursiveFilterExpressionBuilder(filter);
                return (expression == null || expression.Equals("")) ? "" : string.Format("WHERE {0}", expression);
            }
        }

        /// <summary>
        /// Example to Add SQL fields being used in the Kendo Grid
        /// </summary>
        private void AddFields()
        {
            DataSourceFilterMapHelper.AddColumnTypeMapping("projectNumber", FieldType.number);
            DataSourceFilterMapHelper.AddColumnTypeMapping("projectName", FieldType.@string);
            DataSourceFilterMapHelper.AddColumnTypeMapping("projectstatus", FieldType.@string);
            DataSourceFilterMapHelper.AddColumnTypeMapping("projectType", FieldType.@string);
            DataSourceFilterMapHelper.AddColumnTypeMapping("projectAmount", FieldType.number);
        }
        static void Main(string[] args)
        {
        }
    }
}
