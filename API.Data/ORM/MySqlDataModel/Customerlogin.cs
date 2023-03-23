using System;
using System.Collections.Generic;

namespace API.Data.ORM.MySqlDataModel;

public partial class Customerlogin
{
    public int LoginId { get; set; }

    public int? CustomerId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public virtual Customer? Customer { get; set; }
}
