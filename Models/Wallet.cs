using System;
using System.Collections.Generic;

namespace FBC.Models;

public partial class Wallet
{
    public int WalletId { get; set; }

    public string Password { get; set; } = null!;

    public string? Bank { get; set; }

    public string? BankNumber { get; set; }

    public decimal? Credit { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
