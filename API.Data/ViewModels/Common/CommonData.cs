using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Common
{
    public class CommonData:Paging
    {
        public int? Id { get; set; }
        public string? Search { get; set; }
    }
}
