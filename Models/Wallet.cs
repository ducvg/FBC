using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBC.Models;

public partial class Wallet
{
    public int WalletId { get; set; }

    public string Password { get; set; } = null!;

    public string? Bank { get; set; }

    public string? BankNumber { get; set; }

    public decimal? Credit { get; set; }

    public string Id { get; set; }
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;
}
