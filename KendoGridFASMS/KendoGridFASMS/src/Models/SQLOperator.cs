using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridParameterParser.Models
{
    /// <summary>
    /// General kendo operators division
    /// </summary>
    public enum OperatorType
    {
        General,//Operators Common to all kendo Data types
        Character//Operators only allowed by kendo string types
    }

    /// <summary>
    /// The supported fields by kendo
    /// </summary>
    public enum FieldType
    {
        number,
        @string,
        date
        //No support for enums for now
    }

    /// <summary>
    /// SQL Expression and the operator type it represents
    /// </summary>
    public class SQLOperator
    {
        public string SQLExpression { get; set; }
        public OperatorType Type { get; set; }

        public SQLOperator(string sqlExpression, OperatorType type)
        {
            SQLExpression = sqlExpression;
            Type = type;
        }
    }
}
