using CoreShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Specifications
{
    public class CategoryListSpecification : BaseSpecification<Book, string>
    {
        public CategoryListSpecification() : base(null)
        {
            AddSelect(x => x.Category);
            ApplyDistinct();
        }
    }
}
