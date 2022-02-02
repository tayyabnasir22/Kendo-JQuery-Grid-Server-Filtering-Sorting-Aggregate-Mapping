﻿using KendoGridFASMS.Refactor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor
{
    public static class DataSourceFilterMapHelper
    {
        private static Dictionary<string, SQLOperator> OperatorsMapping { get; set; }

        private static Dictionary<string, ColumnMapping> ColumnMapping { get; set; }

        static DataSourceFilterMapHelper()
        {
            //1. Initiate Mappings for Operator Expressions
            OperatorsMapping = new Dictionary<string, SQLOperator>();
            OperatorsMapping.Add("eq", new SQLOperator("{0} = {1}", OperatorType.General));
            OperatorsMapping.Add("neq", new SQLOperator("{0} <> {1}", OperatorType.General));
            OperatorsMapping.Add("gte", new SQLOperator("{0} >= {1}", OperatorType.General));
            OperatorsMapping.Add("gt", new SQLOperator("{0} > {1}", OperatorType.General));
            OperatorsMapping.Add("lte", new SQLOperator("{0} <= {1}", OperatorType.General));
            OperatorsMapping.Add("lt", new SQLOperator("{0} < {1}", OperatorType.General));
            OperatorsMapping.Add("startswith", new SQLOperator("{0} like \'{1}%\'", OperatorType.Character));
            OperatorsMapping.Add("contains", new SQLOperator("{0} like \'%{1}%\'", OperatorType.Character));
            OperatorsMapping.Add("doesnotcontain", new SQLOperator("{0} not like \'%{1}%\'", OperatorType.Character));
            OperatorsMapping.Add("endswith", new SQLOperator("{0} like \'%{1}\'", OperatorType.Character));
            OperatorsMapping.Add("isempty", new SQLOperator("datalength({0}) = 0", OperatorType.General));
            OperatorsMapping.Add("isnotempty", new SQLOperator("datalength({0}) <> 0", OperatorType.General));
            OperatorsMapping.Add("isnull", new SQLOperator("{0} is null", OperatorType.General));
            OperatorsMapping.Add("isnotnull", new SQLOperator("{0} is not null", OperatorType.General));


            //2. Initiate Mappings for Field types
            ColumnMapping = new Dictionary<string, ColumnMapping>();
        }

        /// <summary>
        /// Add a field to be filtered with its SQL name and query column mapping
        /// </summary>
        /// <param name="_fieldName"></param>
        /// <param name="_mapping"></param>
        public static void AddColumnMapping(string _fieldName, ColumnMapping _mapping)
        {
            ColumnMapping.Add(_fieldName, _mapping);
        }

        /// <summary>
        /// Add a field to be filtered with its SQL name and type
        /// </summary>
        /// <param name="_fieldName"></param>
        /// <param name="_fieldType"></param>
        public static void AddColumnMapping(string _fieldName, FieldType _fieldType)
        {
            ColumnMapping.Add(_fieldName, new ColumnMapping()
            {
                Type = _fieldType,
                QueryColumnName = _fieldName
            });
        }

        /// <summary>
        /// Remove an added field
        /// </summary>
        /// <param name="_fieldName"></param>
        public static void RemoveColumnTypeMapping(string _fieldName)
        {
            ColumnMapping.Remove(_fieldName);
        }

        /// <summary>
        /// Format Expression based on the Field Type
        /// </summary>
        /// <param name="_sqlOperator"></param>
        /// <param name="_field"></param>
        /// <param name="_value"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        private static string FormatForTypeType(SQLOperator _sqlOperator, string _field, string _value, ColumnMapping _mapping)
        {
            //1. For character type operator no need to differentiate between field type
            if (_sqlOperator.Type == OperatorType.Character)
            {
                return string.Format(_sqlOperator.SQLExpression, _mapping.QueryColumnName , _value);
            }
            //2. For general type we can accept any type for field so parse check
            else
            {
                return (_mapping.Type == FieldType.@string || _mapping.Type == FieldType.date) ? string.Format(_sqlOperator.SQLExpression, _mapping.QueryColumnName , "\'" + _value + "\'") : string.Format(_sqlOperator.SQLExpression, _mapping.QueryColumnName , _value);
            }
        }

        /// <summary>
        /// Map the field, value and operator to expression
        /// </summary>
        /// <param name="_field"></param>
        /// <param name="_value"></param>
        /// <param name="_operator"></param>
        /// <returns></returns>
        private static string MapToExpression(string _field, string _value, string _operator)
        {
            //1. Get the operator Mapping
            SQLOperator _sqlOperator = null;
            OperatorsMapping.TryGetValue(_operator, out _sqlOperator);

            //2 Get column mapping
            ColumnMapping _mapping;
            ColumnMapping.TryGetValue(_field, out _mapping);

            //3. Append value to string
            return FormatForTypeType(_sqlOperator, _field, _value, _mapping);
        }

        /// <summary>
        /// To map the Data source Request filter to expression that can be used in Where clause
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string RecursiveFilterExpressionBuilder(Filter filter)
        {
            if (filter.Logic == null || filter.Logic.Equals(""))
            {
                return "( " + MapToExpression(filter.field, filter.Value, filter.Operator) + ")";
            }
            else
            {
                int count = 0;
                var expression = "";

                filter.filters.ForEach((filt) =>
                {
                    expression = (count == 0) ? (RecursiveFilterExpressionBuilder(filt)) : (expression + " " + filter.Logic + " " + RecursiveFilterExpressionBuilder(filt));

                    count++;
                });

                return "(" + expression + ")";
            }
        }
    }
}