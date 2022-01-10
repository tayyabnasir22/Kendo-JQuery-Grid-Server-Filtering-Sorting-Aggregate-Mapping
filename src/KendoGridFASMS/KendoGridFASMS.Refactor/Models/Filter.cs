using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor.Models
{
    /// <summary>
    /// Represent the basic level filter
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// The field to which filter has to applied to
        /// </summary>
        private string _field;
        public string field
        {
            get
            {
                return _field;
            }
            set
            {
                _field = value;
            }
        }

        /// <summary>
        /// The operator that will filter the field
        /// </summary>
        private string _operator;
        public string Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value;
            }
        }

        /// <summary>
        /// The specific value for the field for the filter
        /// </summary>
        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Logical operator between the sub filters if any
        /// </summary>
        private string _logic;
        public string Logic
        {
            get
            {
                return _logic;
            }
            set
            {
                _logic = value;
            }
        }

        /// <summary>
        /// Sub filters
        /// </summary>
        public List<Filter> filters { get; set; }
    }
}
