using System.Collections.Generic;

namespace WebBase.Models.ViewModels
{
    public class Pagination<T>
    {
        public List<T> item { get; set; }
        public int totalRecord { get; set; }
    }
}