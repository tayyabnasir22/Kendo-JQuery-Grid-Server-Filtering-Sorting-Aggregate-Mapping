using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoGridFASMS.Refactor.Models
{
    /// <summary>
    /// Represents the Kendo Grid Data Source Request
    /// </summary>
    public class DataSourceRequest
    {
        private int _take;
        public int Take
        {
            get
            {
                return _take;
            }
            set
            {
                _take = value;
            }
        }

        private int _skip;
        public int Skip
        {
            get
            {
                return _skip;
            }
            set
            {
                _skip = value;
            }
        }

        private int _page;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
            }
        }

        private int _pageSize;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        public List<Sort> Sort { get; set; }
        public List<Aggregate> Aggregate { get; set; }
        public Filters Filter { get; set; }
    }
}
