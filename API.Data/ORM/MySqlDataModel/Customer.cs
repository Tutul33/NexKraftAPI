using System;
using System.Collections.Generic;

namespace API.Data.ORM.MySqlDataModel;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Customerlogin> Customerlogins { get; } = new List<Customerlogin>();
}
