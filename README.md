# KendoGridFASMS (Kendo-Grid-Filtering-Aggregation-Sorting-Mapping-Server)
 
**WHY Needed?**
As of now no server side support for server filtering, sorting, aggregation is provided for Kendo jQuery Grid, especially if you want to parse the filters being passed as query parameters.
  
**WHAT it Does?**
KendoGridFASM provides functionality for ASP.Net (MVC/Web API) to not only map the Data Source Request to C# DTO but also provides helper functionality that can be used to generate dynamic expressions for filtering, sorting and aggregation which again can be used to generate dynamic WHERE, ORDER BY and GROUP BY clauses to be used to query your SQL data source.

**HOW to Do?**
Working with Kendo jQuery Gird you may have seen the filters, sorters and aggregate information being passed as:

    take: 20
    skip: 0
    page: 1
    pageSize: 20
    filter[logic]: or
    filter[filters][0][field]: projectName
    filter[filters][0][operator]: startswith
    filter[filters][0][value]: s
    filter[filters][1][field]: projectName
    filter[filters][1][operator]: startswith
    filter[filters][1][value]: p
    aggregate[0][field]: projectName
    aggregate[0][aggregate]: count
    aggregate[1][field]: projectAmount
    aggregate[1][aggregate]: sum
    aggregate[2][field]: projectFee
    aggregate[2][aggregate]: sum

You can use the `DataSourceRequest` to receive these either from the query parameters or the request body in your Action.

Next step is to provided the type for each of the column that will be used in the dynamic expression generation like:

    DataSourceFilterMapHelper.AddColumnTypeMapping("projectNumber", FieldType.number);
    DataSourceFilterMapHelper.AddColumnTypeMapping("projectName", FieldType.@string);
    DataSourceFilterMapHelper.AddColumnTypeMapping("projectstatus", FieldType.@string);
    DataSourceFilterMapHelper.AddColumnTypeMapping("projectType", FieldType.@string);
    DataSourceFilterMapHelper.AddColumnTypeMapping("projectAmount", FieldType.number);

So far string, number and date types are being supported and no support for enums exist.

You can generate sql like expressions for these columns using the `DataSourceFilterMapHelper`, `DataSourceSortMapHelper` and `DataSourceAggregateMapHelper` class as follows:

    var expression = DataSourceFilterMapHelper.RecursiveFilterExpressionBuilder(filter);
Will generate expressions as follows:

    (( projectNumber >= 123))
    (( projectNumber >= 123) and ( projectName like 'a%'))
    (( projectNumber >= 123) and ( projectName like 'a%') and ( projectType = '504'))
    (( projectNumber >= 123) and ( projectName like 'a%') and (( projectType = 'Combo') or ( projectType = '504')))
    (( projectName like 'a%') or ( projectName like '%a%'))
    ((( projectName like 'a%') or ( projectName like '%a%')) and ( projectAmount > 1))

You can prepend a WHERE to get your dynamic where clause that can then be used in your SQL queries.

Similarly for sorting: 

    var sorter = DataSourceSortMapHelper.SortExpressionBuilder(request.Sort);
Will generate expressions as:
 

    projectName desc, projectAmount asc
Again you can prepend ORDER BY to this expression to get your dynamic order by clause for your SQL queries.

Similarly for aggregation:

    AggregateResult aggr= DataSourceAggregateMapHelper.AggregateExpressionBuilder(request.Aggregate);
Where `AggregateResult` has two properties:
**AggregateFields**: returns all the fields that can be used in GROUP BY clause like:

    projectName, projectAmount, projectFee
**AggregateFunction**: returns an expression with all the fields associated with their specified aggregate functions that can be made part of SELECT clause:

    count(projectName) , sum(projectAmount) , sum(projectFee)



## Unified Example:

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
                if(aggregate==null)
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
