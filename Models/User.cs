using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class User : IdentityUser
{
    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public bool? Gender { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<CartOrder> CartOrders { get; set; } = new List<CartOrder>();

    //public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<ExchangeRequest> ExchangeRequests { get; set; } = new List<ExchangeRequest>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
