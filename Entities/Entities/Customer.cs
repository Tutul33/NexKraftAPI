using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Customer:IHasId
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public string? Country { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; } = new List<UserLogin>();
    }
}
