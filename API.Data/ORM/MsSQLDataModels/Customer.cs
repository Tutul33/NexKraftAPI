using System;
using System.Collections.Generic;

namespace API.Data.ORM.MsSQLDataModels;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Country { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; } = new List<UserLogin>();
}
