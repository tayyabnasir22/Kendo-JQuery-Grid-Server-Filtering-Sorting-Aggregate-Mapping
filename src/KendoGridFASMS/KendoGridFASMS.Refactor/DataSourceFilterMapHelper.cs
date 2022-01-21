using KendoGridFASMS.Refactor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Notes
// Why is DataSourceFilterMapHelper static? This means I'll only be able
// to have a single DataSourceFilterMapHelper per Service Layer class. 
//
// 
//
// 
//
//
//
//
//
//
//
#endregion

#region Changes
// 1. Allowing for class to be "newed up" / making class non static
//
//
//
//
//
//
#endregion


namespace KendoGridFASMS.Refactor
{
    public class DataSourceFilterMapHelper_Refactor
    {
        public string RecursiveFilterExpressionBuilder(Filter filter)
        {
            if (string.IsNullOrEmpty(filter.Logic)) {
                return "( " + MapToExpression(filter.field, filter.Value, filter.Operator) + ")";
            }
            else {
                int count = 0;
                string expression = "";

                foreach (Filter temp in filter.filters) {

                    if (count == 0) {
                        expression = RecursiveFilterExpressionBuilder(temp);
                    }
                    else {
                        expression = expression + " " + filter.Logic + " " + RecursiveFilterExpressionBuilder(temp);
                    }

                    count++;
                }
                
                return "(" + expression + ")";
            }
        }



        
        //I'm doing too much, I'm just going to add a new object with database column name and this fieldtype var 
        public void AddColumnTypeMapping(string _fieldName, FieldType _fieldType)
        {
            //this is being exposed to the clinet, this needs to be able to capture the 
            //name of the column in the database 

            //and the data type to correctly format the information
            //I propose a new dto type object to be introduce: 

            //fieldName = this is going to be the name of the piece of data being shown in the kendo grid
            //columnName = this is going to be the name of the column for our sql query
            //the ui / client will have the name of the field coming from the kendo grid and 
            //name can / should be different than whatever we have in the query

            //FieldType = so we know the datatype of what is being displayed on the ui
            //


            //uiField
            //dbField
            //fieldType

            //builder pattern?
            ColumnTypeMapping.Add(_fieldName, _fieldType);
        }

        #region Private Methods 



        //this can also be left alone
        private static Dictionary<string, SQLOperator> OperatorsMapping { get; set; }

        //this will be changing to use an instance member
        private static Dictionary<string, FieldType> ColumnTypeMapping { get; set; }



        //I thinkI can salvage this
        static DataSourceFilterMapHelper_Refactor()
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
            ColumnTypeMapping = new Dictionary<string, FieldType>();
        }




        private string FormatForTypeType(SQLOperator _sqlOperator, string _field, string _value, FieldType _type)
        {
            //1. For character type operator no need to differentiate between field type
            if (_sqlOperator.Type == OperatorType.Character)
            {
                return string.Format(_sqlOperator.SQLExpression, _field, _value);
            }
            //2. For general type we can accept any type for field so parse check
            else
            {
                //Why so many ternary operators ? 
                return (_type == FieldType.@string || _type == FieldType.date) ? string.Format(_sqlOperator.SQLExpression, _field, "\'" + _value + "\'") : string.Format(_sqlOperator.SQLExpression, _field, _value);
            }
        }




        //this method will do the translating from ui column to database column
        private string MapToExpression(string _field, string _value, string _operator)
        {
            //1. Get the operator Mapping
            SQLOperator _sqlOperator = null;
            OperatorsMapping.TryGetValue(_operator, out _sqlOperator);

            //2 Get type mapping
            FieldType _type;
            ColumnTypeMapping.TryGetValue(_field, out _type);

            //3. Append value to string
            return FormatForTypeType(_sqlOperator, _field, _value, _type);
        }
        #endregion

    }//end refactor class




    













    public static class DataSourceFilterMapHelper
    {
        private static Dictionary<string, SQLOperator> OperatorsMapping { get; set; }

        private static Dictionary<string, FieldType> ColumnTypeMapping { get; set; }

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
            ColumnTypeMapping = new Dictionary<string, FieldType>();
        }

        /// <summary>
        /// Add a field to be filtered with its SQL name and type
        /// </summary>
        /// <param name="_fieldName"></param>
        /// <param name="_fieldType"></param>
        public static void AddColumnTypeMapping(string _fieldName, FieldType _fieldType)
        {
            ColumnTypeMapping.Add(_fieldName, _fieldType);
        }

        /// <summary>
        /// Remove an added field
        /// </summary>
        /// <param name="_fieldName"></param>
        public static void RemvoeColumnTypeMapping(string _fieldName)
        {
            ColumnTypeMapping.Remove(_fieldName);
        }
        
        /// <summary>
        /// Format Expression based on the Field Type
        /// </summary>
        /// <param name="_sqlOperator"></param>
        /// <param name="_field"></param>
        /// <param name="_value"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        private static string FormatForTypeType(SQLOperator _sqlOperator, string _field, string _value, FieldType _type)
        {
            //1. For character type operator no need to differentiate between field type
            if (_sqlOperator.Type == OperatorType.Character)
            {
                return string.Format(_sqlOperator.SQLExpression, _field, _value);
            }
            //2. For general type we can accept any type for field so parse check
            else
            {
                return (_type == FieldType.@string || _type == FieldType.date) ? string.Format(_sqlOperator.SQLExpression, _field, "\'" + _value + "\'") : string.Format(_sqlOperator.SQLExpression, _field, _value);
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

            //2 Get type mapping
            FieldType _type;
            ColumnTypeMapping.TryGetValue(_field, out _type);
            
            //3. Append value to string
            return FormatForTypeType(_sqlOperator, _field, _value, _type);
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
