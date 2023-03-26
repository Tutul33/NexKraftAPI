using System;
using System.Collections.Generic;

namespace API.Data.ORM.MySqlDataModel;

public partial class Userlogin
{
    public int LoginId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
}
