using System.Collections.Generic;

namespace WebBase.Models.ViewModels
{
    public class Pagination<T>
    {
        public List<T> items { get; set; }
        public int totalRecords { get; set; }
    }
}