using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Specifications
{
    public class BookSpecParams : PagingParams
    {
        private List<string> _categories = [];

        public List<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public string? Sort { get; set; }

        private string _search;

        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }

    }
}
