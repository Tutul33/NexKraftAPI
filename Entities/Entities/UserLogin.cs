using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserLogin:IHasId
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public virtual Customer? Customer { get; set; }
    }

}
