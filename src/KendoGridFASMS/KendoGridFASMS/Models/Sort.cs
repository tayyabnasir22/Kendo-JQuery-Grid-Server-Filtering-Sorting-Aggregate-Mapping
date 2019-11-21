using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridParameterParser.Models
{
    /// <summary>
    /// Represent the Sort Parameter for Kendo Data Source Request
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// The field to apply sort to
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
        /// Asc or Desc sort order
        /// </summary>
        private string _dir;
        public string Dir
        {
            get
            {
                return _dir;
            }
            set
            {
                _dir = value;
            }
        }

    }
}
