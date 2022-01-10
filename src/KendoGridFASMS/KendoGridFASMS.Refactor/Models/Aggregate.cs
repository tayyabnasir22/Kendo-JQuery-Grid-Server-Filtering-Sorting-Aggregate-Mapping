using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor.Models
{
    /// <summary>
    /// Represents Kendo Data Source Request Aggregate
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// The field to be aggregated
        /// </summary>
        private string _field;
        public string Field
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
        /// The aggregate function
        /// </summary>
        private string _aggregate;
        public string aggregate
        {
            get
            {
                return _aggregate;
            }
            set
            {
                _aggregate = value;
            }
        }
    }
}
