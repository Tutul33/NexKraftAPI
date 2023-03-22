using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ViewModels.Common
{
    public class CommonData
    {
        public int? Id { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public bool? IsPaging { get; set; }
        public string? Search { get; set; }
    }
}
