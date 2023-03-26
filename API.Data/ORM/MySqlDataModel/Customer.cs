using System;
using System.Collections.Generic;

namespace API.Data.ORM.MySqlDataModel;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Country { get; set; } = null!;
}
