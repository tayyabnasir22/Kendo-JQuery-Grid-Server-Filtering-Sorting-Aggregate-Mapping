using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor.Models
{
    /// <summary>
    /// Represents the Kendo Source Request Filters
    /// </summary>
    public class Filters
    {
        /// <summary>
        /// The logical operator
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
        /// Subfilters that will be concaenated by this level logical operator
        /// </summary>
        public List<Filter> filters { get; set; }
    }
}
