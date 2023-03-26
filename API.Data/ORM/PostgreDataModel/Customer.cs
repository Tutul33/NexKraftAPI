using System;
using System.Collections.Generic;

namespace API.Data.ORM.PostgreDataModel;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Country { get; set; }
}
