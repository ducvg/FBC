using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Ban { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>(); 
}
